using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    UIController uiController;

    void Awake()
    {
        int numberGameSessions =
            FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        uiController = GetComponentInChildren<UIController>();
    }

    void Start()
    {
        uiController.UpdateLivesText();
        uiController.UpdateScoreText();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            Invoke("TakeLife", 0.5f);
        }
        else
        {
            Invoke("ResetGameSession", 0.5f);
        }
    }

    void TakeLife()
    {
        playerLives--;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );

        uiController.UpdateLivesText();
    }

    void ResetGameSession()
    {
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();

        SceneManager.LoadScene(4);

        Destroy(gameObject);
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;

        uiController.UpdateScoreText();
    }

    public int GetLives()
    {
        return playerLives;
    }

    public int GetScore()
    {
        return score;
    }
}