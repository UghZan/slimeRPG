using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] GameObject instance;
    [SerializeField] bool collectionChecks = true;
    [SerializeField] int startPoolSize = 10;
    [SerializeField] int maxPoolSize = 100;

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Start()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, startPoolSize, maxPoolSize);
    }

    GameObject CreatePooledItem()
    {
        return Instantiate(instance, transform);
    }

    void OnReturnedToPool(GameObject gameObject)
    {
        gameObject.transform.SetParent(transform); //could be a bit slow, not sure. it's not important for the pool functionality, so safe to remove
        gameObject.transform.position = Vector3.zero; //to prevent instantaneous but noticeable warping of some pooled objects from their previous positions
        gameObject.SetActive(false);
    }

    void OnTakeFromPool(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    void OnDestroyPoolObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
