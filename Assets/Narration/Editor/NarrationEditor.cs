﻿// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Narration))]
public class NarrationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Cycle Text"))
            (target as Narration).CycleText_EditorOnly();

        DrawDefaultInspector();
    }
}
