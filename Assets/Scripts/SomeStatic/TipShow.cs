using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.All)]
public class IsDataBaseKeyAttribute : Attribute {

    public bool IsDataKey
    {
        get { return true; }
    }
    
}
