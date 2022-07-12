using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Product : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSouse;
    [SerializeField] private GameObject _interface;
    [SerializeField] private StateOfProduct _stateOfProduct;
    [SerializeField] private bool _canBrew = true;
    [SerializeField] private bool _canFry = true;
    [SerializeField] private bool _canCut = true;
    public Color OverfryColor {get;set;}
    public Color OverbrewColor {get;set;}
    public virtual Sprite OvercookedSignSprite {get;set;}
    public virtual SpriteRenderer Sign { get ; set; }
    public virtual SpriteRenderer SpriteRenderer {get;set;}
    public virtual Sprite Sprite {get;set;}
    public virtual string Name {get;set;}
    public IMovable IMovable {get;set;}
    public IAnimated IAnimated {get;set;}
    public Rigidbody2D Rigidbody { get; set; }
    public Dictionary<string, Sprite> DictionaryOfSprites = new Dictionary<string, Sprite>();
    public bool IsInUtensils { get; set; }
    public bool IsCought { get; set; }
    public Vector2 StartSize { get; set; }
    public Vector2 InterfaceSize { get; set; }
    public StateOfProduct StateOfProduct { get => _stateOfProduct; set => _stateOfProduct = value; }
    public bool CanBrew { get => _canBrew; set => _canBrew = value; }
    public bool CanCut { get => _canCut; set => _canCut = value; }
    public bool CanFry { get => _canFry; set => _canFry = value; }
    public GameObject Interface { get => _interface; set => _interface = value; }
    public event UnityAction OnTake;
    public string ConsistencyOfCooking { get; set; }
    
    private void Awake() 
    {
        SpriteRenderer.sortingOrder = 5;
        InterfaceSize = _interface.transform.localScale;
        transform.localScale = new Vector3(0.4241533f,0.4241533f,0.4241533f);
        StartSize = transform.localScale;
        OverbrewColor = StateOfProduct.OverbrewColor;
        OverfryColor = StateOfProduct.OverfryColor;
    }
   public virtual void Return()
   {
    Destroy(IAnimated.SkeletonAnimation);
    Destroy( (MonoBehaviour)IMovable);
    Destroy(IAnimated.MeshRenderer);
    Destroy(IAnimated.MeshFilter);
    Destroy((MonoBehaviour) IAnimated);
    Rigidbody.velocity = Vector2.zero;
   }
    public void SetProductPosition(Vector3 harpoonTip)
    {
         transform.position = harpoonTip;
    }

    public virtual void AddName(string letter)
    {
        Name += letter + " ";
        _stateOfProduct.AddIcon(letter,false);
        if (letter != "B")
        {
            SpriteRenderer.sprite = DictionaryOfSprites[letter];
        }
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
    public void MakeOvercookedProduct()
    {
        CanBrew = false;
        CanCut = false;
        CanFry = false;
    }
    public bool CanGrillAndCut(string letter) => Name.Contains(letter) == false && Name.Contains("F") == false && Name.Contains("N") == false;
    public void SetNormalTransform()
    {
        Interface.transform.localScale = InterfaceSize;
        transform.localScale = StartSize;
        transform.eulerAngles = new Vector3(0,0,0);
        _interface.transform.eulerAngles = new Vector3(0,0,0);
        _stateOfProduct.DisappearBar();
    }
    public void TakeProduct()
    {
        OnTake?.Invoke();
    }
    public void PlayOvercookedSound()
    {
        int randomSound = Random.Range(0, _stateOfProduct.OvercookedProudctSoungs.Count);
        _audioSouse.clip = _stateOfProduct.OvercookedProudctSoungs[randomSound];
        _audioSouse.Play();
    }
    public void PlayCouchtProductSound()
    {
        int randomSound = Random.Range(0, _stateOfProduct.CoughtProudctSoungs.Count);
        _audioSouse.clip = _stateOfProduct.CoughtProudctSoungs[randomSound];
        _audioSouse.Play();
    }
    public void PlayCookedProductSound()
    {
        int randomSound = Random.Range(0, _stateOfProduct.CookedProudctSoungs.Count);
        _audioSouse.clip = _stateOfProduct.CookedProudctSoungs[randomSound];
        _audioSouse.Play();
    }
}
