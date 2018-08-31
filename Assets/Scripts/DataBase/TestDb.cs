using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liangddyy.Util;

public class TestDb : MonoBehaviour {

	// Use this for initialization
	void Start () {

        DbUtil dbUtil = DbUtil.getInstance();
        dbUtil.CreateTable(new TestSkill());//建表
        dbUtil.CreateTable(new TestCharactor());

        dbUtil.Insert(new TestSkill() { skillID = 1, skillName = "技能1"});//插入
        dbUtil.Insert(new TestCharactor() { charactorName = "charactor"});

        List<TestSkill> penList = new List<TestSkill>();
        List<TestCharactor> personList = new List<TestCharactor>();
        for (int i = 5; i < 20; i++)
        {
            TestSkill pen = new TestSkill() { skillID = i, skillName = "shill"+i.ToString()};
            TestCharactor person = new TestCharactor() { charactorName = "charactor "+i.ToString()};
            personList.Add(person);
            penList.Add(pen);
        }
        dbUtil.InsertList(penList);//插入链表
        dbUtil.InsertList(personList);
        dbUtil.Update(new TestCharactor() {charactorName = "hh"});

        dbUtil.CloseConnection();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class TestSkill
{
    [IsDataBaseKey()]
    public int skillID =1;
    public string skillName ="";
}

public class TestCharactor
{
    [IsDataBaseKey()]
    public string charactorID;
    public string charactorName ="";

    public TestCharactor()
    {
        charactorID = System.Guid.NewGuid().ToString("N");
    }

}
