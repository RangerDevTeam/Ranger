using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liangddyy.Util;
using Mono.Data.Sqlite;

public class TestDb : MonoBehaviour {

    TestCharactor c1;
    DbUtil dbUtil;
    // Use this for initialization
    void Start () {

        dbUtil = DbUtil.getInstance();
        dbUtil.CreateTable(new TestSkill());//建表
        dbUtil.CreateTable(new TestCharactor());

        dbUtil.Insert(new TestSkill() { skillID = 1, skillName = "技能1"});//插入

        c1 = new TestCharactor();
        c1.charactorName = "c1";
        dbUtil.Insert(c1);

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
       

        

       // dbUtil.CloseConnection();
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Y))
        {
            c1.charactorName = "change";
            dbUtil.Update(c1);
            dbUtil.CloseConnection();


            SqliteDataReader reader = dbUtil.ReadFullTable<TestCharactor>();
            while (reader.Read())
            {
                Debug.Log(reader.GetString(reader.GetOrdinal("charactorID")));
                Debug.Log(reader.GetString(reader.GetOrdinal("charactorName")));
            }

        }
		
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
