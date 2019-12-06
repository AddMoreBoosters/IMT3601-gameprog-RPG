using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    private bool shouldExplode = true;

    private void OnApplicationQuit()
    {
        shouldExplode = false;
    }
    private void OnDestroy()
    {
        if (shouldExplode)
        {
            Instantiate(explosionPrefab).transform.position = transform.position;
        }
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("Explosion");
        }
    }
}
