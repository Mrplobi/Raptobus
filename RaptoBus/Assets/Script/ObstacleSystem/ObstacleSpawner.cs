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
        public List<PatternDescriptor> patterns;
        public float xSpawnPosition;
        float timer = 0;

        private void Start()
        {
            GameManager.Instance.onReset += Restart;
        }

        private void Update()
        {
            if (GameManager.Instance.playing)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    LaunchPattern(patterns[UnityEngine.Random.Range(0, patterns.Count)]);
                }
            }
        }

        public void LaunchPattern(PatternDescriptor descriptor)
        {
            foreach (ObstacleDescriptor obstacleDes in descriptor.obstacles)
            {
                Obstacle obstacle = obstacles.Find(x => x.available == true && x.type == obstacleDes.type);
                if (obstacle == null)
                {
                    obstacle = SpawnNewObstacle(obstacleDes);
                    obstacles.Add(obstacle.GetComponent<Obstacle>());
                }
                obstacle.transform.position = new Vector3(xSpawnPosition + obstacleDes.offset, obstacle.spawnHeight);
                obstacle.Launch();
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