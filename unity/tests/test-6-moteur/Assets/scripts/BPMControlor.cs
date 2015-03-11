using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour
{

	public AudioSource music = null;
	public float tempo = 120.0f;
	public int errorMargin = 100; // en MS
	public int offsetStart = 0; // EN MS
	private double scoreOnHit = 0;
	private bool notified = false;
	private bool notifiedEntered = false;
	private bool notifiedExited = false;
	int modifier = 1;
	private int lastCpt = -1;

	// 

	// CALCULATEURS SUR LES ATTRIBUTS CONSTANTS

	// Retourne le temps en seconde entre 2 ticks
	public float getTimeInOneTick () {
		return (60.0f / (tempo * 2));
	}

	public float getFrameBetweenTicks() {
		return music.clip.frequency / ((tempo * 2) / 60.0f);
	}

	public int getErrorMarginFrames() {
		double time = (double)errorMargin / 1000.0f;
		return (int)(music.clip.frequency * time);
	}

	public bool isInLowerWindow() {
		double d = (getNumStep() + 1 - modifier) * getFrameBetweenTicks() - getErrorMarginFrames();
		return music.timeSamples > d;
	}

	public bool isInUpperWindow() {
		double d = (getNumStep() + 1 - modifier) * getFrameBetweenTicks() + getErrorMarginFrames();
		return  music.timeSamples < d;
	}

	public bool timeIsInWindow ()
	{
		return isInLowerWindow () && isInUpperWindow();
	}

	public int getNumStep ()
	{
		return Mathf.Max (0,(int) ((music.timeSamples - offsetStart) / getFrameBetweenTicks()));
	}

	public bool justChanged() {
		if(lastCpt != getNumStep()) {
			lastCpt = getNumStep();
			return true;
		}
		return false;
	}

	// NOTIFICATEURS

	ArrayList observers;

	public void Awake () {
		this.observers = new ArrayList ();
	}

	public void connect (TempoReceiver r) {
		this.observers.Add (r);
	}

	private void notifyChildren () {
		foreach (TempoReceiver e in this.observers) {
			e.onStep ();
		}
	}

	private void notifyExitSuccessWindow () {
		foreach (TempoReceiver e in this.observers) {
			e.onSuccessWindowExit ();
		}
	}

	private void notifyEnterSuccessWindow () {
		foreach (TempoReceiver e in this.observers) {
			e.onSuccessWindowEnter ();
		}
	}

	// START

	void Start () {
		music.Play ();
	}

	void Update ()
	{
		if(justChanged()) {
			notifiedEntered = false;
		}

		if(!notified && music.timeSamples > getFrameBetweenTicks() * getNumStep()) {
			modifier = 1;
			scoreOnHit = getScoreOnStep();
			notifyChildren();
			notified = true;
		}
		if (!notifiedEntered && isInLowerWindow()) {
			modifier = 0;
			notified = false;
			notifiedEntered = true;
			notifiedExited = false;
			notifyEnterSuccessWindow ();
		}
		if (!notifiedExited && isInUpperWindow()) {
			notifyExitSuccessWindow ();
			notifiedExited = true;
		}		
	}

	private int timeSamplesWithOffsetStart() {
		return music.timeSamples + offsetStart;
	}

	public double getScoreOnStep() {
		return (music.timeSamples - getNumStep() * getFrameBetweenTicks()) / getFrameBetweenTicks();
	}

	private float nextBeatSample = 0f;
	private int cpt = 0; // Debug

	private float getSamplePeriod() {
		return (60f / this.tempo) * music.clip.frequency;
	}

	private float getSampleOffset() {
		float sampleOffset = (60f / tempo) * music.clip.frequency;
		return sampleOffset;
	}

	public double getScore() {
		return Mathf.Abs((float)getScoreOnStep () - (float)scoreOnHit);
	}

	// DEBUG

	private int sc;
	void OnGUI ()
	{
		GUI.Label (new Rect (0, 0, 100, 100), "" + sc);
	}


	

}
