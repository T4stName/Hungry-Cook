using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutProduct : ProductDecorator
{
   public override void CookProduct(Product product)
    {
        if(product is ICut iCut)
        {
               if(product.CanCut)
            {
            product.ConsistencyOfCooking += "C";
            }
            iCut.Cut();
        }
    }
}
