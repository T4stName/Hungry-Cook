using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Harpoon : MonoBehaviour
{
    [SerializeField] private Vector2 _bordersOfRotation;
    [SerializeField] private float _additionRotate = 90f;
    private Vector2 _mousePosition;
    [SerializeField] private HarpoonTip _harpoonTip;
    [SerializeField] private MoveableCamera _camera;

    public HarpoonTip HarpoonTip { get => _harpoonTip; set => _harpoonTip = value; }

    private Vector3 Rotate(Vector2 rotationPostion, float additionRotate)
    {
        Vector2 position = _mousePosition - rotationPostion;
        return new Vector3(0, 0, Mathf.Clamp( Mathf.Rad2Deg  *  Mathf.Atan2(position.y, position.x) + additionRotate, _bordersOfRotation.x, _bordersOfRotation.y));
    }
    public void RotateHarpoon()
    {
        _mousePosition = _camera.Camera.ScreenToWorldPoint(Input.mousePosition);
        float value = transform.eulerAngles.z >= 180 ?transform.eulerAngles.z - 360 : transform.eulerAngles.z;
        UnityAction OnSetRotation = value==  _bordersOfRotation.y || value ==  _bordersOfRotation.x ?(UnityAction) CursorOutOfSightRotate  :(UnityAction)   CursorInSightRotate;
        OnSetRotation?.Invoke();
    }
    private void CursorInSightRotate() => SetRotation(Rotate(transform.position ,_additionRotate).z); 
    
    private void CursorOutOfSightRotate()
    {
            float valueOfRotation = _mousePosition.x > _camera.CenterOfCamera.position.x ? -1 : 1;
            SetRotation(valueOfRotation * Mathf.Abs(Rotate(transform.position ,_additionRotate).z ));
    }

    public void ReturnTip()
    {
        HarpoonTip.Return();
        HarpoonTip.CurrentProduct = null;
    }
    public void MoveTip()
    {
      HarpoonTip.ContinieToMove();
    }
    private void SetRotation(float zRotation) => transform.eulerAngles = new Vector3(0,0, zRotation);
    
}
