using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movimiento")]
    public float speed = 7f;

    [Header("Disparo")]
    public BulletPlayer bulletPrefab;   // Cambiado a BulletPlayer
    public Transform firePoint;         
    public float timeBtwShoot = 2f;   
    public float bulletSpeed = 10f;     
    public int bulletsCount = 30;       
    private float timer = 0f;           
    private bool canShoot = true;       

    [Header("Caracteriticas")]
    public float hp = 10f;  // Vida del jugador


    [Header("Efectos")]
    public AudioClip shoot, die;

    public GameObject fuego;

    [Header("UI")]

    public Text health;

    
    void Start()
    {
        health.text = hp.ToString();
    }

    void Update()
    {
        Movement();
        Rotation();
        CheckIfCanShoot();  
        Shoot();            
    }

    void Rotation()
    {
        Vector3 dir = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        float angleY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = Quaternion.Euler(0, -angleY, 0);
    }

    void Movement()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = dir * speed + new Vector3(0, rb.velocity.y, 0);
    }

    void CheckIfCanShoot()
    {
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= timeBtwShoot)
            {
                timer = 0;
                canShoot = true;
            }
        }
    }

    void Shoot()
    {
        if (canShoot && bulletsCount > 0) 
        {
            if (Input.GetMouseButton(0)) 
            {
                BulletPlayer b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                
                //Efecto de Fuego al disparar (no es buena idea)
                //Instantiate(fuego, firePoint.position, firePoint.rotation);

                // Sonido de disparo
                GameManager.instance.PlaySFX(shoot);

                b.speed = bulletSpeed;  
                bulletsCount--;         
                canShoot = false;       
            }
        }
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {

            // Sonido de muerte
            GameManager.instance.PlaySFX(die);

            //Destroy(gameObject);  // Destruir al jugador cuando se queda sin vida

            // Cambio de Escena
            GameManager.instance.gameOver = true;
            StartCoroutine(Dead());

            // Texto para mostrar la vida
            health.text = hp.ToString();

        }
    }

    // Metodo para llevar a la ventana de muerte
    IEnumerator Dead()
    {
        //Instantiate(expolitonEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndMen");
        Destroy(gameObject);
    }

}
