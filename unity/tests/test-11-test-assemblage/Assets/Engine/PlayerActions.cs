using UnityEngine;
using System.Collections;

public class PlayerActions : HelikoObject, LevelScriptedReceiver, TempoReceiver {

	public LevelScripted level; // Utilisé pour le connect et pour tester le good

	private ArrayList successStep;
	private int failuresCount;
	private int stepsCount;
	private ArrayList observers;

	private BeatCounter bc;

	public void Awake () {
		this.observers = new ArrayList ();
		this.successStep = new ArrayList();
		failuresCount = 0;
		stepsCount = 0;
	}

	public new void Start() {
		base.Start();
		if (level != null)
			this.level.connect(this);
		bc = getBeatCounter();
		bc.connect(this);
	}

	public void connect (PlayerActionReceiver r) {
		this.observers.Add(r);
	}

	public bool isGood (int type) {
		int stepTapped = bc.getStepClosest();
		if (level.getActionFromBeat(stepTapped) == type) {
			this.successStep.Add(stepTapped);
			notifySucces();
			return true;
		} else if (level.getActionFromBeat(stepTapped-1) == type ) {
			this.successStep.Add(stepTapped-1);
			notifySucces();
			return true;
		} else {
			if (level.getActionFromBeat(stepTapped+1) == type ) {
				this.successStep.Add(stepTapped+1);
				notifySucces();
				return true;
			}
		}
		failuresCount++;
		this.notifyFailure();
		return false;
	}

	public void notifyFailure() {
		foreach (PlayerActionReceiver e in this.observers) {
			e.onFailure();
		}
	}

	public void notifySucces() {
		foreach (PlayerActionReceiver e in this.observers) {
			e.onSuccess();
		}
	}

	public void onAction(int action){
		stepsCount++;
	}

	public void onStep (int nBeat) {
		if (nBeat > 2) {
			int previousStep = nBeat-2;
			if (level.isStepUseful (previousStep) && !successStep.Contains(previousStep)) {
				failuresCount++;
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

	public int getFailureCount() {
		return failuresCount;
	}

	public int getSuccessPercencage() {
		if(failuresCount == 0 || stepsCount == 0) {
			return 100;
		}
		return (int) (100 * (1.0f - ((float)failuresCount) / ((float)stepsCount)));
	}
}
