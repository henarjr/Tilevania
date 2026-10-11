using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip bounceSFX;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myFeetCollider;

    PlayerMortality playerMortality;

    float gravityScaleAtStart;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        playerMortality = GetComponent<PlayerMortality>();

        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!playerMortality.IsAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        if (!playerMortality.IsAlive)
        {
            return;
        }

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!playerMortality.IsAlive)
        {
            return;
        }

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
            AudioSource.PlayClipAtPoint(
            jumpSFX,
            transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bouncing"))
        {
            AudioSource.PlayClipAtPoint(
                bounceSFX,
                transform.position
            );
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(
            moveInput.x * runSpeed,
            myRigidbody.linearVelocity.y
        );

        myRigidbody.linearVelocity = playerVelocity;

        bool hasHorizontalSpeed =
            Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed =
            Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(
                Mathf.Sign(myRigidbody.linearVelocity.x),
                1f
            );
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(
            LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        myRigidbody.gravityScale = 0f;

        Vector2 climbVelocity = new Vector2(
            myRigidbody.linearVelocity.x,
            moveInput.y * climbSpeed
        );

        myRigidbody.linearVelocity = climbVelocity;

        bool hasVerticalSpeed =
            Mathf.Abs(myRigidbody.linearVelocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("isClimbing", hasVerticalSpeed);
    }
}