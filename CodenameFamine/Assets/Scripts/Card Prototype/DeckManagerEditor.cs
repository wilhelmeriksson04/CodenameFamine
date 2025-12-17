using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(DeckManager))]
public class DeckManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DeckManager deckManager = (DeckManager)target;
        
        if (GUILayout.Button("Draw Next Card"))
        {
            HandManager handManager = FindObjectOfType<HandManager>();
            
            if (handManager != null)
                deckManager.DrawCard(handManager);
            else
                Debug.LogWarning("No HandManager Found");
        }
    }
}
#endif