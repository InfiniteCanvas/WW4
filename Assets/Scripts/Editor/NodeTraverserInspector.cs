using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WW4.EventSystem;

[CustomEditor(typeof(NodeTraverser))]
public class NodeTraverserInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var nt = (NodeTraverser)target;
        EditorGUILayout.LabelField("Current Node: ",  $"{nt.CurrentNode?.name ?? "None"}");
        EditorUtility.SetDirty(target);
    }
}