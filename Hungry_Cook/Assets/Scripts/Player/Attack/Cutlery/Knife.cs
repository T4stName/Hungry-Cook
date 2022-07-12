using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Cutlery
{
    [SerializeField] private Animator _knifeAnimator;
    [SerializeField] private Animator _animator;
    private Utensils _currentUtensils;
      public override void TryCatchingProduct()
    {
        if(CurrentProduct != null)
        {
        _animator.SetBool("CUT",true);
        _knifeAnimator.SetBool("CUT",true);
        }
        CanCatch = true;
        if (_currentUtensils != null)
        {
         _currentUtensils.CanCook = true;
        }
    }
    public override void DropOut()
    {
        if(CurrentProduct != null)
        {
        StopCutting();
        }
        CanCatch = false;
        if(_currentUtensils != null)
        {
         _currentUtensils.CanCook = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (CanCatch)
        {
            if (other.TryGetComponent<Utensils>(out Utensils utensils) && utensils.IsEmpty == false && utensils is Board)
            {
 
                if (utensils.CurrentProduct is ICut)
                {
                    if (CurrentProduct == null)
                    {
                        _animator.SetBool("CUT",true);
                        _knifeAnimator.SetBool("CUT",true);
                    }
                    CurrentProduct =  utensils.CurrentProduct;
                    _currentUtensils = utensils;
                    _currentUtensils.CanCook = true;
                    _currentUtensils.OnCook += StopCutting;
                }
            }
        }
    }
    private void StopCutting()
    {
        _animator.SetBool("CUT",false);
         _knifeAnimator.SetBool("CUT",false);
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.TryGetComponent<Utensils>(out Utensils utensils)  && utensils.IsEmpty == false && utensils is Board)
        {
            if(_currentUtensils != null)
            {
            StopCutting();
            _currentUtensils.OnCook -= StopCutting;  
            _currentUtensils.CanCook = false;
            CurrentProduct = null;
            }
            _currentUtensils = null;
        }
    }
    private void OnDisable() {
        if (CurrentProduct != null)
        {
            StopCutting();
        }
    }
}
