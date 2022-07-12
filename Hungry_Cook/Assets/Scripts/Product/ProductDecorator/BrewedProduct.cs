using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewedProduct : ProductDecorator
{

    public override void CookProduct(Product product)
    {
        if(product is IBrewed iBrewed)
        {
               if(product.CanBrew)
            {
            product.ConsistencyOfCooking += "B";
            }
            
            iBrewed.Brew();
        }
    }
}
