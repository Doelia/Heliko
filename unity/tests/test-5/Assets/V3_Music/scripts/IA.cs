using UnityEngine;
using System.Collections;

public class IA : TempoReceiver {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator >();
	}

	bool removeNextFrame = false;

	void goMove() {
		anim.SetBool ("change", true);
		removeNextFrame = false;
	}

	public override void onNotify() {
		goMove();
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
