using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	private Rigidbody astRB;

	// Use this for initialization
	void Start () {

		astRB = GetComponent<Rigidbody> ();

		astRB.angularVelocity = Random.insideUnitSphere * tumble;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
