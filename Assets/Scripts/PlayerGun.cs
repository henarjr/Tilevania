using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 20f;

    [SerializeField] private AudioClip shootSFX;

    PlayerInput playerInput;
    InputAction attackAction;

    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
        attackAction.performed += OnAttack;
    }

    void OnDestroy()
    {
        attackAction.performed -= OnAttack;
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        GameObject newBullet =
            Instantiate(bullet, transform.position, Quaternion.identity);

        float direction = transform.parent.localScale.x;

        Rigidbody2D bulletRb =
            newBullet.GetComponent<Rigidbody2D>();

        bulletRb.linearVelocity =
            new Vector2(direction * bulletSpeed, 0f);
        AudioSource.PlayClipAtPoint(shootSFX, transform.position);
    }
}