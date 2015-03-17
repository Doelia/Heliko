using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour, LevelScriptedReceiver {

	public LevelScripted level;
	private ArrayList successStep;
	private int failuresCount;
	private int stepsCount;

	ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
		this.successStep = new ArrayList();
		failuresCount = 0;
		stepsCount = 0;
	}

	public void Start() {
		this.level.connect(this);
	}

	public void connect (PlayerActionReceiver r) {
		this.observers.Add(r);
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

	public void notifyFailure() {
		foreach (PlayerActionReceiver e in this.observers) {
			e.onFailure();
		}
	}

	public void onAction(int action){
		stepsCount++;
		int nStep = level.beatCounter.getNBeat();
		if (nStep > 1) {
			int previousStep = nStep-1;
			if (level.isStepUseful (previousStep) && !successStep.Contains(previousStep)) {
				this.notifyFailure();
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
