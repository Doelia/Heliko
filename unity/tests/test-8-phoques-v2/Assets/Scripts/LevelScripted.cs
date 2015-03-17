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

	private ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
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

		/*
		for (int i = 0; i < steps.Length * 2; i++) {
			Debug.Log (i + " : " + toReturn [i]);
		}
		*/

		return toReturn;
	}

	public int getActionFromBeat(int nBeat) {
		return stepEvents [getIndex (nBeat)];
	}

	public bool isStepUseful(int nStep) {
		return stepEvents [getIndex (nStep)] > 0;
	}

	private int getIndex (int nBeat) {
		return (nBeat % stepEvents.Length);
	}

	private int getCurrentIndex() {
		return getIndex(this.beatCounter.getNBeat());
	}

	// EVENTS

	public void onStep (int nStep) {
		if (isStepUseful(nStep)) {
			notifyChildren (getActionFromBeat(nStep));
		}
	}

	// NOTIFICATIONS

	public void connect (LevelScriptedReceiver r) {
		this.observers.Add (r);
	}

	private void notifyChildren (int type) {
		foreach (LevelScriptedReceiver e in this.observers) {
			e.onAction (type);
		}
	}
}
