using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DrawBehavior _drawBehavior;
    [SerializeField] private RopeController _ropeController;
    [SerializeField] private BoxController _boxController;

    private void Start()
    {
        _drawBehavior.MoveRope += OnMoveRope;
        _ropeController.DropBox += OnDropBox;
    }

    private void OnDestroy()
    {
        _drawBehavior.MoveRope -= OnMoveRope;
        _ropeController.DropBox -= OnDropBox;
    }

    private void OnMoveRope(List<Vector3> positions)
    {
        _ropeController.StartMovingRope(positions);
    }

    private void OnDropBox(Vector3 position)
    {
        _boxController.DropDown(position);
    }
}