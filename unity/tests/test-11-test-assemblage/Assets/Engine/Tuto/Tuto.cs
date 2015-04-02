using UnityEngine;
using System.Collections;

public class Tuto : HelikoObject {

	public StepTuto[] steps;
	int nStep = 0;

	public new void Start() {
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
