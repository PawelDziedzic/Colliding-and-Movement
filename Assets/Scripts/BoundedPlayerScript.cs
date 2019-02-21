using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedPlayerScript : MonoBehaviour {

	public float speed;
	public float gForce;

	private Vector3 movement3D;
	private Vector3 newMovement;

	private Vector3 arrowMovement;
	private float groundingRayDistance;
	private RaycastHit hitInfo;
	private bool isFacingLeft;
	private Vector3 prunedVector;

	private List<Ray> loopRays;

	void Start () {
		movement3D = Vector3.zero;
		newMovement = Vector3.zero;
		arrowMovement = Vector3.zero;
		groundingRayDistance = transform.localScale.y+0.02f;
		isFacingLeft = false;
	}

	void Update () {
		arrowMovement.x=0f;
		if (Input.GetButtonDown ("Jump")) {
			transform.Translate (2*Vector3.up);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			if (!isFacingLeft){
				transform.Rotate (new Vector3 (0f, 180f, 0));
				isFacingLeft = true;
			}
			arrowMovement.x = -speed;
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			if (isFacingLeft) {
				transform.Rotate (new Vector3 (0f, 180f, 0));
				isFacingLeft = false;
			}
			arrowMovement.x = speed;
		}
		//arrowMovement.x = 0.02f;
	}

	void FixedUpdate()
	{
		transform.Translate (movement3D,Space.World);
		ApplyGravity();
		Debug.DrawRay (transform.position, arrowMovement, Color.blue);
		movement3D += arrowMovement;

		do{
			newMovement = movement3D;
			ThreePointShortening();
		}while(!movement3D.Equals(vectorCutOnMargin(newMovement)));

		Debug.DrawRay (transform.position + new Vector3 (0f, 0.74f, 0f), movement3D, Color.magenta);
		Debug.DrawRay (transform.position, movement3D, Color.cyan);
		Debug.DrawRay (transform.position + new Vector3 (0f, -0.74f, 0f), movement3D, Color.yellow);
	}

	void ApplyGravity()
	{
		movement3D.y -= gForce;
		//movement3D.x += speed;
		Debug.DrawRay (transform.position, Vector3.down * movement3D.y, Color.blue);
	}

	bool isGrounded()
	{
		return Physics.Raycast (transform.position, Vector3.down, out hitInfo, groundingRayDistance+Mathf.Abs(movement3D.y));
	}

	void ThreePointShortening()
	{
		movement3D = vectorCutOnMargin(
			getShortestVector(ShorteningFromHead(),
				getShortestVector(ShorteningFromPoint(Vector3.zero,movement3D),ShorteningFromPoint(new Vector3(0f,-0.74f,0f),movement3D))));
	}

	Vector3 vectorCutOnMargin(Vector3 vec)
	{
		if (vec.magnitude >= 0.005f) {
			return vec-vec.normalized*0.0025f;
		} else {
			return Vector3.zero; 
		}
	}

	Vector3 getShortestVector(Vector3 vec1, Vector3 vec2)
	{
		if (vec1.magnitude > vec2.magnitude)
			return vec2;
		else
			return vec1;
	}

	Vector3 ShorteningFromHead()
	{
		///doesnt work upon flipping
		/// casts rays opposite to actual movement
		/// on x axis
		/// but not on y axis
		/// huh
		if (Physics.Raycast (transform.position + new Vector3 (0f, 0.74f, 0f), movement3D.normalized, out hitInfo, movement3D.magnitude)) {
			Debug.Log ("I hit something!(head) " + hitInfo.collider.name);
			Physics.Raycast (transform.position + movement3D+ new Vector3 (0f, 0.74f, 0f)+hitInfo.normal*movement3D.magnitude, -hitInfo.normal, out hitInfo, movement3D.magnitude);
			drawCross (hitInfo.point, Color.magenta);
			return hitInfo.point-transform.position-new Vector3 (0f, 0.75f, 0f);
		} else {
			Debug.Log ("I hit nothing!(head)");
			return movement3D;
		}
	}

	Vector3 ShorteningFromCore()
	{
		///doesnt work upon flipping
		/// casts rays opposite to actual movement
		/// on x axis
		/// but not on y axis
		/// huh
		if (Physics.Raycast (transform.position, movement3D.normalized, out hitInfo, movement3D.magnitude)) {
			Debug.Log ("I hit something!(core) " + hitInfo.collider.name);
			Physics.Raycast (transform.position + movement3D+hitInfo.normal*movement3D.magnitude, -hitInfo.normal, out hitInfo, movement3D.magnitude);
			drawCross (hitInfo.point, Color.cyan);
			return hitInfo.point-transform.position;
		} else {
			Debug.Log ("I hit nothing!(core)");
			return movement3D;
		}
	}

	Vector3 ShorteningFromBottom()
	{
		///doesnt work upon flipping
		/// casts rays opposite to actual movement
		/// on x axis
		/// but not on y axis
		/// huh
		if (Physics.Raycast (transform.position + new Vector3 (0f, -0.74f, 0f), movement3D.normalized, out hitInfo, movement3D.magnitude)) {
			Debug.Log ("I hit something!(bottom) " + hitInfo.collider.name);
			Physics.Raycast (transform.position + movement3D+ new Vector3 (0f, -0.74f, 0f)+hitInfo.normal*movement3D.magnitude, -hitInfo.normal, out hitInfo, movement3D.magnitude);
			drawCross (hitInfo.point, Color.yellow);
			Debug.DrawRay (transform.position,hitInfo.point-transform.position,Color.black);
			return hitInfo.point-transform.position-new Vector3 (0f, -0.75f, 0f);
		} else {
			Debug.Log ("I hit nothing!(bottom)");
			return movement3D;
		}
	}

	Vector3 ShorteningFromPoint(Vector3 pos, Vector3 inputVec)
	{
		///doesnt work upon flipping
		/// casts rays opposite to actual movement
		/// on x axis
		/// but not on y axis
		/// huh
		Debug.DrawRay (transform.position+pos,inputVec,Color.white);
		if(Physics.Raycast(transform.position+pos,inputVec.normalized, out hitInfo, inputVec.magnitude)){
			Debug.Log ("I hit something! " +pos+" - "+ hitInfo.collider.name);
			drawCross (hitInfo.point, Color.black);
			Physics.Raycast (transform.position + pos + inputVec + hitInfo.normal * inputVec.magnitude,
				-hitInfo.normal, out hitInfo, inputVec.magnitude);
			drawCross (hitInfo.point, Color.black);
			drawCross (hitInfo.point- hitInfo.normal * 0.01f, Color.white);
			Debug.DrawRay (hitInfo.point, - hitInfo.normal * 0.01f,Color.red);
			Debug.DrawRay (transform.position,hitInfo.point-transform.position,Color.black);
			return hitInfo.point - transform.position - pos + hitInfo.normal * 0.01f;
		}
		else{
			Debug.Log ("I hit nothing! " +pos);
			return inputVec;
		}
	}

	void drawCross(Vector3 pos, Color col)
	{
		Debug.DrawRay (pos, Vector3.left, col);
		Debug.DrawRay (pos, Vector3.right, col);
		Debug.DrawRay (pos, Vector3.up, col);
		Debug.DrawRay (pos, Vector3.down, col);
	}
}
