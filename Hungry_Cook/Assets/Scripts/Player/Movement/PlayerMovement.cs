using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.Events;
public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed = 4;
    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; set => _rigidbody2D = value; }
    public float Speed { get => _speed; set => _speed = value; }
    private Vector2 _currentPosition;
    public event UnityAction OnMove;
    [SerializeField] private Vector2 _velocity = new Vector2(-8, 8);
    private void Start() 
    {
        _currentPosition= transform.position; 
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update() 
    {
        Move();
    }
        public void Move()
    {
     Rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed,  Rigidbody2D.velocity.y);
     if (transform.position.x <= -22.5f)
     {
        Rigidbody2D.velocity= new Vector2(Mathf.Clamp(Rigidbody2D.velocity.x,0,_velocity.y),Rigidbody2D.velocity.y);
     }
     if (transform.position.x >= 22.5f)
     {
         Rigidbody2D.velocity= new Vector2(Mathf.Clamp(Rigidbody2D.velocity.x,_velocity.x,0),Rigidbody2D.velocity.y);
     }
     if (_currentPosition.x != transform.position.x)
     {
        OnMove?.Invoke();
        _currentPosition.x = transform.position.x;
     }
    }

}
