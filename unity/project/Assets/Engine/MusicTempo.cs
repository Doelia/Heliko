using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicTempo : HelikoObject {

	public float bpm = 133f;
	public float scalar = 1f;
	public Slider progressBar = null;

	private AudioSource audioSource;

	
	public void Awake() {
		this.audioSource = this.GetComponent<AudioSource>();
	}

	public float GetTimePeriod() {
		return 60f / (bpm * scalar);
	}

	public float GetSamplePeriod() {
		return (60f / (bpm * scalar)) * audioSource.clip.frequency;
	}

	public int GetFrequency() {
		return this.audioSource.clip.frequency;
	}

	public float GetMusicPercentage() {
		return ((float)audioSource.timeSamples / (float)audioSource.clip.samples);
	}

	public bool IsFinished() {
		return (audioSource.timeSamples >= audioSource.clip.samples);
	}
	
	public void SetMusicPercentage(float p) {
		audioSource.timeSamples = (int)(audioSource.clip.samples * p);
	}

	public void PauseMusic() {
		this.audioSource.Pause();
	}

	public void UnPauseMusic() {
		this.audioSource.UnPause();
	}

	private void SetAvancement() {
		progressBar.value = this.GetMusicPercentage();
	}

	public void Update() {
		if (this.progressBar != null) {
			SetAvancement();
		}
	}

	/// <summary>
	/// Retourne en millsecondes le temps passé
	/// </summary>
	/// <returns>The time passed in MS</returns>
	public float getTimePassed() {
		return (float) this.audioSource.timeSamples / (float) GetFrequency() * 1000;
	}


	
}
