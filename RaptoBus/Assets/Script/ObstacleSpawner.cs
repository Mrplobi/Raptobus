using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class ObstacleSpawner : MonoBehaviour
    {
        List<Obstacle> obstacles = new List<Obstacle>();
        public GameObject obstacleParent;
        public List<GameObject> obstaclePrefabs;
        public float xSpawnPosition;
        public float spawnTime;
        float timer = 0;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                LaunchObstacle();
                timer = 0;
            }
        }

        public void LaunchObstacle()
        {
            Obstacle obstacle = obstacles.Find(x => x.available == true);
            if (obstacle != null)
            {
                obstacle.transform.position = new Vector3(xSpawnPosition, obstacle.spawnHeight);
            }
            else
            {
                obstacle = SpawnNewObstacle();
                obstacles.Add(obstacle.GetComponent<Obstacle>());
            }
            obstacle.transform.position = new Vector3(xSpawnPosition, obstacle.spawnHeight);
            obstacle.Launch();
        }

        public Obstacle SpawnNewObstacle()
        {
            System.Random random = new System.Random(Time.frameCount);
            GameObject newObstacle = Instantiate(obstaclePrefabs[random.Next(obstaclePrefabs.Count)]);
            return newObstacle.GetComponent<Obstacle>();
        }
    }
}