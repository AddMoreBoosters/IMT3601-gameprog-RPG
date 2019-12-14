using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private TextMeshProUGUI statusText = null;
    [SerializeField]
    private float updateTime = 0.2f;
    [SerializeField]
    private float positionOffset = 0f;

    private Health health;

    //  Let this healthbar know which Health script it's for
    public void SetHealth(Health health)
    {
        this.health = health;
        health.OnHealthChanged += HandleHealthChanged;
        health.OnStatusChanged += HandleStatusChanged;
    }

    private void HandleHealthChanged(float percentage)
    {
        StartCoroutine(ChangeToPercentage(percentage));
    }

    private void HandleStatusChanged(string status)
    {
        if (statusText != null)
        {
            statusText.text = status;
        }
    }

    private IEnumerator ChangeToPercentage(float percentage)
    {
        float preChangePercentage = foregroundImage.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < updateTime)
        {
            elapsedTime += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePercentage, percentage, elapsedTime / updateTime);
            yield return null;
        }
        //  Set fill to exact value to avoid inaccuracies
        foregroundImage.fillAmount = percentage;
    }

    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(health.transform.position + Vector3.forward * positionOffset);
    }

    private void OnDestroy()
    {
        health.OnHealthChanged -= HandleHealthChanged;
    }
}
