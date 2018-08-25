using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : IState {

    private UnitData unitData;

    public HurtState(UnitData u)
    {
        this.unitData = u;
    }

    public uint GetStateID()
    {
        return (uint)UnitData.UnitStateType.hurt;
    }

    //void OnEnter();
    //void OnLeave();
    //等待补全
    public void OnEnter(StateMachine machine, IState preState, object param1, object param2)
    {
        //Debug.Log("进入了受击动画");
        //AnimationPlay.Play(unitData,"idle");
    }
    public void OnLeave(IState nextState, object param1, object param2)
    { 
    }
    //Unity 生命周期
    public void OnUpdate()
    { 

    }
    public  void OnFixedUpdate()
    { 
    }
    public  void OnLateUpdate()
    { 
    }
}
