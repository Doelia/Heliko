using UnityEngine;
using System.Collections;

public class StepTutoBulle : StepTuto {

	public override void play() {
		gameObject.SetActive(true);
	}

	public void closeIt() {
		Debug.Log ("close "+gameObject.name);
		gameObject.SetActive(false);
		this.endStep();
	} 
}
