using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShowerRecipe : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField] private Transform _firstPoint;
   [SerializeField] private Transform _secondPoint;
   [SerializeField] private Vector2 _size = new Vector2(0.7619568f, 3.942136f); 
   [SerializeField] private Image _image;
   private Color _color;
   [SerializeField] private Color _secondColor;
   [SerializeField] private GameObject _recipe;
   private void Start() {
    _color = _image.color;
   }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _secondColor;
        transform.localScale = new Vector3(_size.y,_size.y);
        transform.position = _secondPoint.position;
        _recipe.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _color;
        transform.localScale = new Vector3(_size.x,_size.x);
        transform.position = _firstPoint.position;
        _recipe.SetActive(false);
    }
}
