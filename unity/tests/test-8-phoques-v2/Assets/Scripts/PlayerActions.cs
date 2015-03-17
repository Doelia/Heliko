using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour, LevelScriptedReceiver, TempoReceiver {

	public LevelScripted level;

	private ArrayList successStep;

	ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
		this.successStep = new ArrayList();
	}

	public void Start() {
		this.level.connect(this);
		this.level.beatCounter.connect(this);
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
		return false;
	}

	public void notifFailure() {
		foreach (PlayerActionReceiver e in this.observers) {
			e.onFailure();
		}
	}

	public void onAction(int action){

	}

	public void onStep (int nBeat) {
		if (nBeat > 1) {
			int previousStep = nBeat-1;
			if (level.isStepUseful (previousStep) && !successStep.Contains(previousStep)) {
				this.notifFailure();
			}
		}
	}
}
