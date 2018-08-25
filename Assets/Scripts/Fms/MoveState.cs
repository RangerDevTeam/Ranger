using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState {

    private UnitData unitData;

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
    public void OnUpdate()
    {
        //向上走
        if (Input.GetKey(Keyboard.moveUp))
        {
            unitData.transform.Translate(Vector3.up * 2f * Time.deltaTime);
        }
        //向下走
        if (Input.GetKey(Keyboard.moveDown))
        {
            unitData.transform.Translate(Vector3.down * 2f * Time.deltaTime);
        }
        //向左走
        if (Input.GetKey(Keyboard.moveLeft))
        {
            unitData.transform.Translate(Vector3.left * 2f * Time.deltaTime);
            unitData.animationData.skeletonAnimation.skeleton.flipX = true;
        }
        //向右走
        if (Input.GetKey(Keyboard.moveRight))
        {
            unitData.transform.Translate(Vector3.right * 2f * Time.deltaTime);
            unitData.animationData.skeletonAnimation.skeleton.flipX = false;
        }

    }
    public  void OnFixedUpdate()
    { 
    }
    public  void OnLateUpdate()
    { 
    }
}
