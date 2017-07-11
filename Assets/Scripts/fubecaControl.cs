using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary {

	public float xMin, xMax, zMin, zMax;

}

public class fubecaControl : MonoBehaviour {


	public float speed = 10;
	public float tilt;
	public Boundary boundary;

	public GameObject bullet;
	public Transform shotSpawn;

	public float fireRate = 0.5f;
	private float nextFire = 0.0f;

	private AudioSource weaponAudio;

	private Rigidbody shipRB;

	void Start() {

		shipRB = GetComponent<Rigidbody> ();
		weaponAudio = GetComponent<AudioSource> ();
	}


	void Update() {
	
		if ( CrossPlatformInputManager.GetButtonDown("Jump") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (bullet, shotSpawn.position, shotSpawn.rotation);
			weaponAudio.Play ();
		}
	
	}


	void FixedUpdate () {
		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        shipRB.velocity = movement * speed;

		shipRB.position = new Vector3 (
			Mathf.Clamp(shipRB.position.x, boundary.xMin, boundary.xMax),
			0.0f, 
			Mathf.Clamp(shipRB.position.z, boundary.zMin, boundary.zMax)
		);

		shipRB.rotation = Quaternion.Euler (0.0f, 0.0f, shipRB.velocity.x * -tilt);
	}



}
