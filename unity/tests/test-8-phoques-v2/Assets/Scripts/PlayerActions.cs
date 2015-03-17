using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour, LevelScriptedReceiver {

	public LevelScripted level;
	private ArrayList successStep;

	ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
		this.successStep = new ArrayList();
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
		return false;
	}

	public void notifFailure() {
		foreach (PlayerActionReceiver e in this.observers) {
			e.onFailure();
		}
	}

	public void onAction(int action){
		int nStep = level.beatCounter.getNBeat();
		if (nStep > 1) {
			int previousStep = nStep-1;
			if (level.isStepUseful (previousStep) && !successStep.Contains(previousStep)) {
				this.notifFailure();
			}
		}
	}
}
