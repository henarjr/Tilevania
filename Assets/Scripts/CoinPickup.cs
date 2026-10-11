using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 1;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;

            FindAnyObjectByType<GameSession>()
                .AddToScore(pointsForCoinPickup);

            AudioSource.PlayClipAtPoint(
                coinPickupSFX,
                transform.position
            );

            Destroy(gameObject);
        }
    }
}