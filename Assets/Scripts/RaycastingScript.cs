using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingScript : MonoBehaviour {

	private RaycastHit hitInfo;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.DrawRay (transform.position, Vector3.right*100f, Color.cyan);
		if (Physics.Raycast (transform.position, Vector3.right, out hitInfo,100f)) {
			Debug.DrawRay (hitInfo.point, hitInfo.normal*100f, Color.magenta);
		}
	}
}
