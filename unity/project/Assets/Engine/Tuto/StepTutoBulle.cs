using UnityEngine;
using System.Collections;

public class StepTutoBulle : StepTuto {

	public AudioSource clic;

	public override void play() {
		gameObject.SetActive(true);
	}

	public void closeIt() {
		gameObject.SetActive(false);
		if (clic != null) {
			clic.Play ();
		}
		this.endStep();
	} 

	public void Update() {
		if (Input.GetKeyDown(constantes.buttonOne) ) {
			closeIt();
		}
	}
}
