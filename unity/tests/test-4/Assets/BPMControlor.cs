using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour {

	float timeStart = 0;
	float tempo = 130.0f;
	float d = 0;
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
		this.timeStart = Time.time;
	}

	public float timeBeforeNextTick() {
		return this.getDeltaTempo() - d;
	}

	public float timeSinceLastTick() {
		return d;
	}

	void notifyChildren() {
		foreach (Transform s in transform) {
			s.GetComponent<AnimationPlayer>().onNotify();
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
