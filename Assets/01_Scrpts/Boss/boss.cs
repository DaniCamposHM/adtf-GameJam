using UnityEngine;

public class Boss : MonoBehaviour
{
    public float range = 100f;
    public float timeBtwAttack = 1f;
    public float moveSpeed = 3f;
    public float hp = 10f;
    public Collider swordCollider;

    private float timer = 0f;
    private bool targetInRange = false;
    private bool hasStopped = false;
    private float changeDirectionTime = 2f;
    private float changeDirectionTimer = 0f;
    private Transform target;
    private Animator animator;

    private Vector3 randomDirection;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
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

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetBool("isAttacking", false);
            swordCollider.enabled = false;
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

            if (timer >= timeBtwAttack)
            {
                timer = 0f;
                Attack();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    void Attack()
    {
        swordCollider.enabled = true;
        animator.SetBool("isAttacking", true);
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
            Destroy(gameObject);
        }
    }

    void ChangeRandomDirection()
    {
        // Generar una nueva dirección aleatoria
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
