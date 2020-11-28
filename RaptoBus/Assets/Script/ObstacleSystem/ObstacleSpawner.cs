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
        private List<PatternDescriptor> currentPatterns;
        public float xSpawnPosition;

        float timer = 0;
        float globalDist = 0; // used to increment difficulty lvl
        float nextLvlDist = 10; // TODO optimize value
        int lvl = 1;
        private List<PatternDescriptor>[] patterns = new List<PatternDescriptor>[3];
        private int idPattern = 0;

        private void Start()
        {
            GameManager.Instance.onReset += Restart;
            patterns[0] = patternslvl1;
            patterns[1] = patternslvl2;
            patterns[2] = patternslvl3;
            currentPatterns = patterns[idPattern];
        }

        private void Update()
        {
            if (GameManager.Instance.playing)
            {
                timer -= Time.deltaTime;
                globalDist += Time.deltaTime;
                if(globalDist >= nextLvlDist && lvl < 3)
                {
                    globalDist = 0;
                    lvl++;
                    idPattern++;
                     currentPatterns = patterns[idPattern];
                }

                /*
                 * TODO > endgame
                 */

                if (timer <= 0)
                {
                    LaunchPattern(currentPatterns[UnityEngine.Random.Range(0, currentPatterns.Count)]);
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
                obstacle.Launch(lvl);
            }
            timer = descriptor.totalPatternTime;            
        }

        public Obstacle SpawnNewObstacle(ObstacleDescriptor descriptor)
        {
            GameObject newObstacle = Instantiate(obstaclePrefabs[(int)descriptor.type], obstacleParent.transform);
            return newObstacle.GetComponent<Obstacle>();
        }

        private void Restart()
        {
            timer = 2;
        }
    }
}