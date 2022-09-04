using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LegHandler))]
public class SpiderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LegHandler legHandler = (LegHandler) target;
        if(GUILayout.Button("Redraw Legs"))
        {
            legHandler.PositionKnees();
        }
    }
}
