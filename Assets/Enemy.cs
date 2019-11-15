using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int pointsValue = 1;
    [SerializeField]
    private int health = 1;

    public static event System.Action<int> OnEnemyDied = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        OnEnemyDied(pointsValue);
    }
}
