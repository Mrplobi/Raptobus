using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class ObstacleSpawner : MonoBehaviour
    {
        List<Obstacle> obstacles = new List<Obstacle>();
        public Transform obstacleParent;
        public List<GameObject> obstaclePrefabs;
        public List<PatternDescriptor> patternslvl1;
        public List<PatternDescriptor> patternslvl2;
        public List<PatternDescriptor> patternslvl3;
        public PatternDescriptor emptyPattern;

        private bool isFirstObstacle = true;
        private int curLvl = 0;


        [SerializeField]
        private List<PatternDescriptor> completePatterns = new List<PatternDescriptor>();
        public float xSpawnPosition;

        float timer = 0;
        
        private List<PatternDescriptor>[] patterns = new List<PatternDescriptor>[3];
        private int idPattern = 0;

        private void Start()
        {
            GameManager.Instance.onReset += Restart;
            patterns[0] = patternslvl1;
            patterns[1] = patternslvl2;
            patterns[2] = patternslvl3;
            ChoosePaterns();
        }

        private void Update()
        {
            if (GameManager.Instance.Playing)
            {
                timer -= Time.deltaTime;
                if (timer <= 0 && idPattern < completePatterns.Count)
                {
                    LaunchPattern(completePatterns[idPattern]);
                    idPattern++;
                }
            }
        }

        public void LaunchPattern(PatternDescriptor descriptor)
        {
            Debug.Log("Launching " + descriptor.name);
            // If launching empty > either 1st empty or lvl up > speed up background
            if (descriptor.difficultyLvl == 0)
            {
                BackgroundManager.Instance.SpeedUp(curLvl);
            }
            foreach (ObstacleDescriptor obstacleDes in descriptor.obstacles)
            {
                Obstacle obstacle = obstacles.Find(x => x.available == true && x.type == obstacleDes.type);
                if (obstacle == null)
                {
                    obstacle = SpawnNewObstacle(obstacleDes);
                    obstacles.Add(obstacle.GetComponent<Obstacle>());
                }
                obstacle.transform.position = new Vector3(xSpawnPosition + obstacleDes.offset, obstacle.spawnHeight);
                obstacle.transform.SetParent(obstacleParent);
                obstacle.Launch(descriptor.difficultyLvl);
                if(curLvl < descriptor.difficultyLvl)
                {
                    curLvl = descriptor.difficultyLvl;
                }
            }
            timer = descriptor.totalPatternTime;
        }

        public Obstacle SpawnNewObstacle(ObstacleDescriptor descriptor)
        {
            GameObject newObstacle = Instantiate(obstaclePrefabs[(int)descriptor.type], obstacleParent.transform);
            return newObstacle.GetComponent<Obstacle>();
        }

        private void ChoosePaterns()
        {
            completePatterns = new List<PatternDescriptor>();
            int raptorCount = 0;
            PatternDescriptor previousPattern = new PatternDescriptor();
            for (int i = 0; i < 3; i++)
            {
                AddEmpty(1);
                while (raptorCount < (i+1)*(GameManager.Instance.maxRaptor/3))
                {
                    PatternDescriptor newPattern = (patterns[i])[UnityEngine.Random.Range(0, patterns[i].Count)];
                    if(newPattern != previousPattern)
                    {
                        completePatterns.Add(newPattern);
                        previousPattern = newPattern;
                        raptorCount += newPattern.numberOfRaptors;
                        Progression.totalDist += newPattern.totalPatternTime;
                    }
                }
            }
            // Max raptor not a multiple of 3
            if(raptorCount != GameManager.Instance.maxRaptor)
            {
                while (raptorCount < GameManager.Instance.maxRaptor)
                {
                    PatternDescriptor newPattern = (patterns[2])[UnityEngine.Random.Range(0, patterns[2].Count)];
                    if (newPattern != previousPattern)
                    {
                        completePatterns.Add(newPattern);
                        previousPattern = newPattern;
                        raptorCount += newPattern.numberOfRaptors;
                        Progression.totalDist += newPattern.totalPatternTime;
                    }
                }
            }
            AddEmpty(2);
        }

        private void AddEmpty(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                completePatterns.Add(emptyPattern);
                Progression.totalDist += emptyPattern.totalPatternTime;
            }
        }

        private void Restart()
        {
            curLvl = 0;
            timer = 2;
            idPattern = 0;
            Progression.totalDist = 0;
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Free();
                // Can't reuse raptor otherwise sprites goes all wrong (last moment fix)
                if (obstacle.GetComponent<Raptor>() != null)
                {
                    obstacle.available = false;
                }
            }
            ChoosePaterns();
        }
    }
}