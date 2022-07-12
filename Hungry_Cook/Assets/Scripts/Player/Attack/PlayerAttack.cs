using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Harpoon _harpoon;
    [SerializeField] private List< Cutlery> _cutleries;
    [SerializeField] private Cutlery _currentCutlery;
    private Cutlery _previusCutlery;
    private List<Utensils> _utensils = new List<Utensils>();
    private int _index;
    private bool _isRightButtonClicked = false;
    private bool _isLeftButtonClicked = false;
    public Cutlery CurrentCutlery { get => _currentCutlery; set => _currentCutlery = value; }
    public Harpoon Harpoon { get => _harpoon; set => _harpoon = value; }

    private void Start() {
        Utensils[] utensils = FindObjectsOfType<Utensils>();
         foreach (var item in utensils)
         {
            _utensils.Add(item);
         }
        CurrentCutlery = _cutleries[0];
    }
   private void Update() {
    if (Input.GetMouseButton(0))
    {
        Harpoon.RotateHarpoon();
        if (Input.GetMouseButtonDown(0))
        {
          _isLeftButtonClicked = true;
            Harpoon.HarpoonTip.IsLeftButtonPressed = true;
            _audioSource.clip = _sound;
            _audioSource.Play();
             Harpoon.MoveTip();
        }
    }
    if (Input.GetMouseButtonUp(0))
    {
        ReturnTip();
          _isLeftButtonClicked = false;
        Harpoon.HarpoonTip.IsLeftButtonPressed = false;
    }
     if (Input.GetMouseButton(1))
    {
         if (Input.GetMouseButtonDown(1))
         {
          _isRightButtonClicked = true;
           CurrentCutlery.TryCatchingProduct(); 
           _utensils.ForEach(e=>e.IsPressedRightButton = true);
         }   
    }
    if (Input.GetMouseButtonUp(1))
    {
          _isRightButtonClicked = false;
        CurrentCutlery.DropOut();
        _utensils.ForEach(e=>e.IsPressedRightButton = false);
    }
    if(_isRightButtonClicked == false && _isLeftButtonClicked == false)
    {
    float mouseScrolling = Input.GetAxis("Mouse ScrollWheel");
    if(mouseScrolling != 0)
    {
    UnityAction OnScrool = mouseScrolling > 0 ?(UnityAction) ScroolUp:(UnityAction) ScroolDown;
    OnScrool?.Invoke();
   }
    }
   }
   public void ReturnTip()
   {
         Harpoon.ReturnTip();
   }
   private void ScroolUp()
   {
        int index = _index < _cutleries.Count -1 ? _index +1 : 0;
        SetCurrentCutlery(index);
   }
   private void ScroolDown()
   {
        int index = _index > 0 ? _index - 1 : _cutleries.Count - 1;
        SetCurrentCutlery(index);
   }
    
   private void SetCurrentCutlery(int index)
   {
      if (CurrentCutlery != null)
      {
        _previusCutlery = CurrentCutlery;
        _previusCutlery.gameObject.SetActive(false);
      }
     CurrentCutlery = _cutleries[index];
     CurrentCutlery.gameObject.SetActive(true);
     _index = index;
   }

   
}

