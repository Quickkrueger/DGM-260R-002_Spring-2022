using System;
using KillerIguana.CardManager;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    public DeckData handData;
    public GameObject handRoot;
    public IntData baseHandSize;
    public CardDataEvent cardInfoEvent;

    List<Card> cards;
    private RectTransform _handRect;
    private int currentHandSize = 0;

    

    private void Awake()
    {
        cards = new List<Card>();
        _handRect = GetComponent<RectTransform>();
        handData.FullShuffle();
    }

    // Start is called before the first frame update
    public void DrawCard(Transform deckTransform)
    {
        GameObject cardObject = Instantiate(cardPrefab);
        BaseCard newCard = handData.HandCardFromIndex(handData.GetHandCount() - 1);

        Card cardScript = cardObject.GetComponent<Card>();
        cardScript.InitializeCard(newCard);
        cards.Add(cardScript);
        cardScript.transform.SetParent(handRoot.transform);
        cardScript.transform.position = deckTransform.position;
        cardScript.transform.localRotation = Quaternion.identity;
        cardScript.transform.localScale = Vector3.one * 0.4f;
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
        float spacing = (float)Screen.width / (float)(currentHandSize + 1);
        
        for (int i = 0; i < cards.Count; i++)
        {
            float xOffset = (spacing * (float)(i + 1));

            Vector3 xOffsetWorld = Camera.main.ScreenToWorldPoint(new Vector3(xOffset, 0, 2));
            
            cards[i].InitializeMove(xOffsetWorld.x);
        }
    }

    private void DiscardCard(Card usedCard)
    {
        if (VerifyCard(usedCard))
        {
            handData.HandToDiscard(cards.IndexOf(usedCard));
            cards.Remove(usedCard);
            usedCard.DestroySelf();
            currentHandSize--;
        }

    }

    public void ClearHand()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].playCard.Invoke(cards[i]);
        }
    }

    public void SendDataToPreview(BaseCard cardData)
    {
        cardInfoEvent.Invoke(cardData);
    }


}
