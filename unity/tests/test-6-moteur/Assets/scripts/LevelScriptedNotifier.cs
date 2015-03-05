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
	private bool successThisStep = false;
	private int before = 1;

	public BPMControlor bpm;
	public bool loop = true;

	// PROVISOIRE
	public Transform cube;

	ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
	}

	public void Start ()
	{
		this.bpm.connect (this);
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

		for (int i = 0; i < steps.Length * 2; i++) {
			Debug.Log (i + " : " + toReturn [i]);
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
		if (stepEvents [this.getIndex()] != 0)
			notifyChildren (stepEvents [this.getIndex()]);
	}

	public bool isGood (int type)
	{

		if (this.bpm.timeIsInWindow () && stepEvents [getIndex ()] == type) {
			successThisStep = true;
			return true;
		}
		return false;
	}

	public int getIndex ()
	{
		return (this.bpm.getNumStep()) % stepEvents.Length;
	}

	public void changeColorCube (bool good)
	{
		if (good) {
			cube.renderer.material.color = new Color (0, 1, 0);
		} else {
			cube.renderer.material.color = new Color (1, 0, 0);
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(100,0,100,100), "tab["+this.getIndex()+"] = "+this.stepEvents[this.getIndex()]+"");
	}

	public override void onSuccessWindowExit ()
	{
		if (this.stepEvents[this.getIndex()] > 0) {
			this.changeColorCube(false);
			if (!successThisStep) {
				notifyChildrenOfFailure ();
			}
		}
	}

	public override void onSuccessWindowEnter ()
	{
		successThisStep = false;
		if (this.stepEvents[this.getIndex()] > 0) {
			this.changeColorCube(true);
		}
	}
}
