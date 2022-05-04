using System.Collections.Generic;
using KillerIguana.CardManager;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public DeckData deckData;
    public IntData handLimit;

    public TransformEvent DrawCardEvent;

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
        if (deckData.GetDeckCount() > 0)
        {
            deckData.DeckToHand(0);
            DrawCardEvent.Invoke(transform);
        }
        
    }



}
