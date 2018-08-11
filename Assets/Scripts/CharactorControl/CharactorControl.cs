using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(Keyboard.moveUp))
        {
            transform.Translate(Vector3.up * 2f * Time.deltaTime);
        }
		
	}
}
