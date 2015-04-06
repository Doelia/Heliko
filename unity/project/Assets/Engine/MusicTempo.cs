using UnityEngine;
using System.Collections;

public class MusicTempo : HelikoObject {

	public float bpm = 133f;
	public float scalar = 1f;

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
	
	public void SetMusicPercentage(float p) {
		audioSource.timeSamples = (int)(audioSource.clip.samples * p);
	}

	public void PauseMusic() {
		this.audioSource.Pause();
	}


	/// <summary>
	/// Retourne en millsecondes le temps passé
	/// </summary>
	/// <returns>The time passed in MS</returns>
	public float getTimePassed() {
		return (float) this.audioSource.timeSamples / (float) GetFrequency() * 1000;
	}
	
}
