using UnityEngine;



public class Card : MonoBehaviour
{
    Vector3 pointerOffset;
    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void DragCard()
    {
        Vector3 pointerPosition = Input.mousePosition;
        if(pointerOffset == Vector3.zero)
        {
            pointerOffset = new Vector3();
            pointerOffset = pointerPosition - Camera.main.WorldToScreenPoint(transform.position);
        }


        if(pointerOffset != Vector3.zero && Camera.current != null)
        {

            Debug.Log(Camera.current);
            _rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - pointerOffset);
        }
    }

    public void ReleaseCard()
    {
        pointerOffset = Vector3.zero;
    }
}
