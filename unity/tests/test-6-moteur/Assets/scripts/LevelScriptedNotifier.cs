using UnityEngine;
using System.Collections;

/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */

public class LevelScriptedNotifier : TempoReceiver
{

	public TextAsset levelData;
	private int[] stepEvents;
	private int eventIndex;
	public BPMControlor bpm;
	public bool loop;
	public bool successThisStep;

	ArrayList observers;

	public void Awake ()
	{
		this.observers = new ArrayList ();
		loop = true;
		successThisStep = false;
	}

	public void Start ()
	{
		loadData ();
	}
	
	public void connect (LevelScriptedReceiver r)
	{
		this.observers.Add (r);
	}

	public void loadData ()
	{
		Debug.Log (levelData.text);
		string [] tracks = levelData.text.Split ('\n');
		stepEvents = stringToIntEvents (tracks);
	}

	private int [] stringToIntEvents (string[] s)
	{
		string [] steps = s [0].Split (' ');
		string [] halfSteps = s [1].Split (' ');
		int [] toReturn = new int[steps.Length * 2];
		for (int i = 0; i < steps.Length; i++) {
			toReturn [i * 2] = int.Parse (steps [i]);
			toReturn [i * 2 + 1] = int.Parse (halfSteps [i]);
		}
		return toReturn;
	}

	private void notifyChildren (int type)
	{
		foreach (LevelScriptedReceiver e in this.observers) {
			e.onEventType (type);
		}
	}

	private void notifyChildrenOfFailure ()
	{
		foreach (LevelScriptedReceiver e in this.observers) {
			e.onFailure ();
		}
	}
	                                
	public override void onStep ()
	{
		notifyChildren (stepEvents [eventIndex++]);
		if (loop && eventIndex >= stepEvents.Length) {
			eventIndex = 0;
		}
	}

	public bool isGood (int type)
	{
		if (bpm.isOnStep ()) {
			return stepEvents [eventIndex] == type;
		}
		return false;
	}

	public override void onSuccessWindowExit ()
	{
		if (!successThisStep && stepEvents [eventIndex] != 0) {
			notifyChildrenOfFailure ();
		}
	}

	public override void onSuccessWindowEnter ()
	{
		successThisStep = false;
	}
}
