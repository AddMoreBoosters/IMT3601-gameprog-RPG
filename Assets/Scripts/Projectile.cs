using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float maxLifetime = 5f;
    private float lifetime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        lifetime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1f, 0f, 0f) * speed * Time.deltaTime);
        lifetime += Time.deltaTime;
        if (lifetime > maxLifetime)
        {
            //  Deactivate object and return it to the pool
            ProjectilePool.Instance.ReturnToPool(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //  Deactivate object and return it to the pool
        ProjectilePool.Instance.ReturnToPool(this);
    }
}
