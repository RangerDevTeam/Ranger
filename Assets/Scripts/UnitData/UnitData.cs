using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class UnitData : MonoBehaviour {


    public StateMachine PlayerStateMachine = new StateMachine();
    public AnimationData animationData;
    public AttributeData mAttData;

    public enum UnitStateType : uint
    {
        idle =0,
        move = 1,
        hurt = 2,
        attack = 3,
        die = 4,
        collect = 5,
        eat = 6
    }


    public List<SkillData> mySkillDatas = new List<SkillData>();



	// Use this for initialization
    protected virtual void Start()
    {
        animationData = new AnimationData(FindObj.FindChild(this.gameObject, "Animation").GetComponent<SkeletonAnimation>());
        mAttData = new AttributeData(this);

        CreateSKillData();

    }
	
	// Update is called once per frame
    protected virtual void Update()
    {

        PlayerStateMachine.OnUpdate();
		
	}


    public void CreateSKillData()
    {
        SkillData normal = new SkillData(this,1,1);
        mySkillDatas.Add(normal);
        normal.animationName = "meleeSwing1";
        SkillData firstSkill = new SkillData(this,100,1);
        mySkillDatas.Add(firstSkill);
        firstSkill.animationName = "meleeSwing2";
        SkillData rollSkill = new SkillData(this, 2, 1);
        mySkillDatas.Add(rollSkill);
        rollSkill.animationName = "roll";
    }


}
