using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float currentLightLevel;
    [SerializeField]
    private float lightCheckInterval = 0.2f;
    private float timeSinceLightCheck = 0f;

    private Animator animator;

    public static event System.Action OnPlayerDied = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        OnPlayerDied();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //if (horizontal != 0f)
        //{
        //    gameObject.transform.position += new Vector3(horizontal * Time.deltaTime * speed, 0f, 0f);
        //}

        //if (vertical != 0f)
        //{
        //    gameObject.transform.position += new Vector3(0f, 0f, vertical * Time.deltaTime * speed);
        //}

        //  Turn towards mouse position
        Vector3 mousePosition = new Vector3(0f, 0f, 0f);
        Vector3 mouseDirection = new Vector3(0f, 0f, 0f);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            mousePosition = hit.point;
            mousePosition.y = transform.position.y;
            mouseDirection = mousePosition - transform.position;
            transform.LookAt(transform.position + mouseDirection);
        }
        
        //  Move
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        gameObject.transform.position += moveVector;

        //  Set animator parameters
        float moveAngle = Vector3.Angle(moveVector, transform.forward);

        if (moveVector.magnitude > 0.05f)
        {
            animator.SetFloat("Forward", Mathf.Cos(Mathf.Deg2Rad * moveAngle));
            animator.SetFloat("Strafe", Vector3.Dot(moveVector.normalized, transform.right));
        }
        else
        {
            animator.SetFloat("Forward", 0f);
            animator.SetFloat("Strafe", 0f);
        }

        //  Do light check occasionally
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
        //Debug.Log(currentLightLevel);
    }
}
