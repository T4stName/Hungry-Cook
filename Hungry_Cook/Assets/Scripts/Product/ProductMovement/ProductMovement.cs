using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ProductMovement : MonoBehaviour,IMovable
{
    [SerializeField] private string _currentState;
    [SerializeField] private Rigidbody2D _rigidbdody;
    private Vector2 _direction;
    private bool _canMove = true;
    [SerializeField] private float _waitTime = 4;
    private float _speed;
    [SerializeField] private float _runSpeed = 3;
    [SerializeField] private float _walkSpeed = 1;
    private bool _isUp;
    private float _timer = 0;
    private float _currentTime;
    [SerializeField] private ProductAnimation _productAnimation;
    [SerializeField] private Vector2 _bordersOfMovingX = new Vector2(-22.15f, 22.15f);
    [SerializeField] private Vector2 _possibleBordersForMovingX = new Vector2(-22, 22);
    [SerializeField] private Vector2 _bordersOfMovingY = new Vector2(1, 7);
    [SerializeField] private Vector2 _possibleBordersForMovingY = new Vector2(1.1f, 6.9f);
    [SerializeField] private float _timeForWalking = 2;
    [SerializeField] private Vector2 _bordersBetweenTimeToChangeSpeed = new Vector2(2,4);
    [SerializeField] private Vector2 _bordersBetweenTimeInIdle = new Vector2(4,8);
    private Vector2 _startSise;
    public float Speed { get => _speed; set => _speed = value; }
    public Rigidbody2D Rigidbody2D { get => _rigidbdody; set => _rigidbdody = value; }
    private Dictionary<int, IEnumerator> _states = new Dictionary<int, IEnumerator>();
    private void Start() 
    {
        _startSise = transform.localScale;
        _direction = new Vector2(Random.Range(-1,1), Random.Range(-1,1));
        StartCoroutine(Stay());
        _states.Add(0,Stay());
        _states.Add(1,Walk());
        _states.Add(2,Run());
    }
     private IEnumerator ReturnRandomState()
    {
        int random = Random.Range(0,3);
        Debug.LogError(_states[random]);
        return _states[random];
    }
    private IEnumerator Stay()
    {
        _productAnimation.PlayIdle();
        Speed = 0;
        Rigidbody2D.velocity = Vector2.zero;
        float timer = Random.Range(_bordersBetweenTimeInIdle.x,_bordersBetweenTimeInIdle.y);
        yield return new WaitForSeconds(timer);
         StartRandomState();
    }
    private IEnumerator Run()
    {
        _productAnimation.PlayRun();
        Speed = _runSpeed;
        float timer = Random.Range(_bordersBetweenTimeToChangeSpeed.x,_bordersBetweenTimeToChangeSpeed.y);
        yield return new WaitForSeconds(timer);
        StartRandomState();
   
    }
    private IEnumerator Walk()
    {
        _productAnimation.PlayWalk();
        Speed = _walkSpeed;
        yield return new WaitForSeconds(_timeForWalking);
        StartRandomState();
    }
    private void StartRandomState()
    {
       int random = Random.Range(0,3);
        if (random == 0)
        {
            StartCoroutine(Stay());
        }
        else  if (random == 1)
        {
            StartCoroutine(Walk());
        }
        else if (random == 2)
        {
            StartCoroutine(Run());
        }    
    }
   

   private void Update() 
   {
    ChangeTime();
    Move();
   }
   public void Move()
   {
    Rigidbody2D.velocity = _direction * Speed;
    if (Rigidbody2D.velocity.x != 0)
    {
        int value = Rigidbody2D.velocity.x > 0 ? 1 : -1;
        transform.localScale = new Vector2(_startSise.x * value, transform.localScale.y); 
    }
    if (transform.position.y <= _bordersOfMovingY.x)
    {
        SetPositon(new Vector2(-1,1), new Vector2(0.1f,1),transform.position.x, _possibleBordersForMovingY.x);
    }
    if (transform.position.y >= _bordersOfMovingY.y)
    {
        SetPositon(new Vector2(-1,1), new Vector2(-1f,-0.1f),transform.position.x, _possibleBordersForMovingY.y);
    }
    if (transform.position.x <= _bordersOfMovingX.x)
    {
        SetPositon(new Vector2(0.1f,1), new Vector2(-1f,1), _possibleBordersForMovingX.x, transform.position.y);
    }
    if (transform.position.x >= _bordersOfMovingX.y)
    {
        SetPositon(new Vector2(-1,-0.1f), new Vector2(-1,1), _possibleBordersForMovingX.y, transform.position.y);  
    }
   }
   private void SetPositon(Vector2 bordersForPosibleMovementX, Vector2 bordersForPosibleMovementY, float currentX, float currentY)
   {
        transform.position = new Vector2(currentX, currentY);
        _currentTime = 0;
        ChangeDirection(new Vector2(bordersForPosibleMovementX.x,bordersForPosibleMovementX.y), new Vector2(bordersForPosibleMovementY.x,bordersForPosibleMovementY.y));

   }
   private void ChangeTime()
   {
    _timer += Time.deltaTime;
    if (_timer >= 1)
    {
        _currentTime++;
        _timer = 0;
        if (_waitTime <= _currentTime)
        {
            ChangeDirection(new Vector2(-1,1),new Vector2(-1,1));
        }
    }
   }
   private void ChangeDirection(Vector2 bordrersForX, Vector2 bordersForY)
   {
      _direction = new Vector2(Random.Range(bordrersForX.x,bordrersForX.y), Random.Range(bordersForY.x,bordersForY.y));
   }
}
