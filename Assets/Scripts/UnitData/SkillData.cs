using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData {

    public uint skillId;
    public uint skillLevel;

    public enum ActiveType
    {
        active =0 ,//主动技能
        passive =1//被动技能

    }

    public ActiveType activeType = new ActiveType();
    //技能属于哪个单位
    public UnitData uniData;

    public string animationName;

    //不写等级则默认1级
    public SkillData(UnitData _unitData,uint _skillID)
    {
        if (_unitData == null) Debug.LogError(_unitData + "不存在");
        uniData = _unitData;
        skillId = _skillID;
        skillLevel = 1;
    }


    public SkillData(UnitData _unitData, uint _skillID,uint _skillLevel)
    {
        if (_unitData == null) Debug.LogError(_unitData + "不存在");
        uniData = _unitData;
        skillId = _skillID;
        skillLevel = _skillLevel;
    }


    public void UseActiveSkill()
    {
        if (uniData.PlayerStateMachine.CurrentStateID == (uint)UnitData.UnitStateType.attack) return;//当前已经是attack状态则不转换为attack状态
       uniData.PlayerStateMachine.SwitchState((uint)UnitData.UnitStateType.attack, null, null);
        AnimationPlay.Play(uniData,animationName,false);
        uniData.animationData.skeletonAnimation.state.Complete += ReturnToIdle;

    }

    public void ReturnToIdle(Spine.TrackEntry trackEntry)
    {
        uniData.PlayerStateMachine.SwitchState((uint)UnitData.UnitStateType.idle,null,null);
    }

    void LoadSkillData()
    {

    }


}