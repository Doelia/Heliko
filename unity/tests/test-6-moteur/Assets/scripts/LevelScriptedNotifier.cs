using UnityEngine;
using System.Collections;

/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */

public class LevelScriptedNotifier : TempoReceiver {

	public TextAsset levelData;
	private int[] stepEvents;
	private int[] halfStepEvents;
	private int eventIndex;
	public BPMControlor bpm;

	ArrayList observers;


	public void Awake() {
		this.observers = new ArrayList ();
	}

	public void Start() {
		loadData ();
	}
	
	public void connect(LevelScriptedReceiver r) {
		this.observers.Add (r);
	}

	public void loadData() {
		Debug.Log (levelData.text);
		string [] tracks = levelData.text.Split ('\n');
		stepEvents = stringToIntEvents (tracks [0]);
		halfStepEvents = stringToIntEvents (tracks [1]);
	}

	private int [] stringToIntEvents(string s) {
		string [] events = s.Split (' ');
		int [] toReturn = new int[events.Length];
		for (int i = 0; i < events.Length; i++) {
			toReturn [i] = int.Parse (events [i]);
		}
		return toReturn;
	}

	private void notifyChildren(int type) {
		foreach (LevelScriptedReceiver e in this.observers) {
			e.onEventType(type);
		}
	}
	                                
	public override void onStep() {
		notifyChildren (stepEvents [eventIndex++]);
	}

	public override void onHalfStep() {
		notifyChildren (halfStepEvents [eventIndex]);
	}

	public bool isGood (int type) {
		if (bpm.isOnStep ()) {
			if (stepEvents [eventIndex] == type) {
				stepEvents [eventIndex] = -1;
				return true;
			}
		} else if (bpm.isOnHalfStep ()) {
			if (halfStepEvents [eventIndex] == type) {
				halfStepEvents [eventIndex] = -1;
				return true;
			}
		}
		return false;
	}
}
