using System;
using KillerIguana.CardManager;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    List<Card> cards;
    private RectTransform _handRect;
    public GameObject handRoot;
    private int currentHandSize = 0;

    public IntData baseHandSize;

    private void Awake()
    {
        cards = new List<Card>();
        _handRect = GetComponent<RectTransform>();
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
        currentHandSize++;

        SpaceOutCards();
    }

    public void DrawCard(Card reusedCard)
    {
        reusedCard.playCard.AddListener(DiscardCard);
        cards.Add(reusedCard);
        reusedCard.transform.parent = handRoot.transform;
        reusedCard.transform.SetParent(handRoot.transform);
        reusedCard.transform.localPosition = Vector3.zero;
        reusedCard.transform.localRotation = Quaternion.identity;
        currentHandSize++;

        SpaceOutCards();
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

    public void SpaceOutCards()
    {
        float spacing = (float)Screen.width / (float)(currentHandSize * 1.5f);
        
        for (int i = 0; i < cards.Count; i++)
        {
            float xOffset = (spacing * (i + currentHandSize / 2));

            Vector3 xOffsetWorld = Camera.main.ScreenToWorldPoint(new Vector3(xOffset, 0, 1));
            
            cards[i].InitializeMove(xOffsetWorld.x);
        }
    }

    private void DiscardCard(Card usedCard)
    {
        if (VerifyCard(usedCard))
        {
            cards.Remove(usedCard);
            usedCard.transform.parent = null;
            currentHandSize--;
        }

    }
}
