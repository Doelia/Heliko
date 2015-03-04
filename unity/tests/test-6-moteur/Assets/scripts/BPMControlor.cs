using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour
{

	public AudioSource music;
	public float tempo = 130.0f;
	public float errorMargin = 100; // en MS
	public int offsetStart = 0; // EN MS

	private float d = 0;
	private float dHalf = 0;
	private bool notifiedEnter = false;
	private bool notifiedExit = false;

	public float getDeltaTempo () {
		return (60.0f / tempo);
	}

	// Donne l'écart en MS avec le step parfait (0 si parfait)
	public float getScore () {
		float t = timeSinceLastTick (); // Plus en plus négatif
		float t2 = timeBeforeNextTick (); // Plus en plus positif
		return Mathf.Min (t, t2) * 1000; // en MS
	}

	public bool isOnStep () {
		return (this.getScore () < errorMargin);
	}

	void Start () {
		music.Play ();
		d = (float) offsetStart / 1000.0f;
		Debug.Log ("d="+d);
		this.dHalf = d + this.getDeltaTempo () / 2;
	}
	
	public float timeBeforeNextTick () {
		return this.getDeltaTempo () - d;
	}
	
	public float timeSinceLastTick () {
		return d;
	}
	
	void notifyChildren () {
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onStep ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}

	void notifyExitSuccessWindow () {
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onSuccessWindowExit ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}

	void notifyEnterSuccessWindow () {
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onSuccessWindowEnter ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}
	
	float lastTime = 0;
	
	private float getTime () {
		return (float)music.timeSamples / (float)music.clip.frequency;
	}

	private bool exitedSuccessWindow () {
		if (!notifiedExit && !isOnStep ()) {
			notifiedExit = true;
			notifiedEnter = false;
			return true;
		}
		return false;
	}
	
	private bool enteredSuccessWindow ()
	{
		if (!notifiedEnter && isOnStep ()) {
			notifiedEnter = true;
			notifiedExit = false;
			return true;
		}
		return false;
	}
	
	void Update () {

		float diff = this.getTime () - lastTime;
		lastTime = this.getTime ();
		d += diff;
		dHalf += diff;
		
		if (d > this.getDeltaTempo ()) {
			d = (d - this.getDeltaTempo ());
			this.notifyChildren ();
		}

		if (dHalf > this.getDeltaTempo ()) {
			dHalf = (dHalf - this.getDeltaTempo ());
			this.notifyChildren ();
		}

		if (enteredSuccessWindow ()) {
			this.notifyEnterSuccessWindow ();
		}

		if (exitedSuccessWindow ()) {
			this.notifyExitSuccessWindow ();
		}
		

	}
}
