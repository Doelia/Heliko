using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	Vector3 velocity;
	// Use this for initialization
	void Start () {
		this.velocity = new Vector3 (10, 0, 0);
		Physics.gravity = new Vector3 (0, -10, 0);
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody.velocity = new Vector3(15, this.rigidbody.velocity.y, this.rigidbody.velocity.z);

		if (Input.GetKeyDown ("space")) {
			this.rigidbody.velocity = new Vector3(15, 70, this.rigidbody.velocity.z);
		}

	}
}
