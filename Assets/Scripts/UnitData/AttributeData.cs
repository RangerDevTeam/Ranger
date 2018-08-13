using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttributeData
{

    public List<AttributeSingleData> unitAttData = new List<AttributeSingleData>();

    public enum Attribute : uint
    {
        attack = 0,//攻击
        defence = 1,//防御
        defencePercent = 2,//防御百分比
        health = 3,//生命
        spirit = 4,//精神
        temperature = 5,//温度
        hunger = 6,//饥饿
        thirsty = 7 //口渴

    }

    public AttributeData(UnitData unitData)
    {
        foreach (Attribute att in Enum.GetValues(typeof(Attribute)))
        {
            AttributeSingleData attSingle = new AttributeSingleData((uint)att.GetHashCode());
            attSingle.attName = att.ToString();
            attSingle.attValue = 0;
            unitAttData.Insert(att.GetHashCode(), attSingle);
        }

        //for (int i = 0; i < unitAttData.Count; i++)
        //{
        //    Debug.Log("ID为" + i + "的属性为：" + unitAttData[i].attName + "   属性值为：" + unitAttData[i].attValue);
        //}
    }


}


public class AttributeSingleData
{
    //初始化的时候生成，不允许修改
    private uint attID = 999;
    public uint attValue = 0;
    public string attName = "不存在";
    public AttributeSingleData(uint id)
    {
        attID = id;
    }


}
