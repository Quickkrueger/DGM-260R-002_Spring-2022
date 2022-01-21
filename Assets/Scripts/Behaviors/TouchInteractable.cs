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
        draggingCheckDelay = new WaitForSecondsRealtime(1);
    }

    public IEnumerator OnMouseDown()
    {

        Debug.Log("Mouse down on" + gameObject.name);
        if (!mouseIsDown)
        {
            mouseIsDown = true;
            yield return draggingCheckDelay;
            if (mouseIsDown)
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
            mouseDownEvent.Invoke();
        }
        else if(mouseIsDown && dragging)
        {
            mouseUpEvent.Invoke();
        }
        mouseIsDown = false;
        dragging = false;
        StopAllCoroutines();
    }

}
