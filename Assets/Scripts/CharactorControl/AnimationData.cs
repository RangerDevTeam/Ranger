using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AnimationData {

    public SkeletonAnimation skeletonAnimation;
    
    public delegate void GetAnimationEventHandler(SkeletonAnimation skeletonAnimation, string eventName);
    public event GetAnimationEventHandler GetAnimationEvent;

    public AnimationData(SkeletonAnimation _skeletonAnimation)
    {
        skeletonAnimation = _skeletonAnimation;
        ListenEvent();
    }



    void ListenEvent()
    {
        if (skeletonAnimation == null) return;
        skeletonAnimation.state.Event += ListenAnimtaionEvent;

    }

    void ListenAnimtaionEvent(Spine.TrackEntry trackEntry, Spine.Event e )
    {
        if (GetAnimationEvent != null)
            GetAnimationEvent(skeletonAnimation, e.data.Name);
    }





}
