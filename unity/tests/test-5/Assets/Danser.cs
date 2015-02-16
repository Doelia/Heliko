using UnityEngine;
using System.Collections;

public class Danser : MonoBehaviour {

	Animator anim;
	public AudioSource sound;

	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator >();
	}

	bool removeNextFrame = false;

	void goMove() {
		anim.SetBool ("change", true);
		sound.Play();
		removeNextFrame = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (removeNextFrame ) {
			anim.SetBool ("change", false);
		}

		/*foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				goMove();
			}
		}*/


		if (Input.GetKeyDown(KeyCode.Space)) {
			goMove();
		}

	}
}
