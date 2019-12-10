using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 1f;
    public float currentLightLevel;
    [SerializeField]
    private float lightCheckInterval = 0.2f;
    private float timeSinceLightCheck = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0f)
        {
            gameObject.transform.position += new Vector3(horizontal * Time.deltaTime * speed, 0f, 0f);
        }

        if (vertical != 0f)
        {
            gameObject.transform.position += new Vector3(0f, 0f, vertical * Time.deltaTime * speed);
        }

        timeSinceLightCheck += Time.deltaTime;
        if (timeSinceLightCheck >= lightCheckInterval)
        {
            timeSinceLightCheck = 0f;
            CheckLightLevel();
        }
    }

    private void CheckLightLevel()
    {
        Light[] lights;
        lights = FindObjectsOfType(typeof(Light)) as Light[];
        currentLightLevel = 0f;

        foreach(Light light in lights)
        {
            LightSource source = light.GetComponent<LightSource>();
            if (source != null)
            {
                currentLightLevel += source.LightOnObject(transform);
            }
        }
        Debug.Log(currentLightLevel);
    }
}
