using UnityEngine;
using System.Collections;

public class ControlPlayer : MonoBehaviour {

	public Transform planet;
	public float forceJump;
	public float speedMove;

	// Use this for initialization
	void Start () {
	
	}

	Vector3 getMoveVector() {
		Vector3 vec = this.planet.position - this.transform.position;
		vec = Quaternion.Euler(0, 0, 90) * vec;
		vec = vec.normalized;
		vec *= speedMove;
		return vec;
	}

	Vector3 getJumpVector() {
		Vector3 vec = this.planet.position - this.transform.position;
		vec = - vec.normalized;
		vec = Quaternion.Euler(0, 0, -80) * vec;
		return vec * forceJump;
	}

	// Update is called once per frame
	void Update () {
		Vector3 v = new Vector3(0,0,0);
		v = v + getMoveVector();
		if (Input.GetKeyDown ("a")) {
			v = v + this.getJumpVector ();
		}
		Debug.DrawLine (this.transform.position, v+this.transform.position, Color.red);
		this.rigidbody.velocity = v;

	}
}
