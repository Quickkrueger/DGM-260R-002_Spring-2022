using System.Collections;
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
    private RectTransform _rectTransform;
    private DragArrow dragArrow;
    private Coroutine cardMoveRoutine;

    public GameObject dragArrowPrefab;
    public CardEvent playCard;
    public float moveSpeed;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _img = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        dragArrow = null;
        playCard = new CardEvent();
    }
    public void InitializeCard(BaseCard newCard)
    {
        cardData = newCard;
        _img.material = new Material(_img.material);
        _img.material.SetTexture("CardArt", cardData.graphic);
    }

    public void InitializeMove(float xOffset)
    {
        Vector3 startLocation = transform.position;
        Vector3 destination = transform.parent.position + (Vector3.right * xOffset);

        //Vector3 destination = _rectTransform.TransformPoint(localDestination);
        //Debug.Log("Local Destination: " + localDestination);
        //Debug.Log("Destination: " + destination);
        
        WaitForSeconds moveDelay = new WaitForSeconds(0.011f);
        
        if(cardMoveRoutine != null)
        {
            StopCoroutine(cardMoveRoutine);
        }

        cardMoveRoutine = StartCoroutine(MoveToNewPosition(moveDelay, destination, startLocation, 1));
    }

    public IEnumerator MoveToNewPosition(WaitForSeconds moveDelay, Vector3 destination, Vector3 startLocation, float interval)
    {
        float moveTo = destination.x - startLocation.x;
        
        if(moveTo > 0)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.right * Mathf.Max(moveTo, 0.5f));
        }
        else if(moveTo < 0)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.right * Mathf.Min(moveTo, -0.5f));
        }

        yield return moveDelay;

        startLocation = _rectTransform.position;

        if (!ApproximatelyVector3(Camera.main.WorldToScreenPoint(destination), Camera.main.WorldToScreenPoint(_rectTransform.position)))
        {
            cardMoveRoutine = StartCoroutine(MoveToNewPosition(moveDelay, destination, startLocation, interval++));
        }
    }

    public bool ApproximatelyVector3(Vector3 destination, Vector3 currentLocation)
    {
        if (Mathf.Approximately(Mathf.Round(destination.x), Mathf.Round(currentLocation.x)))
        {
            return true;
        }

        return false;
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
