using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class HarpoonTip : MonoBehaviour
{
    public event UnityAction OnCought;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private LineRenderer _lineRenderer;
    private float _startSpeed;
    private bool _canMove;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _lineRendererStartPoint;
    [SerializeField] private Transform _lineRendererEndPoint;
    [SerializeField] private float _speed;
    private Transform _currentPositon;
    [SerializeField] private float _range = 0.3f;
    private Product _currentProduct;
    private Vector2 _positionOfHarpoon;
    public Product CurrentProduct { get => _currentProduct; set => _currentProduct = value; }
    public bool IsLeftButtonPressed { get; set; }
    private void Start() 
    {
        _positionOfHarpoon = transform.position;
        _startSpeed = _speed;
        _playerMovement.OnMove += SetProductPosition;
    }
    private void Update() 
    {
        if ((Vector2)transform.position != _positionOfHarpoon)
        {
            SetProductPosition();
            _positionOfHarpoon = transform.position;
        }
        _lineRenderer.SetPosition(0,_lineRendererStartPoint.position);
        _lineRenderer.SetPosition(1, _lineRendererEndPoint.position);
        if (_canMove)
        {
        UnityAction OnCheckDistance = Vector2.Distance(transform.position, _currentPositon.position) > _range ? (UnityAction) Move : (UnityAction) TryReturning;
        OnCheckDistance?.Invoke();
        }
    }
    private void TryReturning()
    {
        UnityAction OnReturn = _currentPositon == _startPoint ? (UnityAction) StopMoving :(UnityAction) Return;
        OnReturn?.Invoke();
    }
    public void SetProductPosition()
    {
        CurrentProduct?.SetProductPosition(transform.position);
    }
    private void StopMoving()
    {
         _canMove = false;
         if(CurrentProduct != null)
         {
         OnCought?.Invoke();
         }
    }
    private void Move() => transform.position = Vector2.MoveTowards(transform.position, _currentPositon.position, _speed * Time.deltaTime);
    public void ContinieToMove()
    {
        SetMovement( _endPoint);
    }
    public void Return()
    {
        SetMovement(_startPoint);
    }
    private void SetMovement( Transform position)
    {
        _currentPositon = position;
        _canMove = true;
        SetProductPosition();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent<Product>(out Product product) && CurrentProduct != product && IsLeftButtonPressed && product.IsInUtensils == false && _currentProduct == null && product is ProductDecorator == false)
        {
            if(product.IsCought == false)
            {
            _playerAttack. ReturnTip();
            product.PlayCouchtProductSound();
             product.Return();
             product.SpriteRenderer.gameObject.SetActive(true);
            }
            
            product.IsCought = true;
           SetProductPosition();
            
             CurrentProduct = product;

        }
    }
}
