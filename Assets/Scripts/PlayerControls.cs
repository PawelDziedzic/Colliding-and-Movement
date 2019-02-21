using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public float nOfLives;
	public GameObject theAttack;

	private bool isFacingLeft;
	private Quaternion rot;
	private RaycastHit hitInfo;
	private float rayDistance;
	private float downG;

	// Use this for initialization
	void Start () {
		isFacingLeft = false;
		rayDistance = 0.76f;
		rot = new Quaternion (transform.rotation.x + 90f, transform.rotation.y, transform.rotation.z, transform.rotation.w + 90f);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) 
			DoAttack();

		if (Input.GetButtonDown ("Jump")) {
			if (isGrounded()) {
				transform.Translate (Vector3.up * 0.2f);
				downG = -0.8f;
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			if (!isFacingLeft){
				transform.Rotate (new Vector3 (0f, 180f, 0));
				isFacingLeft = true;
			}
			HorizontalMovement (Vector3.right);
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			if (isFacingLeft) {
				transform.Rotate (new Vector3 (0f, 180f, 0));
				isFacingLeft = false;
			}
			HorizontalMovement (Vector3.right);
		}
	}

	void HorizontalMovement(Vector3 dirVector)
	{
		if (isGrounded ())
			transform.Translate (dirVector * 0.1f);
		else
			transform.Translate (dirVector * 0.05f);
	}

	void FixedUpdate()
	{
		VerticalMovement ();
	}

	void VerticalMovement()
	{
		if (isGrounded ()) {
			downG = 0f;
		} else {
			downG += 0.02f;
		}
		transform.Translate(Vector3.down*downG);
		Debug.DrawRay (transform.position, Vector3.down*(rayDistance+downG), Color.magenta);
		Debug.DrawRay (transform.position, Vector3.left, Color.magenta);
		Debug.DrawRay (transform.position, Vector3.right, Color.magenta);
	}

	bool isGrounded()
	{
		return Physics.Raycast (transform.position, Vector3.down, out hitInfo, rayDistance+downG);
	}

	void DoAttack()
	{
		float xOutset;
		if (isFacingLeft)
			xOutset = transform.position.x - 2f;
		else
			xOutset = transform.position.x + 2f;

		Instantiate (theAttack, new Vector3 (xOutset,transform.position.y,transform.position.z), rot);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "EnemyAttack")
			Destroy (gameObject);
	}

}