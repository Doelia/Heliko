using UnityEngine;
using System.Collections;

public abstract class Timer : MonoBehaviour {

	public AudioSource audioSource;
	public float loopTime = 30f; // Taux de rafraichisement. Si égal à zéro, refrech à chaque update

	private float nextBeatSample; // Le numéro du prochain sample à attendre pour un nouveau beat
	protected float samplePeriod; // Le temps en samples d'un beat
	protected float sampleDelay; // Le temps d'attente en sample avant de compter le premier beat

	protected MusicTempo music;

	private int myMsDelayStartCount = 0;
	private int nBeat = 0;

	protected abstract void beat();

	protected void setSampleDelay(int msDelayStartCount) {
		Debug.Log ("Set sampleDelay at "+msDelayStartCount);
		myMsDelayStartCount = msDelayStartCount;
	}

	public MusicTempo getMusic() {
		return music;
	}

	protected int getNBeat() {
		return this.nBeat;
	}

	public void Start() {
		music = audioSource.GetComponent<MusicTempo>();

		Debug.Log("Time between ticks : "+music.getTimePeriod()*1000);

		sampleDelay = ((float) myMsDelayStartCount / 1000f) * music.getFrequency();
		samplePeriod = music.getSamplePeriod();
		nextBeatSample = sampleDelay;

		audioSource.Play();
		StartCoroutine(BeatCheck());
	}

 	IEnumerator BeatCheck () {
		while (true) {
			if (audioSource.isPlaying) {
				float currentSample = audioSource.timeSamples;
				if (currentSample >= (nextBeatSample)) {
					nBeat++;
					this.beat();
					nextBeatSample += samplePeriod;
				}
			}
			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}
}
