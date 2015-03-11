using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public AudioSource audioSource;
	public AudioSource snare;

	private float nextBeatSample;
	private float sampleOffset;
	private float samplePeriod;

	private float sampleDelay;
	private float startSample;

	public float audioBpm = 133;
	public int msDelay;

	// Use this for initialization
	void Start() {
		sampleDelay = ((float) msDelay / 1000f) * audioSource.clip.frequency;

		samplePeriod = (60f / (audioBpm * 1)) * audioSource.clip.frequency;
		
		audioSource.Play();
		float syncTime = (float) AudioSettings.dspTime;
		nextBeatSample = (float) syncTime * audioSource.clip.frequency - sampleDelay;
		startSample = nextBeatSample;
		StartCoroutine(BeatCheck());
	}

	void tick() {
		Debug.Log("Score = "+getScore());
		snare.Play();
	}

	private float getScore() {
		float currentSample = (float)AudioSettings.dspTime * audioSource.clip.frequency - sampleDelay; 
		float score = currentSample % samplePeriod;
		if (score > samplePeriod / 2) {
			score = samplePeriod - score;
		}
		return score - 3500;
	}

	IEnumerator BeatCheck ()
	{
		while (audioSource.isPlaying) {
			float currentSample = (float)AudioSettings.dspTime * audioSource.clip.frequency;
			
			if (currentSample >= (nextBeatSample)) {
				this.tick();
				nextBeatSample += samplePeriod;
			}

			yield return new WaitForSeconds(10 / 1000f);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
