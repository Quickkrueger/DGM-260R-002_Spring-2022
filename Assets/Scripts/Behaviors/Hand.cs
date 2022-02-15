using KillerIguana.CardManager;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    public BaseCard[] cards;
    public GameObject[] cardsInHand;
    public GameObject handRoot;
    // Start is called before the first frame update
    public void DrawCard(BaseCard newCard)
    {
        GameObject cardObject = Instantiate(cardPrefab);

    }

    public void DrawCard(Card reusedCard)
    {
        reusedCard.playCard.AddListener(DiscardCard);
    }

    void DiscardCard(Card reusedCard)
    {

    }
}
