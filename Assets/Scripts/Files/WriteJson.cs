using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public static class JsonFileDeal<T>{

    static string path = Application.dataPath + "/Json";

    public static bool WriteJsonFile(T jsonObj,string saveName)
    {
        string savePath = path + "/" + saveName+".json";

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        string json = JsonUtility.ToJson(jsonObj);
        File.WriteAllText(savePath,json);
        return true;
    }

    public static T ReadJsonFile(string fileName)
    {
        string filePath = path + "/" + fileName + ".json";
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("[ 文件不存在! 取消读取操作! ]  文件读取路径 : " + path);
            return default(T);
        }
        //Debug.Log(filePath);
        JsonReader reader = new JsonReader(File.ReadAllText(filePath));
       // Debug.LogError(reader);
        T obj = JsonMapper.ToObject<T>(reader);
       // Debug.LogError("obj is    " + obj);
        return obj;
    }



}
