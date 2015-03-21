using UnityEngine;
using System.Collections;

/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */

public class LevelScripted : MonoBehaviour, TempoReceiver {

	public TextAsset levelData;
	public BeatCounter beatCounter;
	public int waitingStep; // Nombre de step à remplire par des 0 (utile pour les déclages)
	
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

		string [] lineOne = s [0].Split (' ');
		string [] lineTwo = s [1].Split (' ');
		string [] lineThree = s [2].Split (' ');
		string [] lineFour = s [3].Split (' ');

		int nbrLines = 4;
		int nbrZeroAdded = nbrLines * waitingStep;
		int nbrActions = lineOne.Length * nbrLines + nbrZeroAdded;

		int [] toReturn = new int[nbrActions];

		for (int i = 0; i < nbrZeroAdded; i++) {
			toReturn[i] = 0;
		}

		for (int i = 0; i < lineOne.Length; i++) {
			toReturn [i * nbrLines + 0 + nbrZeroAdded] = int.Parse (lineOne [i]);
			toReturn [i * nbrLines + 1 + nbrZeroAdded] = int.Parse (lineTwo [i]);
			toReturn [i * nbrLines + 2 + nbrZeroAdded] = int.Parse (lineThree [i]);
			toReturn [i * nbrLines + 3 + nbrZeroAdded] = int.Parse (lineFour [i]);
		}

		for (int i = 0; i < nbrActions; i++) {
			Debug.Log (i + " : " + toReturn [i]);
		}

		return toReturn;
	}

	public int getActionFromBeat(int nBeat) {
		return stepEvents [getIndex (nBeat)];
	}

	public bool isStepUseful(int nBeat) {
		return getActionFromBeat(nBeat) > 0;
	}

	private int getIndex (int nBeat) {
		return (nBeat % stepEvents.Length);
	}

	private int getCurrentIndex() {
		return getIndex(this.beatCounter.getNBeat());
	}

	// EVENTS

	public void onStep (int nBeat) {
		if (isStepUseful(nBeat)) {
			notifyChildren (getActionFromBeat(nBeat));
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
