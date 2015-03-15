using UnityEngine;
using System.Collections;

public class BeatCounter : Timer {

	public int delayInMS = 0;
	public int nbrTicksDelay = 0; // Nombre de beat à attendre avant de commencer à compte

	void Awake() {
		this.observers = new ArrayList ();
		this.setSampleDelay(delayInMS, nbrTicksDelay);
	}

	// Retourne le temps passé en MS depuis le dernier beat
	private int getScore() {
		float currentSample = audioSource.timeSamples - sampleDelay;
		float score = currentSample % samplePeriod;
		score /= music.getFrequency();
		return (int) (score*1000);
	}
	
	// Retourne le temps passé en MS depuis ou avant le dernier beat (le meilleur des deux)
	private int getRelativeScore() {
		int score = getScore();
		float timeBeatInMs = samplePeriod / music.getFrequency() * 1000f;
		if (score > timeBeatInMs/2f) {
			score = (int) timeBeatInMs - score;
		}
		return score;
	}

	// Retourne le numéro du step le plus proche à ce temps
	public int getStepClosest() {
		float currentSample = audioSource.timeSamples - sampleDelay;
		float score = currentSample % samplePeriod;
		int step = (int) (currentSample / samplePeriod);
		if (score > samplePeriod/2) {
			step++;
		}
		return step+1;
	}

	protected override void beat() {
		this.notifyChildren();
	}

	// NOTIFIEUR
	ArrayList observers;

	public void connect (TempoReceiver r) {
		this.observers.Add (r);
	}

	private void notifyChildren () {
		foreach (TempoReceiver e in this.observers) {
			e.onStep (this.getNBeat());
		}
	}

}
