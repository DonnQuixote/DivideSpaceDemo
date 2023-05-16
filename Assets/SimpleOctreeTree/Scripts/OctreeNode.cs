using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctreeNode
{
    int maxDeep;
    float minSize;
    int curDeep;
    Bounds spaceBounds;
    OctreeNode[] childrenNodes;
    Bounds[] childrenBounds;
    public  OctreeNode(int _maxDeep,float _minSize,int _curDeep,Bounds _spaceBounds)
    {
        maxDeep = _maxDeep;
        minSize = _minSize;
        curDeep = _curDeep;
        spaceBounds = _spaceBounds;
        CreateChildrenBounds(_spaceBounds);
    }

    public void CreateChildrenBounds(Bounds _spaceBounds)
    {
        Vector3 center = _spaceBounds.center;
        float childLen = _spaceBounds.size.y / 2;
        float quarter = _spaceBounds.size.y / 4;
        Vector3 childSize = new Vector3(childLen, childLen, childLen);
        childrenBounds = new Bounds[8];
        int count = 0;
        for(int x = -1; x <= 1; x += 2)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                for (int z = -1; z <= 1; z += 2)
                {
                    childrenBounds[count++] = new Bounds(center + new Vector3(x*quarter,y*quarter,z*quarter),childSize);
                }
            }
        }
    }

    public void DivideAndAdd(GameObject go)
    {
        if (spaceBounds.size.y < minSize || curDeep > maxDeep) return;

        if (childrenNodes == null) childrenNodes = new OctreeNode[8];

        bool ifDivided = false;
        for(int i = 0; i < 8; i++)
        {
            if (childrenNodes[i] == null) 
                childrenNodes[i] = new OctreeNode(maxDeep, minSize, curDeep + 1, childrenBounds[i]);

            if (childrenBounds[i].Intersects(go.GetComponent<Collider>().bounds))
            {
                ifDivided = true;
                childrenNodes[i].DivideAndAdd(go);
            }
        }

        if (!ifDivided) childrenNodes = null;
    }

    public void AddObj(GameObject go)
    {
        DivideAndAdd(go);
    }

    public void Draw()
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(spaceBounds.center, spaceBounds.size);
        if (childrenNodes != null)
        {
            for (int i = 0; i < 8; i++)
            {
                if (childrenNodes[i] != null)
                {
                    childrenNodes[i].Draw();
                }
            }
        }
    }
}
