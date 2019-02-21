using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentFloorScript : MonoBehaviour {

	public GameObject surface;
	public GameObject surface2;
	private GameObject floor;

	// Use this for initialization
	void Start () {
		floor = Instantiate (surface, transform.position, Quaternion.identity) as GameObject;
		floor.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.UpArrow)) {
			Destroy (floor);
			floor = Instantiate(surface2, transform.position, Quaternion.identity) as GameObject;
			floor.transform.parent = transform;
		}
		
	}
}
