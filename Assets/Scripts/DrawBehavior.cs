using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawBehavior : MonoBehaviour
{
    [SerializeField] private Transform _ropePosition;

    private bool onDrawingEnd;
    private LineRenderer _lineRenderer;
    private List<Vector3> _drawPositions;

    public event Action<List<Vector3>> MoveRope;

    void Start()
    {
        _drawPositions = new List<Vector3>();
        _lineRenderer = GetComponent<LineRenderer>();
        _drawPositions.Add(_ropePosition.position);
    }

    void Update()
    {
        if (onDrawingEnd)
        {
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            DrawLine();
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            onDrawingEnd = true;
            MoveRope?.Invoke(_drawPositions);
        }
    }

    public void DrawLine()
    {
        Vector3 drawPosition = GetWorldMousePosition();

        if (_drawPositions.Count == 0 || Vector3.Distance(_drawPositions[^1], drawPosition) > 0.1f)
        {
            _drawPositions.Add(drawPosition);
            _lineRenderer.positionCount = _drawPositions.Count;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, drawPosition);
        }
    }

    private Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(_ropePosition.position).z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}