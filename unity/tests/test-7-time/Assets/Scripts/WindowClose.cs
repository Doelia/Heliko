using UnityEngine;
using System.Collections;

public class WindowClose : Timer {

	public GameObject beatCounterGameObject;

	private BeatCounter beatCounter;

	void Awake() {
		Debug.Log("Window close awake");
		this.observers = new ArrayList ();
		beatCounter = beatCounterGameObject.GetComponent<BeatCounter>();
		this.setSampleDelay(beatCounter.delayInMS + beatCounter.timeWindowInMS);
	}

	protected override void beat() {
		this.notifyChildren();
	}

	// Use this for initialization
	public void Start() {
		base.Start();
	}

	ArrayList observers;

	public void connect (TempoReceiver r) {
		this.observers.Add (r);
	}

	private void notifyChildren () {
		foreach (TempoReceiver e in this.observers) {
			e.onSuccessWindowExit();
		}
	}

}
