using System;
using KillerIguana.CardManager;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    List<Card> cards;
    public GameObject handRoot;

    public IntData baseHandSize;

    private void Awake()
    {
        cards = new List<Card>();
    }

    // Start is called before the first frame update
    public void DrawCard(BaseCard newCard)
    {
        GameObject cardObject = Instantiate(cardPrefab);
        Card cardScript = cardObject.GetComponent<Card>();
        cardScript.InitializeCard(newCard);
        cards.Add(cardScript);
        cardScript.transform.SetParent(handRoot.transform);
        cardScript.transform.localPosition = Vector3.zero;
        cardScript.transform.localRotation = Quaternion.identity;

    }

    public void DrawCard(Card reusedCard)
    {
        reusedCard.playCard.AddListener(DiscardCard);
        cards.Add(reusedCard);
        reusedCard.transform.parent = handRoot.transform;
        reusedCard.transform.SetParent(handRoot.transform);
        reusedCard.transform.localPosition = Vector3.zero;
        reusedCard.transform.localRotation = Quaternion.identity;

        
    }

    public Card FindCardFromData(BaseCard cardData)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetCardData() == cardData)
            {
                return cards[i];
            }
        }

        return null;
    }

    void DiscardCard(BaseCard usedCard)
    {
        Card cardToRemove = FindCardFromData(usedCard);
        
        if (cardToRemove != null)
        {
            cards.Remove(cardToRemove);
            cardToRemove.transform.parent = null;
        }

    }
}
