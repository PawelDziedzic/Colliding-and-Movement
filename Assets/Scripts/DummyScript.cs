using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour {

	public float minGroundNormalY = .65f;
	public float gravityModifier = 1f;

	protected bool grounded;
	protected Vector2 groundNormal;
	protected Rigidbody2D rb2d;
	protected Vector2 velocity;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected RaycastHit singleBuffer;
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);


	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;

	void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Start () 
	{

	}

	void Update () 
	{

	}

	void FixedUpdate()
	{
		grounded = false;

		velocity += gravityModifier * new Vector2 (0f, -9.8f) * Time.deltaTime;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 move = Vector2.up * deltaPosition.y;

		Movement (move);
	}

	void Movement(Vector2 move)
	{
		float distance = move.magnitude;

		if (distance > minMoveDistance) 
		{
			rb2d.Cast (move, hitBuffer, distance + shellRadius);
			//Physics.Raycast (transform.position, Vector3.down, out singleBuffer, distance + shellRadius);
			Debug.DrawRay(transform.position, Vector3.down, Color.cyan, distance + shellRadius);

			Vector2 currentNormal = hitBuffer[0].normal;
			if (currentNormal.y > minGroundNormalY) 
			{
				grounded = true;
				groundNormal = currentNormal;
				currentNormal.x = 0;
			}

			float projection = Vector2.Dot (velocity, currentNormal);
			if (projection < 0) 
			{
				//velocity = velocity - projection * currentNormal;
			}

			float modifiedDistance = hitBuffer[0].distance - shellRadius;
			if (modifiedDistance < distance)
				distance = modifiedDistance;
		}

		transform.Translate (move.normalized * distance);
	}

}