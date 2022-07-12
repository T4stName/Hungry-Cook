using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private List<ProductComponent> _productComponents;

    public List<ProductComponent> ProductComponents { get => _productComponents; set => _productComponents = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
}
