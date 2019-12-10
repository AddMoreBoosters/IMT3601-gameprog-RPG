using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float noiseTravelDistance = 20f;
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
            NoiseManager.instance.MakeNoise(transform.position, noiseTravelDistance);
        }
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("Explosion");
        }
    }
}
