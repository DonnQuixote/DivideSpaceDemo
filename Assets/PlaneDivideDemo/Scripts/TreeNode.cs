using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : INode
{
    public Bounds bound { get; set; }

    private int depth;
    private Tree belongTree;
    private TreeNode[] childList;
    //每个节点用队列来存储物体的信息
    private List<GameObjectData> objDataList;
    private bool isInside = false;

    private int leftIndex;
    private int rightIndex;
    public TreeNode(Bounds bound, int depth, Tree belongTree)
    {
        this.belongTree = belongTree;
        this.bound = bound;
        this.depth = depth;
        objDataList = new List<GameObjectData>();
    }

    //public TreeNode(Bounds bound, int depth, Tree belongTree,int _leftIndex)
    //{
    //    this.belongTree = belongTree;
    //    this.bound = bound;
    //    this.depth = depth;
    //    leftIndex = _leftIndex;
    //    rightIndex = leftIndex;
    //}

    public void InsertObjData(GameObjectData objData)
    {
        TreeNode node = null;
        bool bChild = false;

        if (depth < belongTree.maxDepth && childList == null)
        {
            CreateChild();
        }
        if (childList != null)
        {
            for (int i = 0; i < childList.Length; ++i)
            {
                TreeNode item = childList[i];
                if (item.bound.Intersects(objData.GetBounds()))
                {
                    if (node != null)
                    {
                        bChild = false;
                        break;
                    }
                    node = item;
                    bChild = true;
                }
            }
        }
        //物体只占用一个空间划分区块时则放入子节点里
        if (bChild)
        {
            node.InsertObjData(objData);
        }
        //物体占用多个空间划分区块放入父节点
        else
        {
            ////在Tree中使用一整个大的List来保存数据
            //belongTree.gameObjectDatasList.Add(objData);
            //rightIndex++;
            //
            objDataList.Add(objData);
        }
    }

    //摄像机在该节点里
    public void Inside(Camera camera)
    {
        //刷新子节点
        if (childList != null)
        {
            for (int i = 0; i < childList.Length; ++i)
            {
                if (childList[i].bound.CheckBoundsIsInCamera(camera, belongTree.viewRatio))
                {
                    childList[i].Inside(camera);
                }
                else
                {
                    childList[i].Outside(camera);
                }
            }
        }

        if (isInside)
            return;
        isInside = true;
        //for(int i = leftIndex; i <= rightIndex; i++)
        //{
        //    GameObjectsManager.Instance.LoadAsync(belongTree.gameObjectDatasList[i]);
        //}

        for (int i = 0; i < objDataList.Count; ++i)
        {
            GameObjectsManager.Instance.LoadAsync(objDataList[i]);
        }

    }

    //摄像机不在该节点里
    public void Outside(Camera camera)
    {
        //刷新子节点
        if (childList != null)
        {
            for (int i = 0; i < childList.Length; ++i)
            {
                childList[i].Outside(camera);
            }
        }
        if (isInside == false)
            return;
        isInside = false;
        //for (int i = leftIndex; i <= rightIndex; i++)
        //{
        //    GameObjectsManager.Instance.LoadAsync(belongTree.gameObjectDatasList[i]);
        //}

        for (int i = 0; i < objDataList.Count; i++)
        {
            GameObjectsManager.Instance.UnLoad(objDataList[i].uid);
        }
    }

    private void CreateChild()
    {
        childList = new TreeNode[belongTree.maxChildCount];
        int index = 0;
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                Vector3 centerOffset = new Vector3(bound.size.x / 4 * i, 0, bound.size.z / 4 * j);
                Vector3 cSize = new Vector3(bound.size.x / 2, bound.size.y, bound.size.z / 2);
                Bounds cBound = new Bounds(bound.center + centerOffset, cSize);
                childList[index++] = new TreeNode(cBound, depth + 1, belongTree);
                //childList[index++] = new TreeNode(cBound, depth + 1, belongTree,rightIndex+1);
            }
        }
    }

    public void DrawBound()
    {
        if (isInside)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(bound.center, bound.size);
        }

        //else if(rightIndex - leftIndex != 0)
        //{

        //}
        else if (objDataList.Count != 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bound.center, bound.size);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(bound.center, bound.size);
        }

        if (childList != null)
        {
            for (int i = 0; i < childList.Length; ++i)
            {
                childList[i].DrawBound();
            }
        }
    }
}
