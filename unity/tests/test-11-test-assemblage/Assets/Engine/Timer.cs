using UnityEngine;
using System.Collections;

public abstract class Timer : HelikoObject {

	public AudioSource audioSource;
	public float loopTime = 30f; // Temps d'attente entre chaque boucle en MS

	private float nextBeatSample; // Le numéro du prochain sample à attendre pour un nouveau beat
	protected float samplePeriod; // Le temps en samples d'un beat
	protected float sampleDelay; // Le temps d'attente en sample avant de compter le premier beat

	protected MusicTempo music;

	private int myMsDelayStartCount = 0;
	private int myDelayTicks = 0;
	private int nBeat = 0;

	protected abstract void beat();

	protected void setSampleDelay(int msDelayStartCount, int delayTicks) {
		myMsDelayStartCount = msDelayStartCount;
		myDelayTicks = delayTicks;
	}

	public MusicTempo getMusic() {
		return music;
	}

	public int getNBeat() {
		return this.nBeat;
	}

	public new void Start() {

		base.Start();

		if (constantes.instantCalcul)
			loopTime = 0;
		
		music = audioSource.GetComponent<MusicTempo>();

		Debug.Log("Time between ticks : "+music.getTimePeriod()*1000);

		float delayMS = myMsDelayStartCount + (myDelayTicks*music.getTimePeriod()*1000f);

		sampleDelay = ((float) delayMS / 1000f) * music.getFrequency();
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
					this.beat();
					nBeat++;
					nextBeatSample += samplePeriod;
				}
			}
			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}
}
