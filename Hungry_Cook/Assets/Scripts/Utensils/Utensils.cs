using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Utensils : MonoBehaviour
{
    public UnityAction OnError;
    [SerializeField] private bool _canBeOvercooked = true;
    public bool IsPressedRightButton {get;set;}
    private float _time;
    private int _currentTime = 0;
    [SerializeField] private GameObject _sign;
   [SerializeField] private ProductDecorator _productDecorator;
   [SerializeField] private float _timeForPreparing = 18;
   [SerializeField] private float _timeForOvercook = 26;
    public ProductDecorator ProductDecorator { get => _productDecorator; set => _productDecorator = value; }
    public bool IsEmpty { get; set; } = true;
    public float TimeForPreparing { get => _timeForPreparing; set => _timeForPreparing = value; }
    public float CountOfTime { get => _time; set => _time = value; }
    public int CurrentTime { get => _currentTime; set => _currentTime = value; }
    public Product CurrentProduct {get;set;}
    public abstract void Cook();
    public bool IsCookingEnded {get;set;}
    [SerializeField] private bool _canCook = true;
    public bool CanCook {get => _canCook;set => _canCook = value;}
    public GameObject Sign { get => _sign; set => _sign = value; }

    public event UnityAction OnCook;
    public virtual void Overcook()
    {

    }
    private void Update() 
   {
    if(IsEmpty == false && CanCook)
    {
            CountOfTime += UnityEngine.Time.deltaTime;
    if (CountOfTime >= 1)
    {
        
        CurrentTime++;
         if (CurrentProduct != null)
        {
         CurrentProduct.StateOfProduct.Cook(TimeForPreparing,CurrentTime);
        }
        if (CurrentTime >= TimeForPreparing)
        {
                if (CurrentTime >= _timeForOvercook && _canBeOvercooked && Sign.activeInHierarchy)
                {
                    Overcook();
                    if (Sign.activeInHierarchy == true)
                    {
                        Sign.SetActive(false);
                    }
                }
                else if(CurrentTime < _timeForOvercook && CurrentTime >= TimeForPreparing)
                {
                 if (_canBeOvercooked && Sign.activeInHierarchy == false)
                 {
                  Sign.SetActive(true);     
                 }
                  Cook();
                 IsCookingEnded = true;
                 OnCook?.Invoke();
                
                }
        }
        
        CountOfTime = 0;
        
    }
   }
   }
}
