using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryChangePlayerState {

    public CharactorControl controler;
    public TryChangePlayerState(CharactorControl controler)
    {
        this.controler = controler;
    }

    public bool TryMovePlayer()
    {
        //Debug.Log(controler.PlayerStateMachine.GetState((uint)controler.PlayerStateMachine.CurrentStateID));
        if (controler.PlayerStateMachine.CurrentStateID == (uint)UnitData.UnitStateType.move) return false;//当前已经是移动状态则不转换为移动状态
        return InputMoveKey();
    }

    bool InputMoveKey()
    {
        if (Input.GetKey(Keyboard.moveUp)) return true;
        if (Input.GetKey(Keyboard.moveDown)) return true;
        if (Input.GetKey(Keyboard.moveLeft)) return true;
        if (Input.GetKey(Keyboard.moveRight)) return true;
        return false;
    }

    public bool TryIdlePlayer()
    {
        if (controler.PlayerStateMachine.CurrentStateID == (uint)UnitData.UnitStateType.idle) return false;//当前已经是idle状态则不转换为idle状态
        return !InputMoveKey();
    }
}
