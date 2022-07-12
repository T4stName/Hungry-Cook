using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bread : Product,IBrewed, ICut, IFried
{
    private Sprite _overcookedSignSprite;
    private bool _ableToBrew;
    private bool _changeBread = false;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _sign;
    [SerializeField] private Sprite _grillSprite;
    [SerializeField] private Sprite _cutSprite;
    [SerializeField] private Sprite _friedAndCutSprite;
    [SerializeField] private Sprite _brewedSprite;
    [SerializeField] private string _name;

    public override string Name { get => _name; set => _name = value; }

    [SerializeField] private Sprite _sprite;

    public override Sprite Sprite { get => _sprite; set => _sprite = value; }
    public override SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public override SpriteRenderer Sign { get => _sign; set => _sign = value; }
    public override Sprite OvercookedSignSprite { get => _overcookedSignSprite; set => _overcookedSignSprite = value; }

    private void Start() 
    {
        SpriteRenderer.sprite = Sprite;
        DictionaryOfSprites.Add("B", _brewedSprite);
        DictionaryOfSprites.Add("G", _grillSprite);
        DictionaryOfSprites.Add("C", _cutSprite);
        DictionaryOfSprites.Add("F", _friedAndCutSprite);
        Rigidbody = GetComponent<Rigidbody2D>();
        IMovable = GetComponent<IMovable>();
        IAnimated = GetComponent<IAnimated>();
    }
    public void Brew()
    {
        if(_name.Contains("B") == false && _name.Contains("N") == false)
        {
            CanCut = false;
            CanFry = false;
        AddName("B");
        _ableToBrew = true;
        }
    }
    public override void AddName(string letter)
    {
        Name += letter + " ";
         StateOfProduct.AddIcon(letter,false);
        SpriteRenderer.sprite = DictionaryOfSprites[letter];
        if (letter == "B")
        {
            CanBrew = false;
        }
        if (letter == "C")
        {
             CanCut = false;
        }
        if (letter == "G")
        {
            CanFry = false;
        }
        if (letter == "F")
        {
            if (CanCut == false)
            {
                CanFry = false;
            }
            if (CanFry == false)
            {
                CanCut = false;
            }
        }
    }

    public void Fry()
    {
       if (CanGrillAndCut("G") && _ableToBrew == false)
       {
        if (_name.Contains("C"))
         {
          AddName("F");
         _name =  _name.Remove(_name.IndexOf("C"), 2);        
         }
         else
        {
            AddName("G");
        } 
        _changeBread = true;
       }
    }

    public void Cut()
    {
       if (CanGrillAndCut("C") && _ableToBrew == false)
       {
        if (_name.Contains("G"))
         {
        AddName("F");
        _name =  _name.Remove(_name.IndexOf("G"), 2);
        }
         else
        {
            AddName("C");
        } 
        _changeBread = true;
       }
    }

    public void Overbrew()
    {
        _spriteRenderer.color = OverbrewColor;
        TurnOnOvercookSign();
    }

    public void Overfry()
    {
        _spriteRenderer.color = OverfryColor;
        TurnOnOvercookSign();
    }
    private void TurnOnOvercookSign()
    {
        MakeOvercookedProduct();
        Sign.gameObject.SetActive(true);
        _name ="";
        _name += "N";
    }
}
