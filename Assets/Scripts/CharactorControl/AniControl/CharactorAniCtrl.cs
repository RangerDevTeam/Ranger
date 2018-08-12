using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class CharactorAniCtrl : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
       // PlayerMove();
    }

    //角色移动和待机
    void PlayerMove()
    {
        SkeletonAnimation skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        //向左
        if (Input.GetKey(Keyboard.moveLeft))
        {
            skeletonAnimation.Skeleton.FlipX = true;
            if(skeletonAnimation.AnimationName != "run")
            {
                skeletonAnimation.state.SetAnimation(0, "run", true);
            }
                
        }
        //向右
        if (Input.GetKey(Keyboard.moveRight))
        {
            skeletonAnimation.Skeleton.FlipX = false;
            if (skeletonAnimation.AnimationName != "run")
            {
                skeletonAnimation.state.SetAnimation(0, "run", true);
            }
        }
        //向上
        if (Input.GetKey(Keyboard.moveUp))
        {
            if (skeletonAnimation.AnimationName != "run")
            {
                skeletonAnimation.state.SetAnimation(0, "run", true);
            }
        }
        //向下
        if (Input.GetKey(Keyboard.moveDown))
        {
            if (skeletonAnimation.AnimationName != "run")
            {
                skeletonAnimation.state.SetAnimation(0, "run", true);
            }
        }
        //待机
        if (Input.anyKey != true)
        {
            if (skeletonAnimation.AnimationName != "idle")
            {
                skeletonAnimation.state.SetAnimation(0, "idle", true);
            }
        }
    }
}
