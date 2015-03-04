using UnityEngine;
using System.Collections;

public class BPMControlor : MonoBehaviour
{

	public AudioSource music = null;
	public float tempo = 120.0f;
	public float errorMargin = 100; // en MS
	public int offsetStart = 0; // EN MS

	// CALCULATEURS SUR LES ATTRIBUTS CONSTANTS

	// Retourne le temps en seconde entre 2 ticks
	public float getTimeInOneTick () {
		return (60.0f / (tempo));
	}

	public int getTimeInOneTickInMS () {
		return Tools.convertToMS(this.getTimeInOneTick());
	}

	private float getErrorMarginInSeconds () {
		return (float) errorMargin / 1000.0f;
	}

	private float getOffsetStartInSeconds() {
		return (float) offsetStart / 1000.0f;
	}

	// CALCULATEURS EN FONCTION DU TEMPS

	// Retourne le temps passé en MS depuis le commencement de la musique
	private int getTimeInMusic () {	
		int time = Tools.convertToMS( (float) music.timeSamples / (float)music.clip.frequency);		
		Debug.Log ("Temps passé = "+time);
		return time;
	}

	// Retourne le temps passé depuis le tout premier tick (en MS)
	private int getTimeBeforeFirstTick() {
		return this.getTimeInMusic() - offsetStart;
	}

	// Retourne le temps qu'il s'est passé depuis le dernier tick
	private int getTimeBeforeLastTick() {
		return this.getTimeBeforeFirstTick() % this.getTimeInOneTickInMS();
	}

	// Retourne le temps restant avant le prochain tick
	private int getTimeAfterNextTick() {
		return this.getTimeInOneTickInMS() - getTimeBeforeLastTick();
	}

	public int getRelativeScore() {
		int msBefore = this.getTimeBeforeLastTick();
		int msAfter = this.getTimeAfterNextTick();
		if (msBefore > msAfter) {
			return - msAfter;
		} else {
			return msBefore;
		}
	}

	// NOTIFICATEURS
	

	private void notifyChildren () {
		Debug.Log ("TAP!");
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onStep ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}

	private void notifyExitSuccessWindow () {
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onSuccessWindowExit ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}

	private void notifyEnterSuccessWindow () {
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<TempoReceiver> () != null) {
				s1.GetComponent<TempoReceiver> ().onSuccessWindowEnter ();
			} else {
				Debug.LogError (s1.name + " : GetComponent TempoReceiver null");
			}
		}
	}

	// START

	void Start () {
		music.Play ();
		float d = (float) offsetStart / 1000.0f;
		InvokeRepeating ("notifyChildren", d, getTimeInOneTick ());
		//InvokeRepeating ("notifyEnterSuccessWindow", d, getTimeInOneTick () - getErrorMarginInSeconds ());
		//InvokeRepeating ("notifyExitSuccessWindow", d, getTimeInOneTick () + getErrorMarginInSeconds ());
	}
	

}
