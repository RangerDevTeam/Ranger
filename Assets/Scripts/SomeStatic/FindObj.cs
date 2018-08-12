using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FindObj {

    //查找物体的子结构中指定名字的物体
   public  static GameObject FindChild(GameObject father,string childName)
    {
        foreach (Transform trans in father.transform)
        {
            if (trans.name == childName)
                return trans.gameObject;
        }
        return null;
    }
}
