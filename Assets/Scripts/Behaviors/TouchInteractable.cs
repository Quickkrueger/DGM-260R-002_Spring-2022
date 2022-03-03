using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchInteractable : MonoBehaviour
{

    WaitForSecondsRealtime draggingCheckDelay;
    WaitForFixedUpdate dragUpdate;
    bool dragging = false;
    bool mouseIsDown = false;
    public UnityEvent mouseDownEvent;
    public UnityEvent mouseUpEvent;
    public UnityEvent mouseDragEvent;
    private EventTrigger eventTrigger;


    public void Start()
    {
        eventTrigger = gameObject.AddComponent<EventTrigger>();
        
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => MouseDownCaller((PointerEventData) data));
        eventTrigger.triggers.Add(entry);
        
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => MouseUpTrigger((PointerEventData)data));
        eventTrigger.triggers.Add(entry);
        
        draggingCheckDelay = new WaitForSecondsRealtime(0.5f);
    }

    public void MouseDownCaller(PointerEventData pointerEventData)
    {
        StartCoroutine(MouseDownTrigger());
    }

    public IEnumerator MouseDownTrigger()
    {
        
        if (!mouseIsDown)
        {
            mouseIsDown = true;
            yield return draggingCheckDelay;
            if (mouseIsDown && !dragging)
            {
                dragging = true;
                StartCoroutine(MouseDragging());
            }
        }
    }

    public IEnumerator MouseDragging()
    {
        if (dragging)
        {
            mouseDragEvent.Invoke();
            yield return dragUpdate;
            StartCoroutine(MouseDragging());
        }
    }

    public void MouseUpTrigger(PointerEventData pointerEventData)
    {
        StopAllCoroutines();
        if(mouseIsDown && !dragging)
        {
            mouseDownEvent.Invoke();
		//Only Trigger a mouse down event if there is not a dragging event running
        }
        else if(mouseIsDown && dragging)
        {
            mouseUpEvent.Invoke();
        }
        
        dragging = false;
        mouseIsDown = false;
        
    }

}
