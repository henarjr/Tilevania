using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        myRigidbody.linearVelocity = new Vector2(moveSpeed, 0f);

        FlipEnemyFacing();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;

    }

    void FlipEnemyFacing()
    {
        if (Mathf.Abs(moveSpeed) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(
                Mathf.Sign(moveSpeed),
                1f
            );
        }
    }
}