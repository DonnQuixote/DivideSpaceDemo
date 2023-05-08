using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOctree : MonoBehaviour
{
    [SerializeField] GameObject[] worldSpaceObjects;
    [SerializeField] int maxDeep;
    [SerializeField] float minSize;
    [SerializeField] SpawnObjects so;
    Octree octree;

    private void OnEnable()
    {
        so = FindObjectOfType<SpawnObjects>();
    }
    private void Start()
    {
        UpdateOctree();
    }

    public void UpdateOctree()
    {
        if (octree != null) octree = null;
        worldSpaceObjects = null;
        StartCoroutine(WaitForSpawn(so.SpawnCount()));
        
        if (worldSpaceObjects != null)
        {
        octree = new Octree(worldSpaceObjects, minSize, maxDeep);
        }
    }

    IEnumerator WaitForSpawn(int num)
    {
        worldSpaceObjects = GameObject.FindGameObjectsWithTag("Cube");
        yield return new WaitUntil(() => worldSpaceObjects.Length == num);
    }


    private void OnDrawGizmos()
    {
        if (octree != null)
        {
            if (Application.isPlaying)
            {
                octree.rootNode.Draw();
                octree.Draw();
            }
        }
    }
}
