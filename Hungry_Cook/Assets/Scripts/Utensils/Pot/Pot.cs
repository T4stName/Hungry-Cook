using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Utensils
{
    [SerializeField] private Transform _currentPotPoint;
    private HarpoonTip _harpoonTip;
    [SerializeField] private ParticleSystem _particle;
    private void Start()
    {
        _particle.Pause();
        _harpoonTip = FindObjectOfType<HarpoonTip>();
    }
   private void OnTriggerStay2D(Collider2D other) 
   {
    if(_harpoonTip.IsLeftButtonPressed == false)
    {
     if (other.TryGetComponent<Product>(out Product product)  && product.IsCought && IsPressedRightButton == false ) 
     {
        Debug.LogError("WHERE YOU GO?");
        if(product is IBrewed  && IsEmpty && product.CanBrew)
        {
            Debug.LogError("YOU GO THERE");
        product.IsInUtensils = true;
        product.StateOfProduct.AppearBar();
        product.transform.position = _currentPotPoint.position;
        product.SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        product.SpriteRenderer.sortingOrder = 3;
        IsEmpty = false;
        CurrentProduct = product;
        StartCoroutine(Reload());
        _particle.Play();
        product.OnTake += RemoveSign;
        }
        else if(product.CanBrew == false || IsEmpty  ==false)
        {
            Debug.Log("YEH YOU CAN!!!!!!!!");
            OnError?.Invoke();
        }
     }
    }
   }
   private void RemoveSign()
   {
     Sign.SetActive(false);
     if(CurrentProduct.Name == "N")
     {
    CurrentProduct.PlayOvercookedSound();
     }
     else
     {
    CurrentProduct.PlayCookedProductSound();
     }
    CurrentProduct.OnTake -= RemoveSign;
   }
   public override void Overcook()
   {
     if (CurrentProduct is IBrewed iBrewed)
     {
        iBrewed.Overbrew();
     }
   }
   private IEnumerator Reload()
   {
    yield return new WaitForSeconds(1);
    if(IsEmpty == false)
    {
        StartCoroutine(Reload());
    }
    else
    {
         _particle.Stop();
    }
   }


    public override void Cook()
    {
        ProductDecorator.CookProduct(CurrentProduct);
    }
}


