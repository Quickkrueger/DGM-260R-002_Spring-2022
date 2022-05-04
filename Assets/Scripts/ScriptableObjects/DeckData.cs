using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillerIguana.CardManager
{
    [CreateAssetMenu(fileName = "DeckData", menuName = "SO/Data/DeckData", order = 1)]
    public class DeckData : ScriptableObject
    {
        public List<BaseCard> deckCards;
        private List<BaseCard> handCards;
        private List<BaseCard> discardCards;

        #region Get Card
        public BaseCard DeckCardFromIndex(int index)
        {
            return deckCards[index];
        }

        public BaseCard HandCardFromIndex(int index)
        {
            return handCards[index];
        }

        public BaseCard DiscardCardFromIndex(int index)
        {
            return discardCards[index];
        }

        #endregion

        #region Get Count

        public int GetDeckCount()
        {
            return deckCards.Count;
        }

        public int GetHandCount()
        {
            return handCards.Count;
        }

        public int GetDiscardCount()
        {
            return discardCards.Count;
        }

        #endregion

        #region Get Index

        #endregion

        #region Card Transfers
        public bool DeckToHand(int index = 0)
        {
            if(index < deckCards.Count)
            {
                handCards.Add(deckCards[index]);
                deckCards.RemoveAt(index);

                return true;
            }
            return false;
        }

        public bool DiscardToHand(int index = 0)
        {
            if (index < discardCards.Count)
            {
                handCards.Add(discardCards[index]);
                discardCards.RemoveAt(index);

                return true;
            }
            return false;
        }

        public bool HandToDiscard(int index = 0)
        {
            if (index < handCards.Count)
            {
                discardCards.Add(handCards[index]);
                handCards.RemoveAt(index);

                return true;
            }
            return false;
        }

        public bool DeckToDiscard(int index = 0)
        {
            if (index < deckCards.Count)
            {
                discardCards.Add(deckCards[index]);
                deckCards.RemoveAt(index);

                return true;
            }
            return false;
        }

        public bool HandToDeck(int index, int deckLocation = 0)
        {
            if(index < handCards.Count && deckLocation <= deckCards.Count)
            {
                deckCards.Insert(deckLocation, handCards[index]);
                handCards.RemoveAt(index);
                return true;
            }

            return false;
        }

        public bool DiscardToDeck(int index, int deckLocation = 0)
        {
            if (index < discardCards.Count && deckLocation <= deckCards.Count)
            {
                deckCards.Insert(deckLocation, discardCards[index]);
                discardCards.RemoveAt(index);
                return true;
            }

            return false;
        }
        #endregion


        public void DeckShuffle()
        {

            Shuffle();

        }

        public void DiscardShuffle()
        {

            for (int i = discardCards.Count; i >= 0; i++)
            {
                DiscardToDeck(i);
            }

            Shuffle();

        }

        public void FullShuffle()
        {
            for(int i = handCards.Count; i >= 0; i--)
            {
                HandToDeck(i);
            }

            for (int i = discardCards.Count; i >= 0; i--)
            {
                DiscardToDeck(i);
            }
             
            Shuffle();
        }

        private void Shuffle()
        {
            for(int i = 0; i < 7; i++)
            {
                for(int j = 0; j < deckCards.Count; j++)
                {
                    int newLocation = Random.Range(0, deckCards.Count);

                    deckCards.Insert(newLocation, deckCards[j]);

                    if(newLocation <= j)
                    {
                        deckCards.RemoveAt(j + 1);
                    }
                    else
                    {
                        deckCards.RemoveAt(j);
                    }

                }
            }
        }
    }
}
