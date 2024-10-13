using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movimiento")]
    public float speed = 16f;

    [Header("Disparo")]
    public BulletPlayer bulletPrefab;
    public Transform firePoint;
    public float timeBtwShoot = 0.5f;
    public float bulletSpeed = 150f;
    public int bulletsCount = 30;  
    public int maxBullets = 30;    
    private float timer = 0f;
    private bool canShoot = true;

    [Header("Caracteriticas")]
    public float hp = 10f;
    public float maxHp = 10f;  

    [Header("Efectos")]
    public AudioClip shoot, die, reload;  
    public GameObject fuego;

    [Header("UI")]
    public Text ammoDisplay;  
    public Image healthBar;  

    void Start()
    {
        UpdateHealthBar();
        ammoDisplay.text = bulletsCount + "/" + maxBullets;
    }

    void Update()
    {
        Movement();
        Rotation();
        CheckIfCanShoot();
        Shoot();
        Reload();  
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

        if (dir.magnitude > 0)
        {
            Vector3 moveVelocity = dir.normalized * speed;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
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
                GameManager.instance.PlaySFX(shoot);

                b.speed = bulletSpeed;
                bulletsCount--;
                canShoot = false;
                ammoDisplay.text = bulletsCount + "/" + maxBullets;
            }
        }
    }

    void Reload()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.instance.PlaySFX(reload);
            bulletsCount = maxBullets;
            ammoDisplay.text = bulletsCount + "/" + maxBullets;
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            GameManager.instance.PlaySFX(die);
            GameManager.instance.gameOver = true;
            StartCoroutine(Dead());
        }

        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        healthBar.fillAmount = hp / maxHp;
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndMen");
        Destroy(gameObject);
    }
}
