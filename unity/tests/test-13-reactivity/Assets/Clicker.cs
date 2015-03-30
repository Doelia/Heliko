using UnityEngine;
using System.Collections;

public class Clicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	bool hey = true;

	void go() {
		this.GetComponent<AudioSource>().Play();
		if (hey)
		this.GetComponent<Renderer>().material.color = new Color(1,0,0);
		else
			this.GetComponent<Renderer>().material.color = new Color(0,1,0);

		hey = !hey;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				this.GetComponent<AudioSource>().Play();
			}
		}
		*/

		if (Input.touchCount == 1) {    
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				go ();
			}
		}

		if (Input.GetKeyDown (KeyCode.P))
			go ();
	}
}
