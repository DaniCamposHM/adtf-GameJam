using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float range = 10f;
    public float bulletSpeed = 27f;
    public float timeBtwShoot = 1f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float moveSpeed = 3f;
    private float timer = 0f;
    private bool targetInRange = false;
    private bool hasStopped = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
            MoveForward();
        }
    }

    void SearchTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        targetInRange = (distance <= range);

        if (targetInRange)
        {
            hasStopped = true;
        }
    }

    void StopAndShoot()
    {
        if (hasStopped)
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

    void MoveForward()
    {
        if (!hasStopped)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
