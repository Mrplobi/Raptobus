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
            completePatterns = ChoosePaterns();
        }

        private void Update()
        {
            if (GameManager.Instance.playing)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    LaunchPattern(completePatterns[idPattern]);
                    idPattern++;
                }
            }
        }

        public void LaunchPattern(PatternDescriptor descriptor)
        {
            Debug.Log("Launching " + descriptor.name);
            foreach (ObstacleDescriptor obstacleDes in descriptor.obstacles)
            {
                Obstacle obstacle = obstacles.Find(x => x.available == true && x.type == obstacleDes.type);
                if (obstacle == null)
                {
                    obstacle = SpawnNewObstacle(obstacleDes);
                    obstacles.Add(obstacle.GetComponent<Obstacle>());
                }
                obstacle.transform.position = new Vector3(xSpawnPosition + obstacleDes.offset, obstacle.spawnHeight);
                obstacle.Launch(descriptor.difficultyLvl);
            }
            timer = descriptor.totalPatternTime;            
        }

        public Obstacle SpawnNewObstacle(ObstacleDescriptor descriptor)
        {
            GameObject newObstacle = Instantiate(obstaclePrefabs[(int)descriptor.type], obstacleParent.transform);
            return newObstacle.GetComponent<Obstacle>();
        }

        private List<PatternDescriptor> ChoosePaterns()
        {
            int raptorCount = 0;
            List<PatternDescriptor> totalPatterns = new List<PatternDescriptor>();
            PatternDescriptor previousPattern = new PatternDescriptor();
            for (int i = 0; i < 3; i++)
            {
                while (raptorCount < (i+1)*10)
                {
                    PatternDescriptor newPattern = (patterns[i])[UnityEngine.Random.Range(0, patterns[i].Count)];
                    if(newPattern != previousPattern)
                    {
                        totalPatterns.Add(newPattern);
                        previousPattern = newPattern;
                        raptorCount += newPattern.numberOfRaptors;
                        Progression.totalDist += newPattern.totalPatternTime;
                    }
                }
            }
            return totalPatterns;
        }

        private void Restart()
        {
            timer = 2;
            idPattern = 0;
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Free();
            }
            completePatterns = ChoosePaterns();
        }
    }
}