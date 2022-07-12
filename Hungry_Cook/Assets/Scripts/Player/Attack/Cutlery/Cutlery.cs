using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cutlery : MonoBehaviour
{
    [SerializeField] private int _maxOrder = 3;
    [SerializeField] private Transform _productPosition;
    public abstract void TryCatchingProduct();
    public abstract void DropOut();
    public bool CanCatch {get;set;} 
    public Product CurrentProduct {get;set;}
    public Transform ProductPosition { get => _productPosition; set => _productPosition = value; }
    public int MaxOrder { get => _maxOrder; set => _maxOrder = value; }
}
