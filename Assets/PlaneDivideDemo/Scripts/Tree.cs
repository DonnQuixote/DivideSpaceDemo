using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : INode
{
    public Bounds bound { get; set; }
    private TreeNode root;
    public int maxDepth { get; }
    public int maxChildCount { get; }
    public float viewRatio = 1;
    //public List<GameObjectData> gameObjectDatasList;
    public Tree(Bounds bound)
    {
        this.bound = bound;
        this.maxDepth = 5;
        this.maxChildCount = 4;

        root = new TreeNode(bound, 0, this);
        //root = new TreeNode(bound, 0, this,0);
    }

    public void InsertObjData(GameObjectData obj)
    {
        root.InsertObjData(obj);
    }

    public void Inside(Camera camera)
    {
        root.Inside(camera);
    }

    public void Outside(Camera camera)
    {
        root.Outside(camera);
    }

    public void DrawBound()
    {
        root.DrawBound();
    }
}
