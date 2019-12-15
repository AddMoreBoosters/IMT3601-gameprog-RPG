using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T prefab;
    private Queue<T> objects = new Queue<T>();

    public T Get ()
    {
        if (objects.Count == 0)
        {
            AddObjects(1);
        }
        return objects.Dequeue();
    }

    public void ReturnToPool (T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    private void AddObjects (int count)
    {
        var newProjectile = GameObject.Instantiate(prefab);
        newProjectile.gameObject.SetActive(false);
        objects.Enqueue(newProjectile);
    }
}
