using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    public OctreeNode rootNode;
    Bounds spaceBounds;
    public Octree(GameObject[] gos,float minSize,int maxDeep )
    {
        spaceBounds = new Bounds();

        foreach(GameObject go in gos)
        {
            spaceBounds.Encapsulate(go.GetComponent<Collider>().bounds);
        }

        float maxLength = Mathf.Max(new float[]{spaceBounds.extents.x,spaceBounds.extents.y,spaceBounds.extents.z });
        Vector3 boundsSize = new Vector3(maxLength, maxLength, maxLength);
        spaceBounds.SetMinMax(spaceBounds.center - boundsSize, spaceBounds.center + boundsSize);
        rootNode = new OctreeNode(maxDeep, minSize, 1,spaceBounds);
        AddObjs(gos);
    }

    public void AddObjs(GameObject[] gos)
    {
        foreach(GameObject go in gos)
        {
            rootNode.AddObj(go);
        }
    }

    public void Draw()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spaceBounds.center, spaceBounds.size);
    }
}
