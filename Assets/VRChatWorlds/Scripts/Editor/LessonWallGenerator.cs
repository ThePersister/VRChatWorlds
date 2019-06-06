#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LessonWallGenerator : EditorWindow {

    [MenuItem("Lesson Wall/Generator")]
    static void OpenWindow()
    {
        // Get existing open window or if none, make a new one:
        LessonWallGenerator window = (LessonWallGenerator)EditorWindow.GetWindow(typeof(LessonWallGenerator));
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Generate Wall"))
        {
            SpawnWall();
        }
    }

    private void SpawnWall()
    {
        JsonToLessonConverter.Instance.GenerateLessonWall();
    }
}

#endif