using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private List< Product> _products;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Plate _plate;
    [SerializeField] private Vector2Int _countOfAdditionProducts = new Vector2Int(0,10);
    [SerializeField] private Vector2Int _countOfReciptProducts = new Vector2Int(1, 3);
    private void Start() 
    {
        foreach (var item in _products)
        {
            int countOfProducts = 0;
            if(_plate.AllTheProducts.Contains(item))
            {
               countOfProducts = Random.Range(_countOfReciptProducts.x,_countOfReciptProducts.y); 
            }
            else
            {
             countOfProducts = Random.Range(_countOfAdditionProducts.x,_countOfAdditionProducts.y); 
            }
            for (int i = 0; i < countOfProducts; i++)
            {
                CreateProduct(item);
            }
        }
    }
    public void CreateProduct(Product item)
    {
            Instantiate(item,new Vector2(Random.Range(_bottomPoint.position.x, _topPoint.position.x), Random.Range(_bottomPoint.position.y, _topPoint.position.y)),Quaternion.identity);
    }
}
