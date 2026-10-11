using UnityEngine;

public class PlayerMortality : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;

    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] AudioClip dyingSFX;

    CapsuleCollider2D bodyCollider;
    Animator animator;
    Rigidbody2D rb;

    void Awake()
    {
        bodyCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsAlive)
        {
            return;
        }

        Die();
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(
            LayerMask.GetMask("Enimes", "Hazards")))
        {
            IsAlive = false;

            animator.SetTrigger("Dying");

            rb.linearVelocity = deathKick;

            AudioSource.PlayClipAtPoint(
                dyingSFX,
                transform.position
            );

            FindAnyObjectByType<GameSession>()
                .ProcessPlayerDeath();
        }
    }
}