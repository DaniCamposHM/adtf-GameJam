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
    public float moveSpeed = 3f;
    public float hp = 3f;  // Vida del enemigo

    private float timer = 0f;
    private bool targetInRange = false;
    private bool hasStopped = false;
    private float changeDirectionTime = 2f;  // Tiempo para cambiar de dirección
    private float changeDirectionTimer = 0f;

    private Vector3 randomDirection;  // Dirección aleatoria

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeRandomDirection();  // Definir dirección aleatoria inicial
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
            MoveRandomly();  // Buscar al jugador aleatoriamente
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
        else
        {
            hasStopped = false;
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

    void MoveRandomly()
    {
        if (!hasStopped)
        {
            // Cambiar de dirección cada 2 segundos
            changeDirectionTimer += Time.deltaTime;
            if (changeDirectionTimer >= changeDirectionTime)
            {
                ChangeRandomDirection();
                changeDirectionTimer = 0f;
            }

            transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
        }
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);  // Destruir al enemigo cuando se queda sin vida
        }
    }

    void ChangeRandomDirection()
    {
        // Generar una nueva dirección aleatoria
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
