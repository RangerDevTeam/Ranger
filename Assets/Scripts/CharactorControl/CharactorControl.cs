using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorControl : UnitData {



    void Awake()
    {
        PlayerStateMachine.RegisterState(new MoveState(this));
        PlayerStateMachine.RegisterState(new IdleState(this));
    }

	// Use this for initialization
	protected override void Start () {

        base.Start();

        PlayerStateMachine.SwitchState((uint)UnitStateType.idle,null,null);
		
	}
	
	// Update is called once per frame
	protected override void Update () {


        base.Update();
       // ControlPlayer();

	}


    //要考虑下是否有更好的写法
    //通过输入的按键控制玩家
    void ControlPlayer()
    {
        //向上走
        if (Input.GetKey(Keyboard.moveUp))
        {
            transform.Translate(Vector3.up * 2f * Time.deltaTime);
        }
        //向下走
        if (Input.GetKey(Keyboard.moveDown))
        {
            transform.Translate(Vector3.down * 2f * Time.deltaTime);
        }
        //向左走
        if (Input.GetKey(Keyboard.moveLeft))
        {
            transform.Translate(Vector3.left * 2f * Time.deltaTime);
        }
        //向右走
        if (Input.GetKey(Keyboard.moveRight))
        {
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
        }
		
    }

}
