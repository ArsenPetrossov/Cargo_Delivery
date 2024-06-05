using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DrawBehavior _drawBehavior;
    [SerializeField] private RopeController _ropeController;
    [SerializeField] private BoxController _boxController;

    private void Start()
    {
        _drawBehavior.DrawingFinished += OnDrawingFinished;
        _ropeController.RopeOverFinish += OnRopeOverFinish;
    }

    private void OnDestroy()
    {
        _drawBehavior.DrawingFinished -= OnDrawingFinished;
        _ropeController.RopeOverFinish -= OnRopeOverFinish;
    }

    private void OnDrawingFinished(List<Vector3> positions)
    {
        _ropeController.StartMovingRope(positions);
    }

    private void OnRopeOverFinish(Vector3 position)
    {
        _boxController.DropDown(position);
    }
}