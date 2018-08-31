using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorControl : UnitData {

    protected TryChangePlayerState tryChangeCharactorState;
    void Awake()
    {
        PlayerStateMachine.RegisterState(new PlayerMoveState(this));
        PlayerStateMachine.RegisterState(new PlayerIdleState(this));
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
        getKeyDownCode();
    }

    //按键检测
    public static void getKeyDownCode()
    {
        if (Input.anyKeyDown)
        {
            
            Debug.Log("11       "+(KeyCode)Enum.Parse(typeof(KeyCode),"leftControl"));
            Debug.LogError(Event.KeyboardEvent(Input.inputString));
            uint skillID = Keyboard.Instance.GetSkillId(Event.KeyboardEvent(Input.inputString).keyCode.ToString());
        }
    }
}

