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
		//Only Trigger a mouse down event if there is not a dragging event running
        }
        else if(mouseIsDown && dragging)
        {
            mouseUpEvent.Invoke();
            Debug.Log("Mouse up on" + gameObject.name);
        }
        StopAllCoroutines();
        dragging = false;
        mouseIsDown = false;
        
    }

}
