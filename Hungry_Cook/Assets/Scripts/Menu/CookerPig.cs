using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
public class CookerPig : MonoBehaviour
{
   [SerializeField] private SkeletonAnimation _skeletonAnimation;
   [SerializeField] private AnimationReferenceAsset _idle;
    [SerializeField] private AnimationReferenceAsset _idleBlink;
    private int _reachedCount;
    private int _countToBlink;
    private string _currentAnimation = "appear";
    [SerializeField] private Vector2Int _timeLimitBetweenBlinks = new Vector2Int(2,4);
    private void Start() 
    {
        PlayIdle();
    }
    public void PlayIdle()
    {
        SetAnimation(_idle,true,1);
    }
       private void SetAnimation(AnimationReferenceAsset animation,bool loop, float timeScale)
   {
       Spine.TrackEntry animationEntry = _skeletonAnimation.state.SetAnimation(0,animation,loop);
       animationEntry.TimeScale = timeScale;
       animationEntry.Complete += AnimationEntry_Complete;
       _currentAnimation = animation.name;
   }

   private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
   {
    _currentAnimation = "idle";
    UnityAction OnComplete = _reachedCount >= _countToBlink ?(UnityAction) IncreaseBlinkCount :(UnityAction) ResetBlinkCount; 
    OnComplete?.Invoke();
   }
   private void IncreaseBlinkCount()
   {
    PlayIdle();
    _countToBlink ++;
   }
   private void ResetBlinkCount()
   {
      _countToBlink = 0;
     _reachedCount = Random.Range(_timeLimitBetweenBlinks.x, _timeLimitBetweenBlinks.y);
     SetAnimation(_idleBlink,false,1);
   }
}
