using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	public LevelScripted level;
	private ArrayList successStep;
	private int failuresCount;
	private int stepsCount;

	// Use this for initialization
	void Start () {
		this.successStep = new ArrayList();
		failuresCount = 0;
		stepsCount = 0;
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
		failuresCount++;
		return false;
	}

	public void onAction(int action){
		stepsCount++;
		int nStep = level.beatCounter.getNBeat();
		if (nStep > 1) {
			int previousStep = nStep-1;
			if (level.isStepUseful (previousStep) && !successStep.Contains(previousStep)) {

			}
		}
	}

	public int getNumberOfSuccess() {
		return successStep.Count;
	}

	public int getScore() {
		return stepsCount - failuresCount;
	}
}
