﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;

    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion 

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);

        }
    }

    //Spawning GameObjects from pool
    public GameObject SpawnFromPool(string tag, Vector3 position) 
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool" + tag + "does not exist");
            return null;
        }
        GameObject spawnObj = poolDictionary[tag].Dequeue();
        spawnObj.SetActive(true);
        spawnObj.transform.position = position;

        poolDictionary[tag].Enqueue(spawnObj);

        return spawnObj;
    }
}
