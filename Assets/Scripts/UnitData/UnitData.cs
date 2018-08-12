using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class UnitData : MonoBehaviour {


    public StateMachine PlayerStateMachine = new StateMachine();
    public SkeletonAnimation skeletonAnimation;


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

	// Use this for initialization
    protected virtual void Start()
    {

        skeletonAnimation = FindObj.FindChild(this.gameObject,"Animation").GetComponent<SkeletonAnimation>();
		
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {

        PlayerStateMachine.OnUpdate();
		
	}
}
