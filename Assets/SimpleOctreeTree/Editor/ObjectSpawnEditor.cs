using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SpawnObjects))]
public class ObjectSpawnEditor : Editor
{
    CreateOctree octree;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Map"))
        {
            SpawnObjects so = target as SpawnObjects;
            so.GenerateObjs();
            if (octree != null)
            {
                octree.UpdateOctree();
            }
        }
    }

    private void OnEnable()
    {
        octree = FindObjectOfType<CreateOctree>();
    }
}


