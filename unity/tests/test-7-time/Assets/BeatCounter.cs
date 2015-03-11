using UnityEngine;
using System.Collections;

public class BeatCounter : Timer {

	public int delayInMS = 0;
	public int timeWindowInMS = 100; // Temps en MS avant et après la window

	void Awake() {
		Debug.Log("BeatCounter awake");
		this.setSampleDelay(delayInMS);
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

	public bool isInWindow() {
		return this.getRelativeScore() <= timeWindowInMS;
	}

	protected override void beat() {
		Debug.Log("Tap!");
	}

	public void Start() {
		base.Start();
	}

	public void Update() {
	}


}
