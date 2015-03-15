using UnityEngine;
using System.Collections;

/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */

public class LevelScripted : MonoBehaviour, TempoReceiver {

	public bool loop = true;
	public TextAsset levelData;

	public BeatCounter beatCounter;
	
	private int[] stepEvents;
	private ArrayList successStep;

	private ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
		this.successStep = new ArrayList();
	}

	public void Start () {
		loadData();
		beatCounter.connect(this);
	}
	
	public void loadData () {
		Debug.Log (levelData.text);
		string [] tracks = levelData.text.Split ('\n');
		stepEvents = stringToIntEvents (tracks);
	}

	private int [] stringToIntEvents (string[] s) {
		string [] steps = s [0].Split (' ');
		string [] halfSteps = s [1].Split (' ');
		int [] toReturn = new int[steps.Length * 2];
		for (int i = 0; i < steps.Length; i++) {
			toReturn [i * 2] = int.Parse (steps [i]);
			toReturn [i * 2 + 1] = int.Parse (halfSteps [i]);
		}

		for (int i = 0; i < steps.Length * 2; i++) {
			Debug.Log (i + " : " + toReturn [i]);
		}

		return toReturn;
	}

	public int getIndex (int nBeat) {
		return (nBeat % stepEvents.Length);
	}

	public int getPreviousIndex() {
		int previous = this.beatCounter.getNBeat() - 1;
		if (previous < 0) {
			previous = stepEvents.Length - 1;
		}
		return (previous);
	}

	public bool isValidStep(int nStep) {
		return stepEvents [getIndex (nStep)] > 0;
	}

	public bool isGood (int type) {
		int stepTapped = this.beatCounter.getStepClosest();
		//Debug.Log ("tapped = "+stepTapped+", stepActual = "+this.beatCounter.getNBeat());
		if (stepEvents [getIndex (stepTapped)] == type) {
			this.successStep.Add(stepTapped);
			Debug.Log ("Step "+stepTapped+" OK");
			return true;
		}
		return false;
	}

	// EVENTS

	public void onStep (int nStep) {
		int actualValue = stepEvents[getIndex(nStep)];
		//Debug.Log ("value("+nStep+"="+actualValue);
		if (actualValue > 0) {
			notifyChildren (actualValue);
		}
		if (nStep > 1) {
			int previousStep = nStep-1;
			if (isValidStep (previousStep) && !successStep.Contains(previousStep)) {
				Debug.Log ("Step "+(nStep-1)+" :(");
				this.notifyChildrenOfFailure();
			}
		}
	}
	

	// NOTIFICATIONS

	public void connect (LevelScriptedReceiver r) {
		this.observers.Add (r);
	}

	private void notifyChildren (int type) {
		foreach (LevelScriptedReceiver e in this.observers) {
			e.onEventType (type);
		}
	}

	private void notifyChildrenOfFailure () {
		foreach (LevelScriptedReceiver e in this.observers) {
			e.onFailure ();
		}
	}



}
