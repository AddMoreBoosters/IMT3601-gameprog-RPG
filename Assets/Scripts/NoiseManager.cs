using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseManager : MonoBehaviour
{
    public static NoiseManager instance;

    public static event System.Action<Vector3, float> OnNoiseMade = delegate { };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void MakeNoise (Vector3 noisePosition, float noiseTravelDistance)
    {
        OnNoiseMade(noisePosition, noiseTravelDistance);
    }
}
