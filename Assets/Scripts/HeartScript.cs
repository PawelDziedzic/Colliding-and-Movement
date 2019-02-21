using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour {

	Rigidbody rb;
	private GameObject parent;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "EnemyAttack")
			Destroy (gameObject);
		else{
		rb.velocity = Vector3.zero;
		rb.freezeRotation = true;
		rb.ResetInertiaTensor ();
		transform.position = transform.parent.position;
		}
	}

	void OnCollisionExit()
	{
		rb.velocity = Vector3.zero;
		rb.freezeRotation = true;
		rb.ResetInertiaTensor ();
		transform.position = transform.parent.position;
	}
}
