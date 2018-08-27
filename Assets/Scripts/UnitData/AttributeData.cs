using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class AttributeData
{
   Dictionary<string,AttributeSingleData> unitAttData  = new Dictionary<string, AttributeSingleData>();//总属性
   Dictionary<string, AttributeSingleData> equipAttData = new Dictionary<string, AttributeSingleData>();//装备属性
   Dictionary<string, AttributeSingleData> buffAttData = new Dictionary<string, AttributeSingleData>();//buff属性
   Dictionary<string, AttributeSingleData> baseAttData = new Dictionary<string, AttributeSingleData>();//基础属性
   Dictionary<string, AttributeSingleData> baseAndEquipAttData = new Dictionary<string, AttributeSingleData>();//装备+基础，为了某些BUFF可以影响到装备和基础


    //设置枚举，用于初始化属性
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

    //初始化所有属性，单位的属性从文件中读取后写入这里
    public AttributeData(UnitData unitData)
    {
        foreach (Attribute att in Enum.GetValues(typeof(Attribute)))
        {
            CreateDic(unitAttData, (uint)att.GetHashCode(),att.ToString());
            CreateDic(equipAttData, (uint)att.GetHashCode(), att.ToString());
            CreateDic(buffAttData, (uint)att.GetHashCode(), att.ToString());
            CreateDic(baseAttData, (uint)att.GetHashCode(), att.ToString());
        }

        //加载基础属性（未实现，需读取文件）



        //foreach (string key in baseAttData.Keys)
        //{
        //    Debug.Log(key + "  " + baseAttData[key].attValue);
        //}
    }

    //创建并添加一个元素
    public void CreateDic(Dictionary<string, AttributeSingleData> dictionary,uint attID,string name)
    {
        //如果已经存在，则不处理
        if (dictionary.ContainsKey(attID.ToString())) return;
        AttributeSingleData attSingle = new AttributeSingleData(attID);
        attSingle.attName = name;
        attSingle.attValue = 0;
        //以ID作为key
        dictionary.Add(attID.ToString(),attSingle);
    }


    //将所有属性合并到总属性中
    void MergeaLLAttribute()
    {
        //先合并装备+基础
        MergeAttribute(baseAndEquipAttData,baseAttData);
        MergeAttribute(baseAndEquipAttData, equipAttData);
        //把所有的合并到总属性 
        MergeAttribute(unitAttData,buffAttData);
        MergeAttribute(unitAttData, baseAndEquipAttData);
    }

    //将另外一个属性集合并到前一个属性集
    void MergeAttribute(Dictionary<string, AttributeSingleData> final, Dictionary<string, AttributeSingleData> other)
    {
        foreach (string key in other.Keys)
        {
            //尝试在final中创建,已经存在则直接相加
            CreateDic(final, other[key].attID, other[key].attName);
            final[key].attValue += other[key].attValue;
        }
    }

    //通过ID修改属性---按值变化
    void ChangeAttAsNumber(Dictionary<string, AttributeSingleData> dictionary,uint id,int value)
    {
        CreateDic(dictionary, id, "新属性");//没有就创建一个属性
        dictionary[id.ToString()].attValue += value;
    }

    //通过名字修改属性---按百分比变化
    int ChangeAttAsPercent(Dictionary<string, AttributeSingleData> dictionary, uint id, int value)
    {
        CreateDic(dictionary, id, "新属性");//没有就创建一个属性
        dictionary[id.ToString()].attValue = (int)(dictionary[id.ToString()].attValue * ((float)(100 + value)/100));
        return dictionary[id.ToString()].attValue;
    }

    //设置所有字典某个属性的区间
    public void SetMaxValue(uint id,int minValue,int maxValue)
    {
        SetMaxVlue(unitAttData,id,minValue,maxValue);
        SetMaxVlue(equipAttData, id, minValue, maxValue);
        SetMaxVlue(buffAttData, id, minValue, maxValue);
        SetMaxVlue(baseAttData, id, minValue, maxValue);
        SetMaxVlue(baseAndEquipAttData, id, minValue, maxValue);
    }

    //为相关的字典设置属性范围
    void SetMaxVlue(Dictionary<string, AttributeSingleData> dictionary,uint id,int minValue,int maxValue)
    {
        CreateDic(dictionary, id, "新属性");//没有就创建一个属性
        dictionary[id.ToString()].minValue = minValue;
        dictionary[id.ToString()].maxValue = maxValue;
    }



}


public class AttributeSingleData
{
    //初始化的时候生成，不允许修改
    public readonly uint attID = 999;
    private int attributeValue;
    public int attValue
    {
        get { return attributeValue; }
        set {

            attributeValue = value;
            attributeValue = Math.Max(minValue,attributeValue);
            attributeValue = Math.Min(maxValue, attributeValue);
        }
    }
    public int maxValue = 0;
    public int minValue = 0;
    public string attName = "不存在";
    public AttributeSingleData(uint id)
    {
        attID = id;
    }


}
