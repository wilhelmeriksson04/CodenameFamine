using UnityEngine;
using System.Collections.Generic;

namespace CardPrototype
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public CardType cardType;
        public DamageType damageType;

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