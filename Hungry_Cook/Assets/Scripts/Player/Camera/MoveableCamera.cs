using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MoveableCamera : MonoBehaviour
{
    [SerializeField] private Transform _centerOfCamera;
   [SerializeField] private Player _player;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _focusSpeed = 0.2f;
    [SerializeField] private float _focusRange = 0.5f;
    [SerializeField] private Vector2 _orthographicSize;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _focusOffset;
    private Vector3 _currentOffset;
    private Vector3 _previusOffset;
    private Vector3 _startOffset;
   private Camera _camera;
     private Vector2 _velocity;
     [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;
     [SerializeField] private float _bottomLimit;
     [SerializeField] private float _upperLimit;

    public Camera Camera { get => _camera; set => _camera = value; }
    public Transform CenterOfCamera { get => _centerOfCamera; set => _centerOfCamera = value; }
    private bool _isOnFocus;
    private bool _canFocus;
    private void Start() 
    {
        _startOffset = _offset;
        _camera = GetComponent<Camera>();
        ChangeFocus(_focusOffset,_startOffset,true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeFocus(_startOffset,_focusOffset,false);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ChangeFocus(_focusOffset,_startOffset,true);
        }
              transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit), 
             Mathf.Clamp(transform.position.y, _bottomLimit, _upperLimit), 
             transform.position.z
        );
        if (_canFocus)
        {
            _offset = Vector3.MoveTowards(_previusOffset, _currentOffset, _focusSpeed * Time.deltaTime);
            float currentX = _isOnFocus ? _orthographicSize.x : _orthographicSize.y;
            _camera.orthographicSize = currentX * _offset.y; 
            if (Vector2.Distance(_offset, _currentOffset) <= _focusRange)
            {
                _canFocus = false;
            }
            
        }
    }
    private void ChangeFocus(Vector2 previusOffset, Vector2 currentOffset, bool isOnFocus)
    {
        _currentOffset = currentOffset;
        _previusOffset =previusOffset;
        _isOnFocus = isOnFocus;
        _canFocus = true;
    }
   private void FixedUpdate()
    {
        transform.position = new Vector3
        (GetPlayerPosition(transform.position.x, _player.transform.position.x + _offset.x,_velocity.x),
         GetPlayerPosition(transform.position.y, _player.transform.position.y + _offset.y,_velocity.y),
        transform.position.z);
    }
    private float GetPlayerPosition(float position,float playerPositon, float velocity) =>  Mathf.SmoothDamp(position, playerPositon, ref velocity, _speed); 
}
