using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 100;

    private int currentHealth;
    private bool isDead = false;
    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDead) return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.y = 0;
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        if (isDead) return;

        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Salud del jugador: " + currentHealth);
    }

    void Die()
    {
        isDead = true;
        Debug.Log("El jugador ha muerto.");
        // Aquí añadir lógica para mostrar una pantalla de "Game Over"
        gameObject.SetActive(false);
    }
}
