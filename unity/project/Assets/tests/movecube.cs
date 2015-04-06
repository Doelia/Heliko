using UnityEngine;
using System.Collections;

public class movecube : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			this.GetComponent<Animator>().SetTrigger("go");
		}
	}
}
