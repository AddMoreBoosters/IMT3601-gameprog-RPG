using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Transform parentTransform;

    [SerializeField]
    private float fireRateRPM = 60f;
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponent<Transform>();
        cooldown = 60f / fireRateRPM;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown < (60f / fireRateRPM))
            cooldown += Time.deltaTime;

        if (Input.GetMouseButton(0) && cooldown >= (60f / fireRateRPM) && !MyPauseMenu.gameIsPaused)
        {
            cooldown = 0f;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.y = 0f;
            Vector3 direction = mousePosition - parentTransform.position;
            direction.y = 0f;
            direction.Normalize();

            var instantiatedProjectile = ProjectilePool.Instance.Get();
            instantiatedProjectile.transform.position = parentTransform.position + direction;
            float yAngle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(new Vector3(1, 0, 0), direction));
            if (direction.z >= 0f)
            {
                yAngle *= -1;
            }
            instantiatedProjectile.transform.eulerAngles = new Vector3(0f, yAngle, 0f);
            instantiatedProjectile.gameObject.SetActive(true);
        }
    }
}
