using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryChangePlayerState {

    public CharactorControl controler;
    public TryChangePlayerState(CharactorControl controler)
    {
        this.controler = controler;
    }

    public void TryMovePlayer(UnitData unitdata)
    {
        //Debug.Log(controler.PlayerStateMachine.GetState((uint)controler.PlayerStateMachine.CurrentStateID));
        if (unitdata.PlayerStateMachine.CurrentStateID != (uint)UnitData.UnitStateType.idle) return;//当前已经是移动状态则不转换为移动状态
        unitdata.PlayerStateMachine.SwitchState((uint)UnitData.UnitStateType.move, null, null);
    }

    public void TryAttackPlayer()
    {
       // if (controler.PlayerStateMachine.CurrentStateID == (uint)UnitData.UnitStateType.attack);//当前已经是attack状态则不转换为attack状态
        
    }

    public void TryIdlePlayer(UnitData unitdata)
    {
        if (unitdata.PlayerStateMachine.CurrentStateID != (uint)UnitData.UnitStateType.move) return;//当前已经是idle状态则不转换为idle状态
        unitdata.PlayerStateMachine.SwitchState((uint)UnitData.UnitStateType.idle, null, null);
    }
}
