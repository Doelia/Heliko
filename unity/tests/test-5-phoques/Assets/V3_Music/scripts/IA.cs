using UnityEngine;
using System.Collections;

public class IA : TempoReceiver {

	Animator anim;
	public AudioSource audio;

	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator >();
	}

	bool removeNextFrame = false;

	void goMove() {
		anim.SetBool ("change", true);
		removeNextFrame = false;
	}

	int nSound = 6;

	public override void onStep() {
		goMove();
		if ((cpt % 8) == nSound || (cpt % 8) == nSound+1)
			audio.Play ();
	}


	int cpt = 3;
	public override void onHalfStep() {
		if (audio != null) {
			if ((cpt % 8) == nSound)
				audio.Play ();
			
			if ((cpt % 8)  == 0)
				goMove();
			
			cpt++;
		}
	}

	// Update is called once per frame
	void Update () {
		if (removeNextFrame) {
			anim.SetBool ("change", false);
		} else {
			removeNextFrame = true;
		}
	}
}
