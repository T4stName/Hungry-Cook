using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductDecorator : Product
{
    public abstract void CookProduct(Product product);
}
