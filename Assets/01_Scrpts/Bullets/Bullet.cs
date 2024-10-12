using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float timeToDestroy = 0.5f; 
    public float damage = 1f;

    private Transform player; 
    private Vector3 targetDirection; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetDirection = (player.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

