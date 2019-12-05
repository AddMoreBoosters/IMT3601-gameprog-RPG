using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnDestroy()
    {
        Instantiate(explosionPrefab);
        FindObjectOfType<AudioManager>().Play("Explosion");
    }
}
