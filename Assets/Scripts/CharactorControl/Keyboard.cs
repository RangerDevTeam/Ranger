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

    Dictionary<string, KeyBoardData> skillKeyCodeDic = new Dictionary<string, KeyBoardData>();

    static Keyboard instance = new Keyboard();

    public Keyboard()
    {
        CreateDefaultSkillKey();
        SaveSkillKey();
        //根据数组排序来确定绑定的技能ID
        //for (int i = 0; i < skillKeyCode.Count; i++)
        //{
        //    CreateDic(skillKeyCode[i].ToString(), (uint)(i + 1));
        //}
    }

    public static Keyboard Instance
    {
        get { return instance; }
    }

    //创建字典
    public void CreateDic(string keyCodeName, KeyBoardData data)
    {
        //如果存在，则不处理
        if (skillKeyCodeDic.ContainsKey(keyCodeName)) return;
        skillKeyCodeDic.Add(keyCodeName, data);
    }

    //获取技能
    public uint GetSkillId(string keyCodeName)
   {
        //如果存在技能
        if (skillKeyCodeDic.ContainsKey(keyCodeName))
            return skillKeyCodeDic[keyCodeName].skillID;
        else
            return 0;
    }

    //创建技能键
    void CreateSkillKeyCode(uint skillID,KeyCode keyCode)
    {
        KeyBoardData t = new KeyBoardData();
        t.skillID = skillID;
        t.keyCode = keyCode;
        CreateDic(t.keyCode.ToString(),t);

        //JsonFileDeal<KeyBoardData>.WriteJsonFile(t, "KeyBoardData");
    }

    //创建默认键
    void CreateDefaultSkillKey()
    {
        CreateSkillKeyCode(1, KeyCode.Q);
        CreateSkillKeyCode(2, KeyCode.R);
        CreateSkillKeyCode(3, KeyCode.C);
    }

    //保存键位
    void SaveSkillKey()
    {
        SkillKeycodeKey key = new SkillKeycodeKey();
        foreach (KeyBoardData t in skillKeyCodeDic.Values)
        {
            key.skillKey.Add(t);
            Debug.Log(key.skillKey);
        }

        JsonFileDeal<SkillKeycodeKey>.WriteJsonFile(key, "SkillKeycodeKey");
    }

    public class KeyBoardData
    {
        public uint skillID;
        public KeyCode keyCode;

        public KeyBoardData()
        {

        }
    }

    public class SkillKeycodeKey
    {
        public List<KeyBoardData> skillKey = new List<KeyBoardData>();
    }
}
