using UnityEngine;
using System.Collections;

public class Window : Timer {

	public GameObject beatCounterGameObject;

	private BeatCounter beatCounter;

	void Awake() {
		Debug.Log("Window awake");
		beatCounter = beatCounterGameObject.GetComponent<BeatCounter>();
		this.setSampleDelay(beatCounter.delayInMS - beatCounter.timeWindowInMS);
	}

	protected override void beat() {
		Debug.Log("Open window, time = "+audioSource.timeSamples);
	}

	// Use this for initialization
	public void Start() {
		base.Start();
	}

	

}
