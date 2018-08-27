using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState {

    protected UnitData unitData;

    public MoveState(UnitData u)
    {
        this.unitData = u;
    }

    public uint GetStateID()
    {
        return (uint)UnitData.UnitStateType.move;
    }

    //void OnEnter();
    //void OnLeave();
    //等待补全
    public void OnEnter(StateMachine machine, IState preState, object param1, object param2)
    {
        AnimationPlay.Play(unitData, "run");
    }
    public void OnLeave(IState nextState, object param1, object param2)
    { 
    }
    //Unity 生命周期
    public virtual void OnUpdate()
    {

    }
    public  void OnFixedUpdate()
    { 
    }
    public  void OnLateUpdate()
    { 
    }
}
