using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventControl {

    static EventControl instance;

    public static EventControl Instance
    {
        get { return instance ?? (instance = new EventControl()); }
    }

    public delegate void ChangeKeyHandler(KeyCode keyCode);
    public event ChangeKeyHandler ChangeKeyEvent;

    public void SendChangeKeyEvent(KeyCode keyCode)
    {
        if (ChangeKeyEvent != null)
            ChangeKeyEvent(keyCode);
    }
}
