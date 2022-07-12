using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedProduct : ProductDecorator
{
  public override void CookProduct(Product product)
    {
        if(product is IFried iFried)
        {
               if(product.CanFry)
            {
            product.ConsistencyOfCooking += "G";
            }
            /*
              if(product.ConsistencyOfCooking == "")
            {
            product.ConsistencyOfCooking += "G";
            }
            else
            {
                if (product.ConsistencyOfCooking.Contains("G") == false)
                {
                      product.ConsistencyOfCooking += "G"; 
                }
            }*/
            iFried.Fry();
        }
    }
}
