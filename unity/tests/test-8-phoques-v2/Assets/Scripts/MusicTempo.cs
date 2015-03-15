using UnityEngine;
using System.Collections;

public class MusicTempo : MonoBehaviour {

	public float bpm = 133f;
	public float scalar = 1f;

	private AudioSource audioSource;
	
	void Awake() {
		this.audioSource = this.GetComponent<AudioSource>();
	}

	public float getTimePeriod() {
		return 60f / (bpm * scalar);
	}

	public float getSamplePeriod() {
		return (60f / (bpm * scalar)) * audioSource.clip.frequency;
	}

	public int getFrequency() {
		return this.audioSource.clip.frequency;
	}
	
}
