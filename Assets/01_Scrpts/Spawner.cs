using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> enemyPrefabs;
    float timer = 0;
    public float timeBtwSpawn = 7f;
    public Transform target;
    public float rotationSpeed = 50;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = target.position;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        timer += Time.deltaTime;
        if(timer >= timeBtwSpawn)
        {
            timer = 0;
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                spawnPoints[Random.Range(0, spawnPoints.Count)].position,
                Quaternion.identity);
        }
    }
}
