using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public interface IAnimated 
{
      public SkeletonAnimation SkeletonAnimation {get;set;}
      public MeshRenderer MeshRenderer {get;set;}
      public MeshFilter MeshFilter {get;set;}
}
