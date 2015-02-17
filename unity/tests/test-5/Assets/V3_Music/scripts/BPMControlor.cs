using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour {

	public AudioSource music;
	public float tempo = 130.0f;
	public float d = 0;
	public float dHalf = 0;

	public float getDeltaTempo() {
		return (60.0f / tempo);
	}

	public float getScore() {
		float s = this.timeSinceLastTick() / this.getDeltaTempo ();
		float s2 = this.timeBeforeNextTick() / this.getDeltaTempo ();
		return 1 - (Mathf.Min (s, s2) * 2);
	}

	// Use this for initialization
	void Start () {
		music.Play ();
		Debug.Log ("Delta 129 = " + (60f / 130f));
		Debug.Log ("Delta 130 = " + (60f / 130f));
		Debug.Log ("Delta 131 = " + (60f / 131f));
		dHalf = d + this.getDeltaTempo () / 2;
	}

	public float timeBeforeNextTick() {
		return this.getDeltaTempo() - d;
	}

	public float timeSinceLastTick() {
		return d;
	}

	void notifyChildren(bool isStep) {
		foreach (Transform s in transform) {
			foreach (Transform s1 in s) {
				if (s1.GetComponent<TempoReceiver>() != null) {
					if (isStep)
						s1.GetComponent<TempoReceiver>().onStep();
					else
						s1.GetComponent<TempoReceiver>().onHalfStep();
				} else {
					Debug.LogError(s1.name+" : GetComponent TempoReceiver null");
				}

			}
		}
	}

	float lastTime = 0;

	private float getTime() {
		return (float) music.timeSamples / (float)music.clip.frequency;
	}

	// Update is called once per frame
	void Update () {

		float diff = this.getTime() - lastTime;
		lastTime = this.getTime();
		d += diff;
		dHalf += diff;
		
		if (d > this.getDeltaTempo()) {
			d = (d - this.getDeltaTempo());
			this.notifyChildren(true);
		}

		if (dHalf > this.getDeltaTempo()) {
			dHalf = (dHalf - this.getDeltaTempo());
			this.notifyChildren(false);
		}


	}
}
