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
	public float getScore() {
		float currentSample = audioSource.timeSamples - sampleDelay;
		float score = currentSample % samplePeriod;
		if(score > samplePeriod / 2)
			return Mathf.Abs(score - samplePeriod);
		else
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
		return step;
	}

	protected override void beat() {
		this.notifyChildren();
	}

	public bool isInPause() {
		return stopIt || !this.audioSource.isPlaying;
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
