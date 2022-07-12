using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotWater : MonoBehaviour
{
    [SerializeField] private Transform _startWaterPosition;
    [SerializeField] private Transform _endWaterPosition;
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _range = 3;
    private Vector3 _currentPosition;
    private bool _isStartPosition = true;
    private void Start() 
    {
        transform.position = _startWaterPosition.position;
        _currentPosition = _endWaterPosition.position;
    }
    private void Update() 
    {
        transform.position = Vector3.MoveTowards(transform.position,_currentPosition,_speed  * Time.deltaTime);
        if (Vector2.Distance(transform.position,_currentPosition) <= _range)
        {
            ChangeDirection();
        }
    }
    private void ChangeDirection()
    {
        _isStartPosition =! _isStartPosition;
        _currentPosition = _isStartPosition ? _startWaterPosition.position : _endWaterPosition.position;
    }

}
