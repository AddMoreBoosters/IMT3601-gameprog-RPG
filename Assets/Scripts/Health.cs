using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event System.Action<Health> OnHealthAdded = delegate { };
    public static event System.Action<Health> OnHealthRemoved = delegate { };

    [SerializeField]
    private int maxHealth = 1;
    private int currentHealth;

    public HealthBar healthbarPrefab;

    public event System.Action<float> OnHealthChanged = delegate { };
    public event System.Action<string> OnStatusChanged = delegate { };

    private void Start()
    {
        currentHealth = maxHealth;
        //Debug.Log("Enabling health!");
        OnHealthAdded(this);
    }

    public void ModifyHealth (int amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        float currentHealthPercentage = (float)currentHealth / (float)maxHealth;
        OnHealthChanged(currentHealthPercentage);
    }

    public void ChangeStatus(string status)
    {
        OnStatusChanged(status);
    }

    private void OnDestroy()
    {
        //Debug.Log("Disabling health.");
        OnHealthRemoved(this);
    }
}
