using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBarPrefab;

    private Dictionary<Health, HealthBar> healthbars = new Dictionary<Health, HealthBar>();

    private void Awake()
    {
        Debug.Log("Controller registering for events.");
        Health.OnHealthAdded += AddHealthBar;
        Health.OnHealthRemoved += RemoveHealthBar;
    }

    private void OnDestroy()
    {
        Debug.Log("Controller unregistering for events.");
        Health.OnHealthAdded -= AddHealthBar;
        Health.OnHealthRemoved -= RemoveHealthBar;
    }

    private void AddHealthBar(Health health)
    {
        if(healthbars.ContainsKey(health) == false)
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthbars.Add(health, healthBar);
            healthBar.SetHealth(health);
            Debug.Log("Added health bar");
        }
    }

    private void RemoveHealthBar(Health health)
    {
        if (healthbars.ContainsKey(health))
        {
            Destroy(healthbars[health].gameObject);
            healthbars.Remove(health);
            Debug.Log("Removed health bar");
        }
    }
}
