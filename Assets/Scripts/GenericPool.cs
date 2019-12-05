using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T prefab;

    public static GenericPool<T> Instance { get; private set; }
    private Queue<T> projectiles = new Queue<T>();

    private void Awake()
    {
        Instance = this;
    }

    public T Get ()
    {
        if (projectiles.Count == 0)
        {
            AddProjectiles(1);
        }
        return projectiles.Dequeue();
    }

    public void ReturnToPool (T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        projectiles.Enqueue(objectToReturn);
    }

    private void AddProjectiles (int count)
    {
        var newProjectile = GameObject.Instantiate(prefab);
        newProjectile.gameObject.SetActive(false);
        projectiles.Enqueue(newProjectile);
    }
}
