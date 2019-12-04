using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

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
        Enemy.OnEnemyDied += AddScore;
    }

    private void AddScore(int amount)
    {
        score += amount;
        Debug.Log(score);
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDied -= AddScore;
    }
}
