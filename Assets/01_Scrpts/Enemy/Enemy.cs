using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;  
    public float range = 30f;  
    public float bulletSpeed = 27f;
    public float timeBtwShoot = 1f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float moveSpeed = 5f;  
    public float hp = 3f;  

    private float timer = 0f;
    private bool targetInRange = false;
    private float changeDirectionTime = 2f;  
    private float changeDirectionTimer = 0f;
    private Vector3 randomDirection;  

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeRandomDirection();  
    }

    void Update()
    {
        SearchTarget();

        if (targetInRange)
        {
            StopAndShoot();
        }
        else
        {
            MoveRandomly();  
        }
    }

    void SearchTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        targetInRange = (distance <= range);
        
        if (targetInRange)
        {
            ChasePlayer();
        }
    }

    void StopAndShoot()
    {
        RotateToTarget();

        if (timer >= timeBtwShoot)
        {
            timer = 0f;
            Shoot();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
    }

    void RotateToTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    void MoveRandomly()
    {
        changeDirectionTimer += Time.deltaTime;
        if (changeDirectionTimer >= changeDirectionTime)
        {
            ChangeRandomDirection();
            changeDirectionTimer = 0f;
        }

        transform.position += randomDirection * moveSpeed * Time.deltaTime;
    }

  
    void ChasePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized; 
        transform.position += direction * moveSpeed * Time.deltaTime; 
        RotateToTarget(); 
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);  
        }
    }

    void ChangeRandomDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
