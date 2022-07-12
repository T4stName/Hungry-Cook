using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Plate : MonoBehaviour
{
  [SerializeField] private Settings _settings;
  [SerializeField] private GameObject _winScene;
  [SerializeField] private SpriteRenderer _winSceneSpriteRenderer;
  public event UnityAction OnWin;
  [SerializeField] private Recipe _recipe;
  [SerializeField] private List<Product> _products = new List<Product>();
  private List<ProductComponent> _productComponents = new List<ProductComponent>();
  private ProductComponent _currentProductComponent;
  private List<Product> _allTheProducts = new List<Product>();
  private PlayerAttack _playerAttack;

    public List<Product> AllTheProducts { get => _allTheProducts; set => _allTheProducts = value; }

    private void Awake() 
  {
    _playerAttack = FindObjectOfType<PlayerAttack>();
    _productComponents = _recipe.ProductComponents;
    foreach (var item in _productComponents)
    {
        AllTheProducts.Add(item.Product);
    }
  }
  private void OnTriggerStay2D(Collider2D other) 
  {
    if (other.TryGetComponent<Product>(out Product product) && _playerAttack.Harpoon.HarpoonTip.CurrentProduct == null && _playerAttack.CurrentCutlery.CurrentProduct == null)
    {
        bool isDishContainsIt = false;
        foreach (var item in _productComponents)
        {
            if (item.Product.GetType() == product.GetType())
            {
                isDishContainsIt = true;
                _currentProductComponent = item;
                break;
            }
        }
        if (isDishContainsIt)
        {
            if(_currentProductComponent.IsConsistencyImportant == false)
            {
              string productName =  product.Name.Replace(" ", "");
              if (productName == _currentProductComponent.Name)
              {
                TryFinishingDish(product);
                Destroy(product.gameObject);
              }
            }
            else
            {
                if (_currentProductComponent.Name == product.ConsistencyOfCooking)
                {
                   TryFinishingDish(product);
                   Destroy(product.gameObject);
                }
            }
        }
    }
  }
  private void TryFinishingDish(Product product)
  {
     _products.Add(product);
    ProductComponent productComponent =  _productComponents.Find(e=>e.GetType() == product.GetType());
     _productComponents.Remove(productComponent);
     if (_products.Count == AllTheProducts.Count)
     {
      OnWin?.Invoke();
      StartCoroutine(Cooldown());
     }
  }
  private IEnumerator Cooldown()
  {
    _winScene.gameObject.SetActive(true);
      _winSceneSpriteRenderer.sprite = _recipe.Sprite;
    yield return new WaitForSeconds(5);
    _settings.OpenMenu();
  }
}
