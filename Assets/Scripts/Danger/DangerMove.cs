using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private List<GameObject> _wayPoints;
    [SerializeField] private Quaternion _rotateAngle = Quaternion.Euler(0f, 0f, 0.5f);
    private int _currentWayPoint = 0;
    private float _eps = 0.00001f;

    void Update()
    {
        float interpolationStep = _speed * Time.deltaTime;
        var targetPoint = _wayPoints[_currentWayPoint].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, interpolationStep);
        if (Vector3.Distance(transform.position, targetPoint) < _eps)
        {
            _currentWayPoint = (_currentWayPoint + 1) % _wayPoints.Count;
        }
        transform.rotation *= _rotateAngle;
    }
}
