using UnityEngine;
using System.Collections;

public abstract class Timer : HelikoObject {

	public bool startCountAtLoad = true;
	protected bool stopIt = false;
	private bool loop;

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
	protected abstract void endMusic();

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
		if (isStart) return;

		base.Start();

		if (constantes.instantCalcul)
			loopTime = 0;
		
		music = audioSource.GetComponent<MusicTempo>();

		if (music == null) {
			Debug.LogError ("MusicTempo component not found");
			return;
		}

		//Debug.Log("Time between ticks : "+music.getTimePeriod()*1000);

		float delayMS = myMsDelayStartCount + (myDelayTicks*music.GetTimePeriod()*1000f);

		sampleDelay = ((float) delayMS / 1000f) * music.GetFrequency();
		samplePeriod = music.GetSamplePeriod();
		nextBeatSample = sampleDelay;

		if (startCountAtLoad) {
			StartCount();
		} 
	}

	public void StartCount() {
		audioSource.Play();
		StartCoroutine(BeatCheck());
	}

	// Stop complétement le thread de comptage
	public void stopCount() {
		this.stopIt = true;
	}

	public void reset() {
		audioSource.Stop();
		audioSource.Play();
		nBeat = 0;
		nextBeatSample = sampleDelay;
	}

	public void setLoop(bool loop) {
		this.loop = loop;
	}

 	IEnumerator BeatCheck () {
		while (!stopIt) {
			if (audioSource.isPlaying) {
				float currentSample = audioSource.timeSamples;
				if (currentSample >= (nextBeatSample)) {
					this.beat();
					nBeat++;
					nextBeatSample += samplePeriod;
				}
			}
			if (music.IsFinished()) {
				if(loop) {
					reset();
				} else {
					this.endMusic();
				}
			}
			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}
}
