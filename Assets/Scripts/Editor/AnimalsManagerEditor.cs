using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimalsManager))]
public class AnimalsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AnimalsManager animalsManager = (AnimalsManager)target;
        if (GUILayout.Button("New Prey"))
        {
            animalsManager.NewPrey();
        }
        if (GUILayout.Button("New Predator"))
        {
            animalsManager.NewPredator();
        }

    }
}
