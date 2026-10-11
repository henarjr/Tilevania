using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] int killingEnemyPoints = 50;
    [SerializeField] AudioClip enemyDeathSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            FindAnyObjectByType<GameSession>()
                .AddToScore(killingEnemyPoints);

            Destroy(other.gameObject);

            AudioSource.PlayClipAtPoint(
                enemyDeathSFX,
                transform.position
            );
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
