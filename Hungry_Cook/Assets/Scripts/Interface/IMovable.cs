using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    public void Move();
    public Rigidbody2D Rigidbody2D {get;set;}
    public float Speed {get;set;}
}
