using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;  
    public float timeBtwSpawn = 7f;        
    public Transform player;               
    public float spawnRadius = 70f;        
    public float rotationSpeed = 50;       

    private float timer = 0;

    void Update()
    {
        transform.position = player.position;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        timer += Time.deltaTime;
        if (timer >= timeBtwSpawn)
        {
            timer = 0;
            Vector3 spawnPosition = GetRandomPositionAroundPlayer();
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection.y = 0;

        Vector3 spawnPosition = player.position + randomDirection;

        return spawnPosition;
    }
}
