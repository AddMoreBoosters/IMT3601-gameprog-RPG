using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float LightOnObject (Transform target)
    {
        Vector3 direction = target.position - (transform.position);
        float angle;
        Light light = GetComponent<Light>();

        if (direction.magnitude <= light.range)
        {
            //Debug.Log("Player is in range of light");
            angle = Vector3.Angle(transform.forward, direction);

            if (light.type == LightType.Point || angle <= (light.spotAngle / 2f))
            {
                //Debug.Log("Player is within spotlight angle");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, light.range))
                {
                    if (hit.transform.tag == "Player")
                    {
                        //Debug.Log("Player is in light");
                        return light.intensity / direction.magnitude;
                    }
                }
            }
        }
        return 0f;
    }
}
