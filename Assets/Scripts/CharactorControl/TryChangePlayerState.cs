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
        if (InputAttackKey()) return false;
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

    bool InputAttackKey()
    {
        if (Input.GetKey(Keyboard.attack)) return true;
        return false;
    }

    public bool TryAttackPlayer()
    {
        if (controler.PlayerStateMachine.CurrentStateID == (uint)UnitData.UnitStateType.attack) return false;//当前已经是attack状态则不转换为attack状态
        return InputAttackKey();
    }

    public bool TryIdlePlayer()
    {
        if (controler.PlayerStateMachine.CurrentStateID == (uint)UnitData.UnitStateType.idle) return false;//当前已经是idle状态则不转换为idle状态
        if (InputAttackKey()) return false; //当按下攻击键时不转换为idle状态
        return !InputMoveKey();
    }
}
