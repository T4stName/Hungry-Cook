using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChecker : MonoBehaviour
{
   [SerializeField] private Camera _camera;
   [SerializeField] private Transform[] _points;
   private void Update() {
    if (Input.GetMouseButtonDown(1))
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        if (GetTriangleSquare() ==1 )
        {
            
        }
    }
   }
   private float GetRectangleSquare()
   {
    return  1;
   }
   private float GetTriangleSquare()
   {
    return 2 * (Vector2.Distance(_points[0].position, _points[1].position) + Vector2.Distance(_points[1].position, _points[2].position));
   }
}
