using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IdleState {

    public PlayerIdleState(UnitData u) : base(u)
    {
        this.unitData = u;
    }

    public override void OnUpdate()
    {
        if (Input.GetKey(Keyboard.attack))
        {
            unitData.mySkillDatas[0].UseActiveSkill();//idle过程中如果按下攻击键，则优先进行攻击
            return;
        }
  
        if (PlayerMoveState.InputMoveKey() == true)
        {
            unitData.PlayerStateMachine.SwitchState((uint)UnitData.UnitStateType.move, null, null);//idle过程中按下移动健则回到idle
        }
    }


}
