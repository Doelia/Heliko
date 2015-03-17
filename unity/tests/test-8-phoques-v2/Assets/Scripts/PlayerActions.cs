using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	public LevelScripted level;
	private ArrayList successStep;

	// Use this for initialization
	void Start () {
		this.successStep = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isGood (int type) {
		int stepTapped = level.beatCounter.getStepClosest();
		if (level.getActionFromBeat(stepTapped) == type) {
			this.successStep.Add(stepTapped);
			return true;
		}
		return false;
	}

	public void onAction(int action){
		int nStep = level.beatCounter.getNBeat();
		if (nStep > 1) {
			int previousStep = nStep-1;
			if (level.isStepUseful (previousStep) && !successStep.Contains(previousStep)) {

			}
		}
	}
}
