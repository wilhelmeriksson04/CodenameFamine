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
        if (target == null)
            return;

        DrawDefaultInspector();

        DeckManager deckManager = (DeckManager)target;

        if (GUILayout.Button("+1 Card"))
        {
            HandManager handManager = FindObjectOfType<HandManager>();

            if (handManager != null)
                deckManager.DrawCard(handManager);
            else
                Debug.LogWarning("No HandManager found");
        }
    }
}
#endif