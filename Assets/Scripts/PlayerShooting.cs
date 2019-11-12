using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - new Vector2(parentTransform.position.x, parentTransform.position.y);
            direction.Normalize();

            var instantiatedProjectile = ProjectilePool.Instance.Get();
            instantiatedProjectile.transform.position = parentTransform.position +(Vector3)direction;
            float zAngle = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(new Vector2(1, 0), direction));
            if (direction.y < 0f)
            {
                zAngle *= -1;
            }
            instantiatedProjectile.transform.eulerAngles = new Vector3(0f, 0f, zAngle);
            instantiatedProjectile.gameObject.SetActive(true);
        }
    }
}
