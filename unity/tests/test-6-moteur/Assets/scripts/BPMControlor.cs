using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour
{

	public AudioSource music = null;
	public float tempo = 120.0f;
	public int errorMargin = 100; // en MS
	public int offsetStart = 0; // EN MS

	// 

	// CALCULATEURS SUR LES ATTRIBUTS CONSTANTS

	// Retourne le temps en seconde entre 2 ticks
	public float getTimeInOneTick () {
		return (60.0f / (tempo * 2));
	}

	public int getTimeInOneTickInMS () {
		return Tools.convertToMS (this.getTimeInOneTick ());
	}

	private double getErrorMarginInSeconds () {
		return Tools.convertToSeconds(errorMargin);
	}

	private double getOffsetStartInSeconds () {
		return Tools.convertToSeconds(offsetStart);
	}

	// CALCULATEURS EN FONCTION DU TEMPS

	// Retourne le temps passé en MS depuis le commencement de la musique
	private double getTimeInMusic () {	
		return (double)music.timeSamples / (double)music.clip.frequency;
	}

	// Retourne le temps passé en MS depuis le commencement de la musique
	private int getTimeInMusicInMS () {	
		int time = Tools.convertToMS (this.getTimeInMusic ());		
		return time;
	}

	// Retourne le temps passé depuis le tout premier tick (en MS)
	private int getTimeBeforeFirstTick () {
		return this.getTimeInMusicInMS () - offsetStart;
	}

	// Retourne le temps qu'il s'est passé depuis le dernier tick
	private int getTimeBeforeLastTick () {
		return this.getTimeBeforeFirstTick () % this.getTimeInOneTickInMS ();
	}

	// Retourne le temps restant avant le prochain tick
	private int getTimeAfterNextTick () {
		return this.getTimeInOneTickInMS () - getTimeBeforeLastTick ();
	}

	public bool timeIsInWindow () {
		return (this.getAbsoluteScore () < errorMargin);
	}

	public int getNumStep() {
		int timeOnStepClosest = this.getTimeInMusicInMS() - this.getRelativeScore() - offsetStart ;
		timeOnStepClosest /= this.getTimeInOneTickInMS();
		return timeOnStepClosest;
	}

	public int getRelativeScore () {
		int msBefore = this.getTimeBeforeLastTick ();
		int msAfter = this.getTimeAfterNextTick ();
		if (msBefore > msAfter) {
			return - msAfter;
		} else {
			return msBefore;
		}
	}

	private int getAbsoluteScore () {
		int msBefore = this.getTimeBeforeLastTick ();
		int msAfter = this.getTimeAfterNextTick ();
		int score = Mathf.Min (msBefore, msAfter);
		return score;
	}

	// UPDATES

	// Le "d" augmente jusqu'a attendre le temps qu'on veut

	private double d = 0;
	private double lastTime = 0;
	private bool notifiedEnter = false;
	private bool notifiedExit = false;

	private bool exitedSuccessWindow () {		
		if (!notifiedExit && !timeIsInWindow ()) {		
			notifiedExit = true;		
			notifiedEnter = false;		
			return true;		
		}		
		return false;		
	}		
				
	private bool enteredSuccessWindow () {
		if (!notifiedEnter && timeIsInWindow ()) {		
			notifiedEnter = true;		
			notifiedExit = false;		
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

	double last = 0;
	private void notifyChildren () {
		double timePassed = (this.getTimeInMusic() - last) * 1000;
		Debug.Log (timePassed+" time pased");
		Debug.Log ("score: "+this.getTimeBeforeLastTick());
		last = this.getTimeInMusic();
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
		Debug.Log ("getTimeInOneTickInMS=" + getTimeInOneTickInMS ());

		for (int i = 0; i < 200; i++) {
			float voulu = i*getTimeInOneTick();
			StartCoroutine(WaitAndNotif(voulu));
		}

	}

	IEnumerator WaitAndNotif(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		this.notifyChildren();
	}

	void Update () {

	}


	// DEBUG

	private int sc;
	void OnGUI() {
		GUI.Label(new Rect(0,0,100,100), ""+sc);
	}


	

}
