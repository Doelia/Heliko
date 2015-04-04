using UnityEngine;
using System.Collections;

public class Step1 : StepTuto {

	public override void play() {
		StartCoroutine(Go());
	}

	IEnumerator Go() {
		yield return new WaitForSeconds(1f);
		endStep();
	}

}
