using UnityEngine;
using System.Collections;

public class Beat : MonoBehaviour, TempoReceiver {

	public BeatCounter beatCounter;

	public void Start () {
		this.beatCounter.connect(this);
	}
	
	void Update () {
		
	}

	public void onStep () {
		this.GetComponent<Animator> ().SetTrigger ("Move");
	}

	public void onSuccessWindowExit () {

	}

	public void onSuccessWindowEnter () {

	}
}
