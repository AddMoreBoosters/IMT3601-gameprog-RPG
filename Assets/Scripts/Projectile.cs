using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float maxLifetime = 5f;
    private float elapsedLifetime;

    [SerializeField]
    private int damage = 1;

    private void OnEnable()
    {
        elapsedLifetime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1f, 0f, 0f) * speed * Time.deltaTime);
        elapsedLifetime += Time.deltaTime;
        if (elapsedLifetime > maxLifetime)
        {
            //  Deactivate projectile and return it to the pool
            ProjectilePool.Instance.ReturnToPool(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health destructibleTarget = other.gameObject.GetComponent<Health>();
        if (destructibleTarget != null)
        {
            destructibleTarget.ModifyHealth(-damage);
        }

        //  Deactivate projectile and return it to the pool
        ProjectilePool.Instance.ReturnToPool(this);
    }
}
