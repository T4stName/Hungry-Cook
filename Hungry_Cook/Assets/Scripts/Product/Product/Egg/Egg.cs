using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Egg : Product,IBrewed, ICut, IFried
{
     private Sprite _overcookedSignSprite;
    private bool _ableToBrew;
    private bool _isGrilled;
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
        if (CanGrillAndCut("B") && _isGrilled == false)
       {
        CanFry = false;
        AddName("B");
        _ableToBrew = true;
        CanCut = true;
       }
    }
    public override void AddName(string letter)
    {
        Name += letter + " ";
         StateOfProduct.AddIcon(letter,true);
        SpriteRenderer.sprite = DictionaryOfSprites[letter];
        if (letter == "B")
        {
            CanBrew = false;
        }
        if (letter == "C" || letter == "F")
        {
             CanCut = false;
        }
        if (letter == "G")
        {
            CanFry = false;
        }
    }

    public void Fry()
    {
       if(_name.Contains("G") == false && _name.Contains("N") == false && _ableToBrew == false)
        {
         CanCut = false;
        CanBrew = false;
        AddName("G");
        _isGrilled = true;
        }
    }

    public void Cut()
    {
        Debug.Log(CanGrillAndCut("C"));
       if (CanGrillAndCut("C") && _ableToBrew && _isGrilled == false)
       {
        CanCut = false;
          AddName("F");
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
