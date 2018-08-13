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
        if (tryChangeCharactorState.TryAttackPlayer())
        {
            PlayerStateMachine.SwitchState((uint)UnitStateType.attack, null, null);
            return;
        }

        if (tryChangeCharactorState.TryMovePlayer())
        {
            //Debug.Log("改变为移动状态");
            PlayerStateMachine.SwitchState((uint)UnitStateType.move, null, null);
        }
        else if(tryChangeCharactorState.TryIdlePlayer())
        {
            //Debug.Log("改变为待机状态");
            PlayerStateMachine.SwitchState((uint)UnitStateType.idle, null, null);
        }
    }

}
