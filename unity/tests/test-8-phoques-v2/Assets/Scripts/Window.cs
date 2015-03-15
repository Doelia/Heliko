using UnityEngine;
using System.Collections;

public abstract class Window : Timer {

	public GameObject beatCounterGameObject;
	private BeatCounter beatCounter;
	protected int right = 1;

	public void Awake() {
		this.observers = new ArrayList ();
		beatCounter = beatCounterGameObject.GetComponent<BeatCounter>();
		this.setSampleDelay(beatCounter.delayInMS + right * beatCounter.timeWindowInMS);
	}

	protected override void beat() {
		this.notifyChildren();
	}

	new public void Start() {
		base.Start();
	}

	protected ArrayList observers;

	public void connect (TempoReceiver r) {
		this.observers.Add (r);
	}

	protected abstract void notifyChildren ();
}

