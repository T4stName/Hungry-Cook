using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class ProductAnimation : MonoBehaviour, IAnimated
{
   [SerializeField] private SkeletonAnimation _skeletonAnimation;
   [SerializeField] private AnimationReferenceAsset _idle;
    [SerializeField] private AnimationReferenceAsset _run;
    [SerializeField] private AnimationReferenceAsset _walk;
    private string _currentAnimation = "appear";
    public SkeletonAnimation SkeletonAnimation { get => _skeletonAnimation; set => _skeletonAnimation = value; }
    public MeshRenderer MeshRenderer {get;set;}
    public MeshFilter MeshFilter { get; set; }

    private void Start() 
    {
        MeshFilter = GetComponent<MeshFilter>();
        MeshRenderer = GetComponent<MeshRenderer>();
        PlayIdle();
    }
    public void PlayIdle()
    {
        SetAnimation(_idle,true,1);
    }
    public void PlayWalk()
    {
        SetAnimation(_walk,true,1);
    }
    public void PlayRun()
    {
        SetAnimation(_run,true,1);
    }
    private void SetAnimation(AnimationReferenceAsset animation,bool loop, float timeScale)
   { 
       Spine.TrackEntry animationEntry = SkeletonAnimation.state.SetAnimation(0,animation,loop);
       animationEntry.TimeScale = timeScale;
       animationEntry.Complete += AnimationEntry_Complete;
       _currentAnimation = animation.name;
   }

   private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
   {
   // PlayIdle();
   }
}
