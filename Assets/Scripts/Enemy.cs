using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int pointsValue = 1;
    [SerializeField]
    private int damage = 1;

    public static event System.Action<int> OnEnemyDied = delegate { };

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = FindObjectOfType<PlayerMovement>().transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health destructibleTarget = collision.gameObject.GetComponent<Health>();
        if (destructibleTarget != null && destructibleTarget.gameObject.CompareTag("Player"))
        {
            destructibleTarget.ModifyHealth(-damage);
        }
    }

    private void OnDestroy()
    {
        OnEnemyDied(pointsValue);
    }
}
