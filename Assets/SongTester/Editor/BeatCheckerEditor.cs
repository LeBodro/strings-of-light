// ©2020 Maxime Boudreault. All Rights Reserved.

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BeatChecker))]
public class BeatCheckerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (Application.isPlaying && GUILayout.Button("Replace Timestamps"))
        {
            (target as BeatChecker).Replace();
        }
        DrawDefaultInspector();
    }
}
