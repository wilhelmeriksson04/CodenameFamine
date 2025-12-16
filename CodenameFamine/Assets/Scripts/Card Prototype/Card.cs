using UnityEngine;
using System.Collections.Generic;

namespace CardPrototype
{
#if UNITY_EDITOR
    using UnityEditor;
    using CardPrototype;

    [CustomEditor(typeof(Card))]
    public class CardEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //always visible
            DrawProperty("cardName");
            DrawProperty("cardType");

            //get cardType list
            SerializedProperty cardTypeProp = serializedObject.FindProperty("cardType");
            bool isArtifact = HasType(cardTypeProp, Card.CardType.Artifact);
            bool isCharacter = HasType(cardTypeProp, Card.CardType.Character);
            bool isCreature = HasType(cardTypeProp, Card.CardType.Creature);
            bool isProvision = HasType(cardTypeProp, Card.CardType.Provision);
            bool isSpell = HasType(cardTypeProp, Card.CardType.Spell);
            bool isWeapon = HasType(cardTypeProp, Card.CardType.Weapon);
            bool isAnyType = isArtifact || isCharacter || isCreature || isProvision || isSpell || isWeapon;

            //conditional fields
            if (isArtifact)
            {
                EditorGUILayout.Space(); EditorGUILayout.LabelField("Artifact", EditorStyles.boldLabel);
                DrawProperty("damage");
                DrawProperty("health");
                DrawProperty("shieldAdd");
                DrawProperty("damageType");
            }

            if (isCharacter)
            {
                EditorGUILayout.Space(); EditorGUILayout.LabelField("Character", EditorStyles.boldLabel);
                DrawProperty("damage");
                DrawProperty("health");
                DrawProperty("damageType");
            }

            if (isCreature)
            {
                EditorGUILayout.Space(); EditorGUILayout.LabelField("Creature", EditorStyles.boldLabel);
                DrawProperty("ammoCount");
                DrawProperty("damage");
                DrawProperty("damageType");
            }

            if (isProvision)
            {
                EditorGUILayout.Space(); EditorGUILayout.LabelField("Provision", EditorStyles.boldLabel);
                DrawProperty("healthAdd");
            }

            if (isSpell)
            {
                EditorGUILayout.Space(); EditorGUILayout.LabelField("Spell", EditorStyles.boldLabel);
                DrawProperty("damage");
                DrawProperty("healthAdd");
                DrawProperty("manaCount");
                DrawProperty("shieldAdd");
                DrawProperty("shield");
                DrawProperty("damageType");
            }

            if (isWeapon)
            {
                EditorGUILayout.Space(); EditorGUILayout.LabelField("Weapon", EditorStyles.boldLabel);
                DrawProperty("ammoCount");
                DrawProperty("damage");
                DrawProperty("range");
                DrawProperty("damageType");
            }

            if (isAnyType)
                EditorGUILayout.HelpBox("You can assign multiple types to a card. The relevant fields will be shown above.", MessageType.Info);
            else
                EditorGUILayout.HelpBox("Please assign at least one Card Type to see relevant fields.", MessageType.Warning);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawProperty(string name)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
        }

        private bool HasType(SerializedProperty listProperty, params Card.CardType[] types)
        {
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                var element = (Card.CardType)listProperty.GetArrayElementAtIndex(i).enumValueIndex;

                foreach (var type in types)
                {
                    if (element == type)
                        return true;
                }
            }

            return false;
        }
    }
#endif

    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public List<DamageType> damageType;
        public float health;
        public float healthAdd;
        public float damage;
        public float range;
        public int shieldAdd;
        public int shield;
        public int ammoCount;
        public int manaCount;

        public enum CardType
        {
            Artifact,
            Character,
            Creature,
            Provision,
            Spell,
            Weapon
        }

        public enum DamageType
        {
            Bleed,
            Magical,
            Physical,
            Poison,
            Ranged
        }
    }
}