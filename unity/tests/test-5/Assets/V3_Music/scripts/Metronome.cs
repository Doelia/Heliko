using UnityEngine;
using System.Collections;

public class Metronome : TempoReceiver {

	void Start () {
	
	}
	
	void Update () {
	
	}

	public override void onStep() {

	}

	public override void onHalfStep() {
		//this.GetComponent<AudioSource> ().Play ();
	}
}
