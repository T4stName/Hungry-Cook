using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
     [SerializeField] private SkeletonAnimation _skeletonAnimation;
     private void Start() {
        _skeletonAnimation.skeleton.A = 0.6f;
     }
}
