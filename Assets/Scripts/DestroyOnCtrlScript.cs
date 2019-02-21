using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCtrlScript : MonoBehaviour {

	GameObject atInstance;
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable()
	{
		atInstance = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1"))
			Destroy (atInstance);
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			Destroy (gameObject);
		}
	}
}
