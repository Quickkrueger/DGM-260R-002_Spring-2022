
using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragArrow : MonoBehaviour
{
    private Vector3 origin;
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
        target = newTarget;
    }

    private void CalculateArrow()
    {
        _lineRenderer.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.4f),
            new Keyframe(0.999f - baseHeadScale, 0.4f),
            new Keyframe(1 - baseHeadScale, 1f),
            new Keyframe(1f, 0f)
            );
        
        _lineRenderer.SetPositions(new Vector3[]
        {
            origin,
            Vector3.Lerp(origin, target, 0.999f - baseHeadScale),
            Vector3.Lerp(origin, target, 1f - baseHeadScale),
            target
        });
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
