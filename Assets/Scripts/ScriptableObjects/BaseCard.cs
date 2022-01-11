using UnityEngine;

namespace KillerIguana.CardManager
{

    public class BaseCard : ScriptableObject
    {
        public int cost;
        public Texture graphic;
        public CardType cardType;
        public CardEffect cardEffect;

    }

    public enum CardType
    {
        Turret,
        Upgrade,
        Utility
    };
}
