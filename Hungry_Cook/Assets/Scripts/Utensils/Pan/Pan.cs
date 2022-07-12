using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Utensils
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private Transform _currentPotPoint;
    private HarpoonTip _harpoonTip;
    private void Start() {
        _harpoonTip = FindObjectOfType<HarpoonTip>();
    }
   private void OnTriggerStay2D(Collider2D other) 
   {
    if(_harpoonTip.IsLeftButtonPressed == false)
    {
     if (other.TryGetComponent<Product>(out Product product) &&  product.IsCought  && IsPressedRightButton == false) 
     {
        if(product is IFried && IsEmpty && product.CanFry)
        {
        product.IsInUtensils = true;
        product.StateOfProduct.AppearBar();
        product.transform.position = _currentPotPoint.position;
        product.SpriteRenderer.sortingOrder = 2;
        IsEmpty = false;
        CurrentProduct = product;
        CurrentProduct.transform.localScale = new Vector3(0.3623829f, CurrentProduct.StartSize.y);
        CurrentProduct.transform.eulerAngles = new Vector3(0,0,90);
        product.Interface.transform.eulerAngles = new Vector3(0,0,0);
        product.Interface.transform.localScale = new Vector3(1f,1.4f);
        _fire.SetActive(true);
        product.OnTake += RemoveFire;
        }
        else if(product.TryGetComponent<IFried>(out IFried ifried) && ifried == null  && IsEmpty == false && product.CanFry == false)
        {
            OnError?.Invoke();
        }
     }
    }
   }
    public override void Overcook()
   {
     if (CurrentProduct is IFried iFried)
     {
        iFried.Overfry();
     }
   }
   private void RemoveFire()
   {
        _fire.SetActive(false);
        Sign.SetActive(false);
        CurrentProduct.OnTake -= RemoveFire;
   }
    public override void Cook()
    {
        ProductDecorator.CookProduct(CurrentProduct);
    }
}
