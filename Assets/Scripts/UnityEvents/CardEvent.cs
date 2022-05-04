using UnityEngine;
using UnityEngine.Events;
namespace KillerIguana.CardManager
{
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform>
    {

    }
    [System.Serializable]
    public class CardDataEvent : UnityEvent<BaseCard>
    {

    }
    [System.Serializable]
    public class CardEvent : UnityEvent<Card>
    {

    }
}