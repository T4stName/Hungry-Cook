using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Board : Utensils
{
    [SerializeField] private Transform _currentPotPoint;
    private HarpoonTip _harpoonTip;
    private void Start() {
        _harpoonTip = FindObjectOfType<HarpoonTip>();
    }
   private void OnTriggerStay2D(Collider2D other) 
   {
    if(_harpoonTip.IsLeftButtonPressed == false)
    {
     if (other.TryGetComponent<Product>(out Product product)   && product.IsCought && IsPressedRightButton == false ) 
     {
        if(product is ICut  && IsEmpty && product.CanCut)
        {
        product.IsInUtensils = true;
        product.StateOfProduct.AppearBar();
        product.transform.position = _currentPotPoint.position;
        product.SpriteRenderer.sortingOrder = 3;
        product.transform.eulerAngles = new Vector3(0,0,90);
        product.Interface.transform.eulerAngles = new Vector3(0,0,0);
        product.Interface.transform.localScale = new Vector3(product.InterfaceSize.x, 1.4287f* product.InterfaceSize.y, 0);
        IsEmpty = false;
        CurrentProduct = product;
        }
        else if(product.TryGetComponent<ICut>(out ICut icut) && icut == null  && IsEmpty == false && product.CanCut == false)
        {
            OnError?.Invoke();
        }
     }
    }
   }
    


    public override void Cook()
    {
        ProductDecorator.CookProduct(CurrentProduct);
    }
}
