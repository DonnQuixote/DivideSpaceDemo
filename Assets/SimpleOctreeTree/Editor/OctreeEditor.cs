using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CreateOctree))]
public class OctreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate Octree"))
        {
            CreateOctree co = target as CreateOctree;
            co.UpdateOctree();
        }
    }
}
