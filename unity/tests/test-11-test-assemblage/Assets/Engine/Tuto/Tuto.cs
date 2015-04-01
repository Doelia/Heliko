using UnityEngine;
using System.Collections;

public class Tuto : HelikoObject {

	public StepTuto[] steps;
	int nStep = 0;


	public void Start() {
		initTuto();
		startTuto();
	}

	public void initTuto() {
		foreach (StepTuto t in steps) {
			t.init(this);
		}
	}

	public void startTuto() {
		next();
	}

	public void next() {
		steps[nStep].play();
		nStep++;
	}

}
