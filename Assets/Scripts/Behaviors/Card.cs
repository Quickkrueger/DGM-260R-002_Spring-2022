using KillerIguana.CardManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    BaseCard cardData;
    Vector3 pointerOffset;
    Rigidbody _rb;
    Image _img;
    public GameObject dragArrowPrefab;
    private DragArrow dragArrow;

    public CardEvent playCard;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _img = GetComponent<Image>();
        dragArrow = null;
        playCard = new CardEvent();
    }
    public void InitializeCard(BaseCard newCard)
    {
        cardData = newCard;
        _img.material = new Material(_img.material);
        _img.material.SetTexture("CardArt", cardData.graphic);

    }

    public void DragCard()
    {
        Camera cam = Camera.main;
        if (dragArrow != null)
        {
            Vector3 screenSpaceTarget = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane + 0.6f);
            Vector3 worldSpaceTarget = cam.ScreenToWorldPoint(screenSpaceTarget);
            dragArrow.UpdateArrow(worldSpaceTarget);
        }
        else
        {
            dragArrow = Instantiate(dragArrowPrefab).GetComponent<DragArrow>();

            Vector3 screenSpaceOrigin = cam.WorldToScreenPoint(transform.position);
            screenSpaceOrigin.z = cam.nearClipPlane + 0.6f;
            Vector3 worldSpaceOrigin = cam.ScreenToWorldPoint(screenSpaceOrigin);
            
            dragArrow.InitializeArrow(worldSpaceOrigin);
        }
        /* Vector3 pointerPosition = Input.mousePosition;
        if(pointerOffset == Vector3.zero)
        {
            pointerOffset = new Vector3();
            pointerOffset = pointerPosition - Camera.main.WorldToScreenPoint(transform.position);
        }
        else if(pointerOffset != Vector3.zero && Camera.main != null)
        {
            _rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - pointerOffset);
        } */
    }

    public void ReleaseCard()
    {
        dragArrow.DestroyArrow();
        dragArrow = null;
        pointerOffset = Vector3.zero;
        //CardPlayed();
    }

    void CardPlayed()
    {
        playCard.Invoke(this);
    }

    public BaseCard GetCardData()
    {
        return cardData;
    }
}
