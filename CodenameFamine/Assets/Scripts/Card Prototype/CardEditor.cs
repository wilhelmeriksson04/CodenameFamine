using UnityEditor;
using UnityEngine;
using CardPrototype;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawProperty("cardName");
        DrawProperty("cardType");

        SerializedProperty cardTypeProp = serializedObject.FindProperty("cardType");

        Section[] sections = 
        {
        new(Card.CardType.Artifact,   "Artifact",   "damage", "health", "shieldAdd", "damageType"),
        new(Card.CardType.Character,  "Character",  "damage", "health", "damageType"),
        new(Card.CardType.Creature,   "Creature",   "damage", "health", "damageType"),
        new(Card.CardType.Provision,  "Provision",  "healthAdd"),
        new(Card.CardType.Spell,      "Spell",      "damage", "healthAdd", "manaCount", "shieldAdd", "shield", "damageType"),
        new(Card.CardType.Weapon,     "Weapon",     "ammoCount", "damage", "range", "damageType"),
        };

        foreach (var section in sections)
            DrawSection(cardTypeProp, section);

        serializedObject.ApplyModifiedProperties();
    }

    private struct Section
    {
        public Card.CardType type;
        public string label;
        public string[] fields;

        public Section(Card.CardType type, string label, params string[] fields)
        {
            this.type = type;
            this.label = label;
            this.fields = fields;
        }
    }

    private void DrawSection(SerializedProperty cardTypeProp, Section section)
    {
        if (cardTypeProp.enumValueIndex != (int)section.type)
            return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(section.label, EditorStyles.boldLabel);

        foreach (var field in section.fields)
            DrawProperty(field);
    }

    private void DrawProperty(string name)
    {
        SerializedProperty prop = serializedObject.FindProperty(name);
        
        if (prop != null)
            EditorGUILayout.PropertyField(prop);
    }
}