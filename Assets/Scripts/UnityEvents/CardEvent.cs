using UnityEngine;
using UnityEngine.Events;
namespace KillerIguana.CardManager
{
    [System.Serializable]
    public class CardDataEvent : UnityEvent<Transform>
    {

    }
    
    public class CardEvent : UnityEvent<Card>
    {

    }
}