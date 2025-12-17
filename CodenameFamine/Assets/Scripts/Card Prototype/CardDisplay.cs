using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using CardPrototype;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    [Space]
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    [Space]
    public Image cardImage;
    public Image[] cardTypeImages;

    public enum CardType
    {
        Artifact, //index 0
        Character, //index 1
        Creature, //index 2
        Provision, //index 3
        Spell, //index 4
        Weapon //index 5
    }

    //color for each card type
    private readonly Color[] cardColors =
    {
        /*new Color(0.788f, 0.635f, 0.302f), //artifact - antique gold
        new Color(0.227f, 0.373f, 0.659f), //character - navy blue
        new Color(0.549f, 0.114f, 0.094f), //creature - bloodsteel red
        new Color(0.761f, 0.706f, 0.604f), //provision - parchment linen
        new Color(0.482f, 0.294f, 0.718f), //spell - arcane violet
        new Color(0.420f, 0.439f, 0.471f) //weapon - cold iron*/

        new Color(0.94f, 0.91f, 0.80f), //artifact – antique gold
        new Color(0.85f, 0.89f, 0.96f), //character – navy blue
        new Color(0.95f, 0.84f, 0.83f), //creature – bloodsteel red
        new Color(0.93f, 0.91f, 0.86f), //provision – parchment linen
        new Color(0.89f, 0.84f, 0.96f), //spell – arcane violet
        new Color(0.88f, 0.89f, 0.92f)  //weapon – cold iron
    };

    private void Start() => UpdateCardDisplay();

    public void UpdateCardDisplay()
    {
        int typeIndex = (int)cardData.cardType;

        for (int i = 0; i < cardTypeImages.Length; i++)
            cardTypeImages[i].enabled = i == typeIndex;

        cardImage.color = cardColors[typeIndex];

        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = cardData.damage.ToString();
    }
}