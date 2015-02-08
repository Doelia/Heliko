using UnityEngine;
using System.Collections;

public class MoveProjectile : MonoBehaviour {

	public Transform planet;
	public float speedMove = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vec = this.planet.position - this.transform.position;
		vec = Quaternion.Euler(0, 0, 90) * vec;
		vec = vec.normalized;
		vec *= speedMove;
		transform.rigidbody.velocity = vec;
		Vector3 nor = this.planet.position - this.transform.position;;
		nor = nor.normalized * 5;
		float angle = Vector3.Angle (nor, (nor.y > 0)?Vector3.right:Vector3.left);
		transform.localEulerAngles = new Vector3(0, 0, angle);
	}
}
