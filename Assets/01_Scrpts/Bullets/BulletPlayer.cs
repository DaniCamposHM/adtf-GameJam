using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float speed = 10f;  
    public float timeToDestroy = 2f;  
    public float damage = 1f;  // Daño que hace la bala

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);  // Hacer daño al enemigo
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Arbol"))
        {
            Destroy(gameObject);  
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Daño Boss");
            other.gameObject.GetComponent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
