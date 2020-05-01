using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();
    void OnObjectSpawn(Vector3 direction);
}

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int PoolSize;
    }

    public Transform PoolContent;

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private GameObject mObjectToSpawn;

    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(PoolContent);
            }

            PoolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject Trigger(string tag, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag: " + tag + " does not exist!");
            return null;
        }

        mObjectToSpawn = PoolDictionary[tag].Dequeue();

        mObjectToSpawn.SetActive(true);
        mObjectToSpawn.transform.position = position;
        mObjectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = mObjectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null) pooledObject.OnObjectSpawn();

        PoolDictionary[tag].Enqueue(mObjectToSpawn);

        return mObjectToSpawn;
    }

    public GameObject Trigger(string tag, Vector3 position, Quaternion rotation, Vector3 direction)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag: " + tag + " does not exist!");
            return null;
        }

        mObjectToSpawn = PoolDictionary[tag].Dequeue();

        mObjectToSpawn.SetActive(true);
        mObjectToSpawn.transform.position = position;
        mObjectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = mObjectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            if (direction != null) pooledObject.OnObjectSpawn(direction);
            else pooledObject.OnObjectSpawn();
        }

        PoolDictionary[tag].Enqueue(mObjectToSpawn);

        return mObjectToSpawn;
    }

    public void DestroyAll()
    {
        foreach (Transform child in PoolContent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
