using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Transform parentTransform;

    //[SerializeField]
    //private float fireRateRPM = 60f;
    //[SerializeField]
    //private float noiseTravelDistance = 10f;
    //private float cooldown;
    [SerializeField]
    private Weapon[] weapons;
    private int weaponSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponent<Transform>();
        weapons[weaponSelected].cooldown = 60f / weapons[weaponSelected].fireRateRPM;
    }

    // Update is called once per frame
    void Update()
    {
        if (weapons[weaponSelected].cooldown < (60f / weapons[weaponSelected].fireRateRPM))
            weapons[weaponSelected].cooldown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSelected = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapons.Length >= 2)
                weaponSelected = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weapons.Length >= 3)
                weaponSelected = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (weapons.Length >= 4)
                weaponSelected = 3;
        }

        if (Input.GetMouseButton(0) && weapons[weaponSelected].cooldown >= (60f / weapons[weaponSelected].fireRateRPM) && !MyPauseMenu.gameIsPaused)
        {
            weapons[weaponSelected].cooldown = 0f;

            Vector3 mousePosition = new Vector3(0f, 0f, 0f);
            Vector3 direction = new Vector3(0f, 0f, 0f);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                mousePosition = hit.point;
                mousePosition.y = 0f;
                direction = mousePosition - parentTransform.position;
                direction.y = 0f;
                direction.Normalize();
            }

            var instantiatedProjectile = ProjectilePool.Instance.Get();
            instantiatedProjectile.transform.position = parentTransform.position + direction;
            float yAngle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(new Vector3(1, 0, 0), direction));
            if (direction.z >= 0f)
            {
                yAngle *= -1;
            }
            instantiatedProjectile.transform.eulerAngles = new Vector3(0f, yAngle, 0f);
            instantiatedProjectile.gameObject.SetActive(true);

            FindObjectOfType<AudioManager>().Play("Gunshot");
            NoiseManager.instance.MakeNoise(transform.position, weapons[weaponSelected].noiseTravelDistance);
        }
    }
}
