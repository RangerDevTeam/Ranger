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

    //初始化所有属性，单位的属性从文件中读取后写入这里
    public AttributeData(UnitData unitData)
    {

        AttributeKeys attkeys = JsonFileDeal<AttributeKeys>.ReadJsonFile("AttributeKey");
        if (attkeys == null)
        {
            Debug.LogError("没读取到AttributeKey文件");
            return;
        }
        foreach (Attribute att in Enum.GetValues(typeof(Attribute)))
        {
            for (int i = 0; i < attkeys.singleKeys.Count; i++)
            {
                CreateDic(unitAttData, attkeys.singleKeys[i]);
                CreateDic(equipAttData, attkeys.singleKeys[i]);
                CreateDic(buffAttData, attkeys.singleKeys[i]);
                CreateDic(baseAttData, attkeys.singleKeys[i]);
                CreateDic(baseAndEquipAttData, attkeys.singleKeys[i]);
            
            }

        }

        //加载基础属性（未实现，需读取文件）

     
        Debug.Log(attkeys.singleKeys.Count);
        for (int i = 0; i < attkeys.singleKeys.Count; i++)
        {
            Debug.Log(attkeys.singleKeys[i].attName);
        }

    }

    //创建并添加一个元素
    public void CreateDic(Dictionary<string, AttributeSingleData> dictionary,AttributeSingleData single)
    {
        //如果已经存在，则不处理
        if (dictionary.ContainsKey(single.attID.ToString())) return;
        AttributeSingleData attSingle = new AttributeSingleData(single.attID);
        attSingle.attName = single.attName;
        attSingle.minValue = single.minValue;
        attSingle.maxValue = single.maxValue;
        attSingle.attValue = 0;
        //以ID作为key
        dictionary.Add(single.attID.ToString(),attSingle);
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
            CreateDic(final, other[key]);
            final[key].attValue += other[key].attValue;
        }
    }

    //通过ID修改属性---按值变化
    void ChangeAttAsNumber(Dictionary<string, AttributeSingleData> dictionary,uint id,int value)
    {
        CreateDic(dictionary, new AttributeSingleData(id));//没有就创建一个属性
        dictionary[id.ToString()].attValue += value;
    }

    //通过名字修改属性---按百分比变化
    int ChangeAttAsPercent(Dictionary<string, AttributeSingleData> dictionary, uint id, int value)
    {
        CreateDic(dictionary, new AttributeSingleData(id));//没有就创建一个属性
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
        CreateDic(dictionary, new AttributeSingleData(id));//没有就创建一个属性
        dictionary[id.ToString()].minValue = minValue;
        dictionary[id.ToString()].maxValue = maxValue;
    }



}


public class AttributeSingleData
{
    //初始化的时候生成，不允许修改
    public  uint attID = 999;
    public int attributeValue;
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

    public AttributeSingleData()
    { 
    }


}


public class AttributeKeys
{
    public List<AttributeSingleData> singleKeys = new List<AttributeSingleData>();
}

