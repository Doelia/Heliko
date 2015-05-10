using UnityEngine;
using System.Collections;
using ChartboostSDK;
using Soomla.Store;
using Soomla.Store.maBoutique;


/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */

public class LevelScripted : HelikoObject, TempoReceiver {

	public TextAsset levelData;
	private BeatCounter beatCounter;
	public int waitingStep; // Nombre de step à remplire par des 0 (utile pour les décalages)
	
	private int[] stepEvents;
	private ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
	}

	public new void Start () {
		base.Start();
		beatCounter = GetBeatCounter();
		loadData();
		beatCounter.Connect(this);
		chargeAdvertise();
	}
	
	public void loadData () {
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
			//Debug.Log (i + " : " + toReturn [i]);
		}

		return toReturn;
	}

	public int getActionFromBeat(int nBeat) {
		if (nBeat < 0)
			return 0;
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

	public void notifyEnd() {
		notifyChildren(-1);
	}
	
	public void chargeAdvertise()
	{
		if(StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId)<=0)
		{
			Chartboost.cacheInterstitial(CBLocation.Default);
		}
	}

	// EVENTS

	public void OnStep (int nBeat) {
		if (isStepUseful(nBeat)) {
			notifyChildren (getActionFromBeat(nBeat));
		}
		if (getIndex(nBeat) == stepEvents.Length-1) {
			notifyEnd();
		}
	}

	public void OnEndMusic() {

	}

	// NOTIFICATIONS

	public void connect (LevelScriptedReceiver r) {
		this.observers.Add (r);
	}

	public void Disconnect (LevelScriptedReceiver r) {
		this.observers.Remove (r);
	}

	private void notifyChildren (int type) {
		foreach (LevelScriptedReceiver e in this.observers) {
			e.OnAction (type);
		}
	}
}
