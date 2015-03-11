using UnityEngine;
using System.Collections;

/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */

public class LevelScripted : MonoBehaviour, TempoReceiver
{

	public bool loop = true;
	public TextAsset levelData;

	public BeatCounter beatCounter;
	public WindowClose wc;
	public WindowOpen wo;
	
	private int[] stepEvents;
	private int eventIndex = 0;
	private bool successThisStep = true;
	private int before = 1;

	ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
	}

	public void Start () {
		loadData();
		beatCounter.connect(this);
		wc.connect(this);
		wo.connect(this);
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

	private void incrementIndex () {
		eventIndex++;
		if (loop && eventIndex >= stepEvents.Length) {
			eventIndex = 0;
		}
	}

	public int getIndex () {
		return (eventIndex + before) % stepEvents.Length;
	}
	
	public bool isGood (int type) {
		if (successThisStep) {
			return true;
		}
		if (this.beatCounter.isInWindow () && stepEvents [getIndex ()] == type) {
			successThisStep = true;
			return true;
		}
		return false;
	}

	// EVENTS

	public void onStep () {
		this.incrementIndex ();
		before = 0;
		Debug.Log("Step!, stepEvents["+getIndex()+"] = "+stepEvents[getIndex()]);
		if (stepEvents [getIndex()] > 0) {
			notifyChildren (stepEvents [getIndex()]);
		}
	}
	
	public void onSuccessWindowExit () {
		if (stepEvents [eventIndex] > 0) {
			if (!successThisStep) {
				notifyChildrenOfFailure ();
			}
		}
	}

	public void onSuccessWindowEnter () {
		successThisStep = false;
		before = 1;
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
