using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackScript : MonoBehaviour {

	float lifeSpan;
	float lifeStart;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable()
	{
		lifeSpan = 5.2f;
		lifeStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lifeStart >= lifeSpan)
			Destroy(gameObject);
	}
}
