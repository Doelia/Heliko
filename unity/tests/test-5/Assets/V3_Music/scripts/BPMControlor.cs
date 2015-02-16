using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour {

	public float tempo = 130.0f;
	float d = 0.3f;
	float lastTime;

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
	}

	public float timeBeforeNextTick() {
		return this.getDeltaTempo() - d;
	}

	public float timeSinceLastTick() {
		return d;
	}

	void notifyChildren() {
		foreach (Transform s in transform) {
			foreach (Transform s1 in s) {
				s1.GetComponent<IA>().onNotify();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		d += Time.deltaTime;
		if (d > this.getDeltaTempo()) {
			d = (d - this.getDeltaTempo());
			this.notifyChildren();
		}
	}
}
