using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : MoveState {


    public PlayerMoveState(UnitData u):base(u)
    {
        this.unitData = u;
    }

    public override void OnUpdate()
    {
        if (Input.GetKey(Keyboard.attack))
        {
            unitData.mySkillDatas[0].UseActiveSkill();//移动过程中如果按下攻击键，则优先进行攻击
            return;
        }
        if (InputMoveKey() == false)
        {
            unitData.PlayerStateMachine.SwitchState((uint)UnitData.UnitStateType.idle, null, null);//移动过程中松开移动健则回到idle
            return;
        } 

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


    public static bool InputMoveKey()
    {
        if (Input.GetKey(Keyboard.moveUp)) return true;
        if (Input.GetKey(Keyboard.moveDown)) return true;
        if (Input.GetKey(Keyboard.moveLeft)) return true;
        if (Input.GetKey(Keyboard.moveRight)) return true;
        return false;
    }



}
