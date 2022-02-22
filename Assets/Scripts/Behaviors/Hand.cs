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
        cardScript.playCard.AddListener(DiscardCard);

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

    public bool VerifyCard(Card cardToVerify)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == cardToVerify)
            {
                return true;
            }
        }
        
        return false;
    }

    private void DiscardCard(Card usedCard)
    {
        if (VerifyCard(usedCard) != false)
        {
            cards.Remove(usedCard);
            usedCard.transform.parent = null;
        }

    }
}
