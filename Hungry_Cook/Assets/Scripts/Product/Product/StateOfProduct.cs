using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class StateOfProduct : MonoBehaviour
{
   private Canvas _canvas;
   [SerializeField] private List<AudioClip> _coughtProudctSoungs;
    [SerializeField] private List<AudioClip> _overcookedProudctSoungs;
   [SerializeField] private List<AudioClip> _cookedProudctSoungs;
    private bool _cutSelect;
    private bool _frySelect;
    [SerializeField] private Color _overbrewColor;
    [SerializeField] private Color _overfryColor;
    private int _currentIndexOfIcon;
   private Sprite _currentSprite;
   [SerializeField] private List<Image> _icons;
   [SerializeField] private List<Sprite> _sprites;
   private Dictionary<string, Sprite> _dictionary = new Dictionary<string, Sprite>();
   [SerializeField] private Image _barOfProgress;
   [SerializeField] private GameObject _bar;
   private float _fillValue;

    public Color OverfryColor { get => _overfryColor; set => _overfryColor = value; }
    public Color OverbrewColor { get => _overbrewColor; set => _overbrewColor = value; }
    public List<AudioClip> CoughtProudctSoungs { get => _coughtProudctSoungs; set => _coughtProudctSoungs = value; }
    public List<AudioClip> OvercookedProudctSoungs { get => _overcookedProudctSoungs; set => _overcookedProudctSoungs = value; }
    public List<AudioClip> CookedProudctSoungs { get => _cookedProudctSoungs; set => _cookedProudctSoungs = value; }
    private Utensils _utensils;
    private void Start() 
   {
      _canvas = GetComponent<Canvas>();
      _canvas.sortingOrder = 20;
    _dictionary.Add("B", _sprites[0]);
    _dictionary.Add("G", _sprites[1]);
    _dictionary.Add("C", _sprites[2]);
   }
   public void Cook(float timeForPreparing, float currentTimeForPreparing)
   {
          AppearBar();
        _fillValue = currentTimeForPreparing;
        _fillValue = _fillValue / timeForPreparing;
        _barOfProgress.fillAmount = _fillValue;
   }
   public void AppearBar()
   {
      _barOfProgress.fillAmount = 0;
      if (_bar.activeInHierarchy == false)
       {
        _bar.SetActive(true);
       }
   }
   public void DisappearBar()
   {
        _bar.SetActive(false);
   }
   public void AddIcon(string letter,bool isEgg)
   {
       Debug.LogError(letter);
      if (isEgg)
      {
           _icons[_currentIndexOfIcon].gameObject.SetActive(true);
     if(letter != "F")
     {
     _icons[_currentIndexOfIcon].sprite = _dictionary[letter];
     if (letter == "B")
     {
        _frySelect = true;
     }
     else if (letter == "C")
     {
        _cutSelect = true;
     }
     }
     else
     {
        if (_cutSelect)
        {
            _icons[_currentIndexOfIcon].sprite = _sprites[0];
        }
        else if (_frySelect)
        {
            _icons[_currentIndexOfIcon].sprite = _sprites[2];
        }
     }
     _currentIndexOfIcon++;
      }
     else
     {
    if(_currentIndexOfIcon < _sprites.Count)
    {
     _icons[_currentIndexOfIcon].gameObject.SetActive(true);
     if(letter != "F")
     {
     _icons[_currentIndexOfIcon].sprite = _dictionary[letter];
     if (letter == "G")
     {
        _frySelect = true;
     }
     else if (letter == "C")
     {
        _cutSelect = true;
     }
     }
     else
     {
        if (_cutSelect)
        {
            _icons[_currentIndexOfIcon].sprite = _sprites[1];
        }
        else if (_frySelect)
        {
            _icons[_currentIndexOfIcon].sprite = _sprites[2];
        }
     }
     _currentIndexOfIcon++;
    }
   }
   }
}
