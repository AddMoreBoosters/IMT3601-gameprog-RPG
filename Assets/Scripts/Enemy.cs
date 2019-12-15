using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int pointsValue = 1;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float speed = 1f;

    private Transform player;
    [SerializeField]
    private float fovRadius = 10f;
    [SerializeField]
    private float fovAngle = 140f;
    [SerializeField]
    private float FOVcheckInterval = 0.5f;
    private float timeSinceFOVcheck = 0f;
    private bool canSeePlayer = false;

    public static event System.Action<int> OnEnemyDied = delegate { };

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = FindObjectOfType<PlayerMovement>().transform;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        NoiseManager.OnNoiseMade += OnHeardNoise;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceFOVcheck += Time.deltaTime;
        if (timeSinceFOVcheck >= FOVcheckInterval && player != null)
        {
            timeSinceFOVcheck = 0f;
            canSeePlayer = InFOV(player);
            if (canSeePlayer)
            {
                agent.destination = player.position;
                NoiseManager.instance.MakeNoise(transform.position, 10f);
                GetComponent<Health>().ChangeStatus("!");
                //  Play an appropriate sound file, don't have one yet
            }
        }
        animator.SetBool("HasPath", agent.hasPath);
        if (!agent.hasPath)
        {
            GetComponent<Health>().ChangeStatus("-");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health destructibleTarget = collision.gameObject.GetComponent<Health>();
        if (destructibleTarget != null && destructibleTarget.gameObject.CompareTag("Player"))
        {
            destructibleTarget.ModifyHealth(-damage);
        }
        else if(collision.gameObject.GetComponent<Projectile>() != null && player != null)
        {
            agent.destination = player.position;
            GetComponent<Health>().ChangeStatus("!");
        }
    }

    private void OnDestroy()
    {
        OnEnemyDied(pointsValue);
    }

    private void OnDisable()
    {
        NoiseManager.OnNoiseMade -= OnHeardNoise;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fovRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis((fovAngle / 2f), transform.up) * transform.forward * fovRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-(fovAngle / 2f), transform.up) * transform.forward * fovRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (FindObjectOfType<PlayerMovement>().transform.position - transform.position).normalized * fovRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * fovRadius);
    }

    public bool InFOV(Transform target)
    {
        Vector3 direction = target.position - (transform.position + Vector3.up);
        float angle;
        float detectionRange = (fovRadius * FindObjectOfType<PlayerMovement>().currentLightLevel);

        if (direction.magnitude <= detectionRange)
        {
            //Debug.Log("Player is in range");
            angle = Vector3.Angle(transform.forward, direction);

            if (angle <= (fovAngle / 2f))
            {
                //Debug.Log("Player is within FOV angle");
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, direction.normalized, out hit, detectionRange))
                {
                    if (hit.transform.tag == "Player")
                    {
                        //Debug.Log("Found Player!");
                        return true;
                    }
                }
            }
        }

        return false;
    }

    //  Investigate noises, if you're not already pursuing player
    public void OnHeardNoise (Vector3 noisePosition, float noiseTravelDistance)
    {
        if (!canSeePlayer)
        {
            Vector3 direction = noisePosition - transform.position;
            if (direction.magnitude <= noiseTravelDistance)
            {
                agent.destination = noisePosition;
                GetComponent<Health>().ChangeStatus("?");
            }
        }
    }
}
