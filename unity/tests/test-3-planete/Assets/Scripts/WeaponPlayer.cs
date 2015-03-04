using UnityEngine;
using System.Collections;

public class WeaponPlayer : MonoBehaviour {

	public Transform projectile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			Transform s = Instantiate (projectile) as Transform;
			s.position = this.transform.position;
		}
	}
}
