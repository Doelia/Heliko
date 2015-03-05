using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour
{

	public AudioSource music = null;
	public float tempo = 120.0f;
	public float errorMargin = 100; // en MS
	public int offsetStart = 0; // EN MS

	private int nbrTicks = 0;

	// 

	// CALCULATEURS SUR LES ATTRIBUTS CONSTANTS

	// Retourne le temps en seconde entre 2 ticks
	public float getTimeInOneTick ()
	{
		return (60.0f / (tempo * 2));
	}

	public int getTimeInOneTickInMS ()
	{
		return Tools.convertToMS (this.getTimeInOneTick ());
	}

	private float getErrorMarginInSeconds ()
	{
		return (float)errorMargin / 1000.0f;
	}

	private float getOffsetStartInSeconds ()
	{
		return (float)errorMargin / 1000.0f;
	}

	// CALCULATEURS EN FONCTION DU TEMPS

	// Retourne le temps passé en MS depuis le commencement de la musique
	private float getTimeInMusic ()
	{	
		return (float)music.timeSamples / (float)music.clip.frequency;
	}

	// Retourne le temps passé en MS depuis le commencement de la musique
	private int getTimeInMusicInMS ()
	{	
		int time = Tools.convertToMS (this.getTimeInMusic ());		
		return time;
	}

	// Retourne le temps passé depuis le tout premier tick (en MS)
	private int getTimeBeforeFirstTick ()
	{
		return this.getTimeInMusicInMS () - offsetStart;
	}

	// Retourne le temps qu'il s'est passé depuis le dernier tick
	private int getTimeBeforeLastTick ()
	{
		return this.getTimeBeforeFirstTick () % this.getTimeInOneTickInMS ();
	}

	// Retourne le temps restant avant le prochain tick
	private int getTimeAfterNextTick ()
	{
		return this.getTimeInOneTickInMS () - getTimeBeforeLastTick ();
	}

	public bool timeIsInWindow ()
	{
		return (this.getAbsoluteScore () < errorMargin);
	}

	// Ne doit être utilisé en dehors seulement pour les tests, sinon utiliser la window
	public int getRelativeScore ()
	{
		int msBefore = this.getTimeBeforeLastTick ();
		int msAfter = this.getTimeAfterNextTick ();
		if (msBefore > msAfter) {
			return - msAfter;
		} else {
			return msBefore;
		}
	}

	private int getAbsoluteScore ()
	{
		int msBefore = this.getTimeBeforeLastTick ();
		int msAfter = this.getTimeAfterNextTick ();
		return Mathf.Min (msBefore, msAfter);
	}

	// UPDATES

	// Le "d" augmente jusqu'a attendre le temps qu'on veut

	private float d = 0;
	private float lastTime = 0;
	private bool notifiedEnter = false;
	private bool notifiedExit = false;

	private bool exitedSuccessWindow ()
	{		
		if (!notifiedExit && !timeIsInWindow ()) {		
			notifiedExit = true;		
			notifiedEnter = false;		
			return true;		
		}		
		return false;		
	}		
				
	private bool enteredSuccessWindow ()
	{
		if (!notifiedEnter && timeIsInWindow ()) {		
			notifiedEnter = true;		
			notifiedExit = false;		
			return true;		
		}		
		return false;		
	}

	void Update ()
	{
		float diff = this.getTimeInMusic () - lastTime;		
		lastTime = this.getTimeInMusic ();		
		d += diff;
		
		if (d > this.getTimeInOneTick ()) {		
			d = (d - this.getTimeInOneTick ());		
			this.notifyChildren ();
		}

		if (enteredSuccessWindow ()) {	
			this.notifyEnterSuccessWindow ();		
		}		
				
		if (exitedSuccessWindow ()) {		
			this.notifyExitSuccessWindow ();		
		}
	}


	// NOTIFICATEURS

	ArrayList observers;

	public void Awake ()
	{
		this.observers = new ArrayList ();
	}

	public void connect (TempoReceiver r)
	{
		this.observers.Add (r);
	}

	private void notifyChildren ()
	{
		foreach (TempoReceiver e in this.observers) {
			e.onStep ();
		}
	}

	private void notifyExitSuccessWindow ()
	{
		foreach (TempoReceiver e in this.observers) {
			e.onSuccessWindowExit ();
		}
	}

	private void notifyEnterSuccessWindow ()
	{
		foreach (TempoReceiver e in this.observers) {
			e.onSuccessWindowEnter ();
		}
	}

	// START

	void Start ()
	{
		music.Play ();
		Debug.Log ("getTimeInOneTickInMS=" + getTimeInOneTickInMS ());
		d = - Tools.convertToSeconds (offsetStart);
	}
	

}
