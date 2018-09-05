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
        SkillKeycodeKey loadedKeys = JsonFileDeal<SkillKeycodeKey>.ReadJsonFile("SkillKeycodeKey");
        if (loadedKeys == null)
        {
            CreateDefaultSkillKey();
        }
        else
        {
            for (int i = 0; i < loadedKeys.skillKeys.Count; i++)
            {
                CreateSkillKeyCode(loadedKeys.skillKeys[i].skillID, loadedKeys.skillKeys[i].keyCode);
            }
        }
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

    //创建技能键
    void CreateSkillKeyCode(uint skillID,KeyCode keyCode)
    {
        KeyBoardData t = new KeyBoardData();
        t.skillID = skillID;
        t.keyCode = keyCode;
        CreateDic(t.keyCode.ToString(),t);
    }

    //获取技能
    public uint GetSkillId(KeyCode keyCode)
    {
        string keyCodeName = keyCode.ToString();
        //如果存在技能
        if (skillKeyCodeDic.ContainsKey(keyCodeName))
            return skillKeyCodeDic[keyCodeName].skillID;
        else
            return 0;
    }

    //创建默认技能键
    void CreateDefaultSkillKey()
    {
        CreateSkillKeyCode(1, KeyCode.Q);
        CreateSkillKeyCode(2, KeyCode.R);
        CreateSkillKeyCode(3, KeyCode.C);

        SaveSkillKey();
    }

    //修改技能键
    public void ChangeSkillKey(uint skillID, KeyCode keyCode)
    {
        if (skillKeyCodeDic[keyCode.ToString()].skillID == skillID)
            return;
        bool haveKey = false;
        string oldKey = null;
        foreach (string key in skillKeyCodeDic.Keys)
        {
            if (skillKeyCodeDic[key].skillID == skillID)
            {
                oldKey = key;
                haveKey = true;
            }
        }
        //传入的技能已有技能键
        if (haveKey)
        {
            //传入的键已设置技能,则两个键交换其链接的技能
            if (skillKeyCodeDic.ContainsKey(keyCode.ToString()))
            {
                skillKeyCodeDic[oldKey].skillID = skillKeyCodeDic[keyCode.ToString()].skillID;
                skillKeyCodeDic[keyCode.ToString()].skillID = skillID;
            }
            //传入的键没设置技能，则传入的键链接技能，原按键取消链接
            else
            {
                CreateSkillKeyCode(skillID, keyCode);
                skillKeyCodeDic.Remove(oldKey);
            }
        }
        //传入的技能没有技能键
        else
        {
            //传入的键已设置技能，则去掉原技能的链接，传入的键链接传入的技能
            if (skillKeyCodeDic.ContainsKey(keyCode.ToString()))
            {
                skillKeyCodeDic.Remove(keyCode.ToString());
                CreateSkillKeyCode(skillID, keyCode);
            }
            //传入的键没设置技能，直接创建新链接
            else
            {
                CreateSkillKeyCode(skillID, keyCode);
            }
        }
    }

    //保存键位
    public void SaveSkillKey()
    {
        SkillKeycodeKey key = new SkillKeycodeKey();
        foreach (KeyBoardData t in skillKeyCodeDic.Values)
        {
            key.skillKeys.Add(t);
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
        public List<KeyBoardData> skillKeys = new List<KeyBoardData>();
    }
}
