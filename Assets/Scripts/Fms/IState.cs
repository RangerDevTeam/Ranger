using UnityEngine;
using System.Collections;

public interface IState{
 //获取状态机状态
 uint GetStateID();
  
 //void OnEnter();
 //void OnLeave();
 //等待补全
    void OnEnter(StateMachine machine,IState preState,object param1,object param2);
    void OnLeave(IState nextState,object param1,object param2);
//Unity 生命周期
    void OnUpdate();
    void OnFixedUpdate();
    void OnLateUpdate();
 }