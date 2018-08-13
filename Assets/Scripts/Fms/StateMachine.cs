  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;
  
  public class StateMachine {
       //使用字典 存储状态
       private Dictionary<uint, IState> mDictionaryState;
       //当前状态
       private IState mCurrentState;
  
      public uint CurrentStateID //获取当前状态机ID
      {
          get
          {
             return mCurrentState == null ? 0 : mCurrentState.GetStateID();
         }
      }
  
     public IState CurrentState //获取当前状态机
     {
         get
          {
              return mCurrentState;
          }
      }
  
      public StateMachine() // 构造函数初始化
      {
          mDictionaryState = new Dictionary<uint, IState>();
          mCurrentState = null;
      }
  
      public bool RegisterState(IState state) //添加状态
     {
          if (state == null)
          {
              return false;
          }
          if (mDictionaryState.ContainsKey(state.GetStateID()))
          {
              return false;
          }
          mDictionaryState.Add(state.GetStateID(), state);
          return true;
      }
  
      public IState GetState(uint stateID)//获取状态
      {
          IState state = null;
         mDictionaryState.TryGetValue(stateID, out state);
          return state;
      }
  
      public void StopState(object param1, object param2)//停止状态
      {
          if (mCurrentState == null)
          {
              return;
          }
          mCurrentState.OnLeave(null, param1, param2);
          mCurrentState = null;
     }
  
      public bool UnRegisterState(uint StateID)//取消状态注册
     {
          if (!mDictionaryState.ContainsKey(StateID))
          {
              return false;
          }
          if (mCurrentState != null && mCurrentState.GetStateID() == StateID)
          {
              return false;
          }
          mDictionaryState.Remove(StateID);
          return true;
      }
      
      //切换状态
  
      //切换状态的委托
      public delegate void BetweenSwitchState(IState from, IState to, object param1, object param2);
      //切换状态的回调
      public BetweenSwitchState BetweenSwitchStateCallBack = null;
      public bool SwitchState(uint newStateID, object param1, object param2)
      {
          //Debug.Log("当前状态为： " + mCurrentState);
          //当前状态切当前状态 false
          if (mCurrentState != null && mCurrentState.GetStateID() == newStateID)
          {
             return false;
          }
          //切换到没有注册过的状态 false
          IState newState = null;
         //Debug.Log("新状态ID " + newStateID);
          mDictionaryState.TryGetValue(newStateID, out newState);
         //Debug.Log("新状态 " + newState);
          if (newState == null)
         {
            return false;
          }
         // Debug.Log("新状态" + newState);
          // 退出当前状态
         if (mCurrentState != null)
         {
             mCurrentState.OnLeave(newState, param1, param2);
         }
 
         //状态切换间做的事
         IState oldState = mCurrentState;
         mCurrentState = newState;
        if (BetweenSwitchStateCallBack != null)
        {
             BetweenSwitchStateCallBack(oldState, mCurrentState, param1, param2);
         }
         //进入新状态
         newState.OnEnter(this, oldState, param1, param2);
         return true;
     }
 
     public bool IsState(uint stateID)  //当前是否在某个状态
     {
         return mCurrentState == null ? false : mCurrentState.GetStateID() == stateID;
     }
     public void OnUpdate()
     {
         if (mCurrentState != null)
         {
             mCurrentState.OnUpdate();
         }
     }
     public void OnFixedUpdate()
     {
         if (mCurrentState != null)
         {
             mCurrentState.OnFixedUpdate();
         }
     }
     public void OnLateUpdate()
     {
         if (mCurrentState != null)
         {
             mCurrentState.OnLateUpdate();
         }
     }
 }