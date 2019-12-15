using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject loseScreen;

    public static bool gameFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        gameFinished = false;
        Enemy.OnEnemyDied += AddScore;
        PlayerMovement.OnPlayerDied += GameOver;
    }

    private void AddScore(int amount)
    {
        score += amount;
        if (!AreThereEnemiesLeft())
            GameWon();
    }

    private void GameOver ()
    {
        if (!gameFinished)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
            gameFinished = true;
        }
    }

    private void GameWon ()
    {
        if (!gameFinished)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            gameFinished = true;
        }
    }

    private bool AreThereEnemiesLeft ()
    {
        if (FindObjectOfType<Enemy>() != null)
        {
            return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDied -= AddScore;
        PlayerMovement.OnPlayerDied -= GameOver;
    }
}
