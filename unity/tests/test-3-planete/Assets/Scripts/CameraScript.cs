using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform player;
	public Transform planet;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = new Vector3(this.player.position.x, this.player.position.y, -1);

		Vector3 nor = this.planet.position - this.player.position;;
		//this.gameObject.transform.position += - nor.normalized;

		nor = nor.normalized * 5;
		float angle = Vector3.Angle (nor, (nor.x > 0)?Vector3.down:Vector3.up);
		if (nor.x < 0) angle += 180;
		transform.localEulerAngles = new Vector3(0, 0, angle);


	}
}
