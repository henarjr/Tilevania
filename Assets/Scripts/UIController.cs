using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    GameSession gameSession;

    void Awake()
    {
        gameSession = GetComponentInParent<GameSession>();
    }

    public void UpdateLivesText()
    {
        livesText.text = gameSession.GetLives().ToString();
    }

    public void UpdateScoreText()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}