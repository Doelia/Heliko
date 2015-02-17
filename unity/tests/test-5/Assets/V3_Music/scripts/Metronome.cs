using UnityEngine;
using System.Collections;

public class Metronome : TempoReceiver {

	void Start () {
	
	}
	
	void Update () {
	
	}

	public override void onNotify() {
		this.GetComponent<AudioSource> ().Play ();
	}
}
