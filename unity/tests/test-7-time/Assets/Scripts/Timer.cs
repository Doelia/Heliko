using UnityEngine;
using System.Collections;

public abstract class Timer : MonoBehaviour {

	public AudioSource audioSource;

	private float nextBeatSample; // Le numéro du prochain sample à attendre pour un nouveau beat
	protected float samplePeriod; // Le temps en samples d'un beat
	protected float sampleDelay; // Le temps d'attente en sample avant de compter le premier beat

	protected MusicTempo music;

	private int myMsDelayStartCount = 0;

	protected abstract void beat();

	protected void setSampleDelay(int msDelayStartCount) {
		myMsDelayStartCount = msDelayStartCount;
	}

	public void Start() {
		music = audioSource.GetComponent<MusicTempo>();

		sampleDelay = ((float) myMsDelayStartCount / 1000f) * music.getFrequency();
		samplePeriod = music.getSamplePeriod();
		nextBeatSample = sampleDelay;

		audioSource.Play();
		StartCoroutine(BeatCheck());
	}

 	IEnumerator BeatCheck () {
		while (audioSource.isPlaying) {
			float currentSample = audioSource.timeSamples;
			if (currentSample >= (nextBeatSample)) {
				this.beat();
				nextBeatSample += samplePeriod;
			}
			yield return new WaitForSeconds(0 / 1000f);
		}
	}

}
