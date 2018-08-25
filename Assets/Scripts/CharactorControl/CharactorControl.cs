using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorControl : UnitData {

    protected TryChangePlayerState tryChangeCharactorState;

    void Awake()
    {
        PlayerStateMachine.RegisterState(new MoveState(this));
        PlayerStateMachine.RegisterState(new IdleState(this));
        PlayerStateMachine.RegisterState(new AttackState(this));

        tryChangeCharactorState = new TryChangePlayerState(this);
    }

	// Use this for initialization
	protected override void Start () {

        base.Start();

        PlayerStateMachine.SwitchState((uint)UnitStateType.idle,null,null);
		
	}
	
	// Update is called once per frame
	protected override void Update () {


        base.Update();
       ControlPlayer();

	}


    //要考虑下是否有更好的写法
    //通过输入的按键控制玩家
    void ControlPlayer()
    {
        if (Input.GetKey(Keyboard.attack)) mySkillDatas[0].UseActiveSkill();
        if (InputMoveKey() == false) tryChangeCharactorState.TryIdlePlayer(this);
        if (InputMoveKey() == true) tryChangeCharactorState.TryMovePlayer(this);


    }


    bool InputMoveKey()
    {
        if (Input.GetKey(Keyboard.moveUp)) return true;
        if (Input.GetKey(Keyboard.moveDown)) return true;
        if (Input.GetKey(Keyboard.moveLeft)) return true;
        if (Input.GetKey(Keyboard.moveRight)) return true;
        return false;
    }




}

