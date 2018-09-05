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
            CreateDefaultKey();
        }
        else
        {
            for (int i = 0; i < loadedKeys.skillKeys.Count; i++)
            {
                CreateKeyBoardData(loadedKeys.skillKeys[i].skillID, loadedKeys.skillKeys[i].keyCode, loadedKeys.skillKeys[i].keyCodeType);
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

    //创建新按键功能类
    void CreateKeyBoardData(uint skillID,KeyCode keyCode,string keyCodeType)
    {
        KeyBoardData t = new KeyBoardData();
        t.skillID = skillID;
        t.keyCode = keyCode;
        t.showKeyCode = keyCode;
        t.keyCodeType = keyCodeType;
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
    void CreateDefaultKey()
    {
        CreateKeyBoardData(0,KeyCode.A,"moveLeft");
        CreateKeyBoardData(0, KeyCode.D, "moveRight");
        CreateKeyBoardData(0, KeyCode.S, "moveDown");
        CreateKeyBoardData(0, KeyCode.W, "moveUp");
        CreateKeyBoardData(0, KeyCode.Space, "pickUp");

        CreateKeyBoardData(1, KeyCode.Q,"skill");
        CreateKeyBoardData(2, KeyCode.R,"skill");
        CreateKeyBoardData(3, KeyCode.C,"skill");

        SaveKey();
    }

    //修改按键（UI表面上的）
    public void ChangeKey(KeyCode keyCode,KeyBoardData t)
    {
        t.showKeyCode = keyCode;
        string keyCodeName = keyCode.ToString();
        //要设置的按键原先有功能类
        if (skillKeyCodeDic.ContainsKey(keyCodeName))
        {
            //将该功能的按键设为空值
            skillKeyCodeDic[keyCodeName].showKeyCode = KeyCode.None;
        }
    }

    //保存键位修改配置
    public void SaveChangeKey(KeyBoardData t)
    {
        string keyCodeName = t.keyCode.ToString();
        //如果字典里的原键位上仍是自己，则去掉
        if (skillKeyCodeDic[keyCodeName] == t)
            skillKeyCodeDic.Remove(keyCodeName);

        //将键位改成预先设置的键位
        t.keyCode = t.showKeyCode;
        keyCodeName = t.keyCode.ToString();
        
        CreateDic(keyCodeName, t);//如果字典里没有该键位则创建一个新键位
        skillKeyCodeDic[keyCodeName] = t;
    }

    //将键位写入Json
    public void SaveKey()
    {
        SkillKeycodeKey key = new SkillKeycodeKey();
        //foreach (KeyBoardData t in skillKeyCodeDic.Values)
        //{
        //    key.skillKeys.Add(t);
        //    Debug.Log(t.keyCode + "++" + t.showKeyCode );
        //}

        JsonFileDeal<SkillKeycodeKey>.WriteJsonFile(key, "SkillKeycodeKey");
    }

    public class KeyBoardData
    {
        public uint skillID;
        public KeyCode keyCode;
        public KeyCode showKeyCode;
        public string keyCodeType;

        public KeyBoardData()
        {
            EventControl.Instance.ChangeKeyEvent += new EventControl.ChangeKeyHandler(JudgeKey);
        }

        public void JudgeKey(KeyCode changeKeyCode)
        {
            if (keyCode == changeKeyCode)
            {
                Keyboard.Instance.ChangeKey(changeKeyCode,this);
            }
        }
    }

    public class SkillKeycodeKey
    {
        public List<KeyBoardData> skillKeys = new List<KeyBoardData>();
    }
}
