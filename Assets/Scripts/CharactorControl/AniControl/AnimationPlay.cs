using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public static class AnimationPlay {

    //重载四个方法，传入不同参数时不同的处理
    //未传入循环，默认循环
    //未传入序号，默认为0
    public static void Play(UnitData unitData,string animationName)
    {
       // Play(unitData, animationName, true, (uint)unitData.PlayerStateMachine.CurrentStateID);
        Play(unitData, animationName, true, 0);
    }

    public static void Play(UnitData unitData, string animationName,bool loop)
    {
        //Play(unitData, animationName, loop, (uint)unitData.PlayerStateMachine.CurrentStateID);
        Play(unitData, animationName, loop, 0);
    }

    public static void Play(UnitData unitData, string animationName, uint animationNumber)
    {
        Play(unitData, animationName, true, animationNumber);
    }

    public static void Play(UnitData unitData, string animationName, bool loop,uint animationNumber)
    {
        unitData.skeletonAnimation.state.SetAnimation((int)animationNumber, animationName, loop);

    }

}
