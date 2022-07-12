using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : Cutlery
{
    public override void TryCatchingProduct()
    {
        CanCatch = true;
    }
    public override void DropOut()
    {
        CanCatch = false;
        if(CurrentProduct != null)
        {
        CurrentProduct.SpriteRenderer.sortingOrder = 5;
        CurrentProduct = null;
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (CanCatch && CurrentProduct == null)
        {
            if (other.TryGetComponent<Utensils>(out Utensils utensils) && utensils.IsEmpty == false)
            {
                if ( utensils.CurrentProduct.IsCought)
                {
                    CurrentProduct = utensils.CurrentProduct;
                    CurrentProduct.SpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
                    CurrentProduct.SpriteRenderer.sortingOrder = MaxOrder;
                    utensils.IsCookingEnded = false;
                CurrentProduct.SetNormalTransform();
                CurrentProduct.TakeProduct();
                    utensils.CurrentTime = 0;
                    CurrentProduct.IsInUtensils = false;
                    utensils.IsEmpty = true;
                }
            }
            else if (other.TryGetComponent<Product>(out Product product) && product.IsInUtensils == false&& product.IsCought)
            {
                Debug.Log(product);
                CurrentProduct = product;
                CurrentProduct.SetNormalTransform();
                CurrentProduct.TakeProduct();
                CurrentProduct.SpriteRenderer.sortingOrder = MaxOrder;
            }
        }
    }
    private void Update() 
    {
        if (CanCatch && CurrentProduct != null)
        {
            CurrentProduct.SetProductPosition(ProductPosition.position);
        }
    }
}
