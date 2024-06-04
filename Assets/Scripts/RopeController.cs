using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private Transform _finishPoint;

    public event Action<Vector3> DropBox;
    private float _speed = 4;
    private Coroutine _coroutine;

    private void Update()
    {
        if (IsNearFinishPoint())
        {
            StopCoroutine(_coroutine);
            DropBox?.Invoke(_finishPoint.position);
        }
    }

    public void StartMovingRope(List<Vector3> positions)
    {
        _coroutine = StartCoroutine(MoveToTarget(positions));
    }

    private IEnumerator MoveToTarget(List<Vector3> positions)
    {
        foreach (var position in positions)
        {
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
                
                yield return null;
            }
        }
    }

    private bool IsNearFinishPoint()
    {
        var sum = _finishPoint.position.x - transform.position.x;
        return sum >= 0.1f;
    }
}