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
    public KeyCode getKeyDownCode()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && PlayerStateMachine.CurrentStateID == (uint)UnitStateType.idle)
                {
                    uint skillID = Keyboard.Instance.GetSkillId(keyCode.ToString());
                    for (int i = 0; i < mySkillDatas.Count; i++)
                    {
                        if (mySkillDatas[i].skillId == skillID)
                        {
                            mySkillDatas[i].UseActiveSkill();
                            return keyCode;
                        }
                    }
                    return keyCode;
                }
            }
        }
        return KeyCode.None;
    }
}

