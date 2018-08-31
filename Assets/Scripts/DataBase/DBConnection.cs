using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBConnection{

    static DBConnection instance;
    public static DBConnection Instance
    {
        get { return instance ?? (instance = new DBConnection()); }
    }

    RangerDataBase.SQLiteHelper database = null;

    public DBConnection()
    {
        database = new RangerDataBase.SQLiteHelper("data source=rangerDataBase.db");
    }

    public bool CreateTable<T>(T obj)
    {
        return false;
    }


}
