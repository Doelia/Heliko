using UnityEngine;
using System.Collections;

public class BeatCounter : Timer {

	public int delayInMS = 0; // Délai avant de lancer le compteur
	public int nbrTicksDelay = 0; // Nombre de beat à attendre avant de commencer à compter

	void Awake() {
		this.observers = new ArrayList ();
		this.setSampleDelay(delayInMS, nbrTicksDelay);
	}

	/**
	* @return L'écart avec le beat le plus proche
	*/ 
	public float getScore() {
		float currentSample = audioSource.timeSamples - sampleDelay;
		float score = currentSample % samplePeriod;
		if (score > samplePeriod / 2)
		return Mathf.Abs(score - samplePeriod);
		else
		return score;
	}

	/**
	* 	@return Le numéro du step le plus proche à ce temps
	*/ 
	public int getStepClosest() {
		float currentSample = audioSource.timeSamples - sampleDelay;
		float score = currentSample % samplePeriod;
		int step = (int) (currentSample / samplePeriod);
		if (score > samplePeriod/2) {
			step++;
		}
		return step;
	}

	/**
	* 	@action Notifie les enfants connectés qu'il y a eu un beat
	*/ 
	protected override void beat() {
		this.NotifyChildren();
	}

	/**
	* 	@action Notifie les enfants connectés que la musique est terminée
	*/ 
	protected override void endMusic() {
		this.NotifyChildrenEndMusic();
	}

	/**
	* 	@return Si la musique est en pause
	*/ 
	public bool isInPause() {
		return stopIt || !this.audioSource.isPlaying;
	}


	// SYSTEME DE NOTIFICATION
	ArrayList observers;

	public void Connect (TempoReceiver r) {
		this.observers.Add (r);
	}

	private void NotifyChildren () {
		foreach (TempoReceiver e in this.observers) {
			e.OnStep (this.getNBeat());
		}
	}

	private void NotifyChildrenEndMusic () {
		foreach (TempoReceiver e in this.observers) {
			e.OnEndMusic();
		}
	}

}
