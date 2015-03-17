using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour, LevelScriptedReceiver, TempoReceiver {

	public LevelScripted level;

	private ArrayList successStep;
	private int failuresCount;
	private int stepsCount;
	private string scoreString;

	ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
		this.successStep = new ArrayList();
		failuresCount = 0;
		stepsCount = 0;
		scoreString = "Pourcentage : 100 %";
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
		if (stepTapped >= 0 && level.getActionFromBeat(stepTapped) == type) {
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
		scoreString = "Pourcentage : " + getSuccessPercencage().ToString() + " %";
		stepsCount++;
	}

	public void onStep (int nBeat) {
		if (nBeat > 1) {
			int previousStep = nBeat-1;
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

	public int getSuccessPercencage() {
		if(stepsCount > 0){
			return (int) (100 * (1.0f - ((float)failuresCount) / ((float)stepsCount )));
		}
		else
			return 100;
	}

	public void OnGUI () {
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.normal.textColor = Color.black; 
		string s = "Réussis : " + getNumberOfSuccess();
		GUI.Label (new Rect (100, 10, 100, 20), s, style);
		s = "Échecs : " + failuresCount;
		GUI.Label (new Rect (100, 30, 100, 20), s, style);
		Debug.Log(scoreString);
		GUI.Label (new Rect (100, 50, 100, 20), scoreString, style);
	}
}
