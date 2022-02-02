using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchInteractable : MonoBehaviour
{

    WaitForSecondsRealtime draggingCheckDelay;
    WaitForFixedUpdate dragUpdate;
    bool dragging = false;
    bool mouseIsDown = false;

    public UnityEvent mouseDownEvent;
    public UnityEvent mouseUpEvent;
    public UnityEvent mouseDragEvent;


    public void Start()
    {
        draggingCheckDelay = new WaitForSecondsRealtime(0.5f);
    }

    public IEnumerator OnMouseDown()
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
            Debug.Log("Mouse dragging on" + gameObject.name);
            mouseDragEvent.Invoke();
            yield return dragUpdate;
            StartCoroutine(MouseDragging());
        }
    }

    public void OnMouseUp()
    {
        if(mouseIsDown && !dragging)
        {
            Debug.Log("Mouse down on" + gameObject.name);
            mouseDownEvent.Invoke();
        }
        else if(mouseIsDown && dragging)
        {
            mouseUpEvent.Invoke();
        }
        StopAllCoroutines();
        dragging = false;
        mouseIsDown = false;
        
    }

}
