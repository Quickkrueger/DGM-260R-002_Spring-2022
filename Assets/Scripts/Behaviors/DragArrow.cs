
using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragArrow : MonoBehaviour
{
    public Vector3 origin;
    private Vector3 target;
    public float baseHeadScale = 0.4f;
    private float relativeHeadScale;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void InitializeArrow(Vector3 newOrigin)
    {
        origin = newOrigin;

    }

    public void UpdateArrow(Vector3 newTarget)
    {
        Debug.Log(Input.mousePosition);
        target = newTarget;
        CalculateArrow();
    }

    private void CalculateArrow()
    {
        
        relativeHeadScale = (baseHeadScale / Vector3.Distance(origin, target));
        
        
        _lineRenderer.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.025f),
            new Keyframe(0.999f - relativeHeadScale, 0.025f),
            new Keyframe(1 - relativeHeadScale, 0.075f),
            new Keyframe(1f, 0f)
            );
        
        _lineRenderer.SetPositions(new Vector3[]
        {
            origin,
            Vector3.Lerp(origin, target, 0.999f - relativeHeadScale),
            Vector3.Lerp(origin, target, 1f - relativeHeadScale),
            target
        });
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
