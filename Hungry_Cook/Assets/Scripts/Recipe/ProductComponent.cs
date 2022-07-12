using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ProductComponent", order = 1)]
public class ProductComponent : ScriptableObject
{
   [SerializeField] private Product _product;
   [SerializeField] private string _name;
   [SerializeField] private bool _isConsistencyImportant;
    public Product Product { get => _product; set => _product = value; }
    public string Name { get => _name; set => _name = value; }
    public bool IsConsistencyImportant { get => _isConsistencyImportant; set => _isConsistencyImportant = value; }
}
