using System.Collections.Generic;
using KillerIguana.CardManager;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;

public class Deck : MonoBehaviour
{
    public List<BaseCard> cards;
    public IntData handLimit;

    public CardDataEvent DrawCardEvent;

    private void Start()
    {
        Drawhand();
    }

    public void Drawhand()
    {
        for (int i = 0; i < handLimit.num; i++)
        {
            DrawCardEvent.Invoke(cards[0]);
            BaseCard temp = cards[0];
            cards.RemoveAt(0);
        }
    }

    public void DrawCard()
    {
        DrawCardEvent.Invoke(cards[0]);
        BaseCard temp = cards[0];
        cards.RemoveAt(0);
    }



}
