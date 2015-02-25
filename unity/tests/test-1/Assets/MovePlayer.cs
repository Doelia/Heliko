using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	Vector3 velocity;
	// Use this for initialization
	void Start () {
		this.velocity = new Vector3 (10, 0, 0);
<<<<<<< HEAD
		Physics.gravity = new Vector3 (0, -10, 0);
=======
		Physics.gravity = new Vector3 (0, -100, 0);
>>>>>>> 4d953c22397835fc8d5a7bc813f6f56c8da6cca7
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody.velocity = new Vector3(15, this.rigidbody.velocity.y, this.rigidbody.velocity.z);

		if (Input.GetKeyDown ("space")) {
			this.rigidbody.velocity = new Vector3(1, 10, this.rigidbody.velocity.z);
		}

	}
}
