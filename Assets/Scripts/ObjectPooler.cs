using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    public GameObject pipePrefab;
    public int poolSize = 5;

    private List<GameObject> pipePool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        pipePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pipePrefab);
            obj.SetActive(false);
            pipePool.Add(obj);
        }
    }

    public GameObject GetPooledPipe()
    {
        for (int i = 0; i < pipePool.Count; i++)
        {
            if (!pipePool[i].activeInHierarchy)
            {
                return pipePool[i];
            }
        }

        GameObject obj = Instantiate(pipePrefab);
        obj.SetActive(false);
        pipePool.Add(obj);
        return obj;
    }
}