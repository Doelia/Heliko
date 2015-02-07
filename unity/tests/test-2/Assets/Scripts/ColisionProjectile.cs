using UnityEngine;
using System.Collections;

public class ColisionProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name.Equals ("Obstacle")) {
			Destroy (gameObject);
			Destroy(collider.gameObject);
		} else {
		}
	}

}
