using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private float spaceSize;
    [SerializeField] private float scaleSize;
    [SerializeField] private Transform go;
    private Transform mapHolder;

    private void Start()
    {
        GenerateObjs();
    }
    public void GenerateObjs()
    {
        string goParentName = "ObjectsManager";
        if (transform.Find(goParentName))
        {
           DestroyImmediate(transform.Find(goParentName).gameObject);
        }
        mapHolder = new GameObject(goParentName).transform;
        mapHolder.parent = transform;
        RandomSpawn();
    }

    public void RandomSpawn()
    {
        for(int i = 0; i < count; i++)
        {
            float randomValueX = Random.Range(-spaceSize, spaceSize);
            float randomValueY = Random.Range(-spaceSize, spaceSize);
            float randomValueZ = Random.Range(-spaceSize, spaceSize);
            float randomScale = Random.Range(0.0f, scaleSize);
            Transform obj =Instantiate(go, new Vector3(randomValueX, randomValueY, randomValueZ), Quaternion.identity);
            obj.parent = mapHolder;
            obj.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }

    public int SpawnCount()
    {
        return count;
    }
}
