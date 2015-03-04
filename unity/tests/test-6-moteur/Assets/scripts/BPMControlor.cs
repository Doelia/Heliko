using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour
{

	public AudioSource music;
	public float tempo = 130.0f;
	public float errorMargin = 100; // en MS
	public int offsetStart = 0; // EN MS

	private float d = 0;
	private bool notifiedEnter = false;
	private bool notifiedExit = false;

	public float getDeltaTempo ()
	{
		return (60.0f / (tempo * 2));
	}

	private float getErrorMarginInSeconds ()
	{
		return (float)errorMargin / 1000.0f;
	}

	void Start ()
	{
		music.Play ();
		d = (float)offsetStart / 1000.0f;
		InvokeRepeating ("notifyChildren", d, getDeltaTempo ());
		InvokeRepeating ("notifyEnterSuccessWindow", d, getDeltaTempo () - getErrorMarginInSeconds ());
		InvokeRepeating ("notifyExitSuccessWindow", d, getDeltaTempo () + getErrorMarginInSeconds ());
	}
	
	
	void notifyChildren ()
	{
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onStep ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}

	void notifyExitSuccessWindow ()
	{
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onSuccessWindowExit ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}

	void notifyEnterSuccessWindow ()
	{
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onSuccessWindowEnter ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}
	
	void Update ()
	{

	}
}
