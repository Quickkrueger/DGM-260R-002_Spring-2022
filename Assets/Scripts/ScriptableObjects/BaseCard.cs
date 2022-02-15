using UnityEngine;

namespace KillerIguana.CardManager
{

    [CreateAssetMenu(fileName = "BaseCard", menuName = "SO/Objects/BaseCard", order = 1)]
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
