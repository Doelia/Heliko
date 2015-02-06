using UnityEngine;
using System.Collections;

public class ControlPlayer : MonoBehaviour {

	public Transform planet;
	public int forceJump = 20;

	// Use this for initialization
	void Start () {
	
	}

	void jump() {
		Vector3 direction = this.planet.position - this.transform.position;
		direction = - direction.normalized;
		this.rigidbody.velocity = direction * forceJump;
	}

	void move() {
		Vector3 vec = this.planet.position - this.transform.position;
		vec = Quaternion.AngleAxis(90, Vector3.up) * vec;
		this.rigidbody.velocity = vec;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			this.jump ();
		} else {
			this.move ();
		}
	}
}
