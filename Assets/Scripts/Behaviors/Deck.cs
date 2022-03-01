using System.Collections.Generic;
using KillerIguana.CardManager;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<BaseCard> cards;
    public IntData handLimit;

    public CardDataEvent DrawCardEvent;

    private void Start()
    {
        DrawHand();
    }

    public void DrawHand()
    {
        for (int i = 0; i < handLimit.num; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        DrawCardEvent.Invoke(cards[0]);
        BaseCard temp = cards[0];
        cards.RemoveAt(0);
    }



}
