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
        else if(pointerOffset != Vector3.zero && Camera.main != null)
        {

            Debug.Log(Camera.main);
            _rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - pointerOffset);
        }
    }

    public void ReleaseCard()
    {
        pointerOffset = Vector3.zero;
    }
}
