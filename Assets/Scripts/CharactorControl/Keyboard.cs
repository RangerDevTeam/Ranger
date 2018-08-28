using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard {

    public static KeyCode moveUp = KeyCode.W;
    public static KeyCode moveLeft = KeyCode.A;
    public static KeyCode moveRight = KeyCode.D;
    public static KeyCode moveDown = KeyCode.S;
    public static KeyCode attack = KeyCode.J;
    public static List<KeyCode> skillKeyCode = new List<KeyCode>();

    Dictionary<string, uint> skillKeyCodeData = new Dictionary<string, uint>();

    static Keyboard instance = new Keyboard();

    public Keyboard()
    {
        CreateSkillKeyCode();
        //根据数组排序来确定绑定的技能ID
        for (int i = 0; i < skillKeyCode.Count; i++)
        {
            CreateDic(skillKeyCode[i].ToString(), (uint)(i + 1));
        }
    }

    public static Keyboard Instance
    {
        get { return instance; }
    }

    //创建字典
    public void CreateDic(string keyCodeName, uint _skillID)
    {
        //如果存在，则不处理
        if (skillKeyCodeData.ContainsKey(keyCodeName)) return;
        skillKeyCodeData.Add(keyCodeName, _skillID);
    }

    //获取技能
    public uint GetSkillId(string keyCodeName)
   {
        //如果存在技能
        if (skillKeyCodeData.ContainsKey(keyCodeName))
            return skillKeyCodeData[keyCodeName];
        else
            return 0;
    }

    //创建技能键
    public void CreateSkillKeyCode()
    {
        KeyCode skill1 = KeyCode.Q;
        skillKeyCode.Add(skill1);

        KeyCode skill2 = KeyCode.C;
        skillKeyCode.Add(skill2);
    }
}
