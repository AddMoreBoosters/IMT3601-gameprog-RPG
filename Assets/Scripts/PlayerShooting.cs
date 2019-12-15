using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Transform parentTransform;
    [SerializeField]
    private Weapon[] weapons;
    private int weaponSelected = 0;

    public float projectileSpawnDistance = 1.5f;
    public float projectileSpawnHeight = 0.5f;

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

        if (Input.GetMouseButton(0) && weapons[weaponSelected].cooldown >= (60f / weapons[weaponSelected].fireRateRPM) && Time.timeScale != 0f)
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

            Projectile instantiatedProjectile;

            if (weapons[weaponSelected].projectile == (int)Weapon.ProjectileTypes.Rocket)
            {
                instantiatedProjectile = FindObjectOfType<RocketPool>().Get();
                //Debug.Log("Getting a rocket");
            }
            else
            {
                instantiatedProjectile = FindObjectOfType<ProjectilePool>().Get();
                //Debug.Log("Getting a bullet");
            }
            instantiatedProjectile.transform.position = parentTransform.position + direction * projectileSpawnDistance + Vector3.up * projectileSpawnHeight;
            float yAngle = Vector3.Angle(new Vector3(1, 0, 0), direction);
            if (direction.z >= 0f)
            {
                yAngle *= -1;
            }
            instantiatedProjectile.transform.eulerAngles = new Vector3(0f, yAngle + Random.Range(-weapons[weaponSelected].spreadDegrees / 2f, weapons[weaponSelected].spreadDegrees / 2f), 0f);
            instantiatedProjectile.gameObject.SetActive(true);

            FindObjectOfType<AudioManager>().Play(weapons[weaponSelected].soundName);
            NoiseManager.instance.MakeNoise(transform.position, weapons[weaponSelected].noiseTravelDistance);
        }
    }
}
