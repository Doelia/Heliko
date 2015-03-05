using UnityEngine;
using System.Collections;

public class BPMControlor1 : MonoBehaviour
{

	public AudioSource music = null;
	public float tempo = 120.0f;
	public int errorMargin = 100; // en MS
	public int offsetStart = 0; // EN MS
	public float loopTime = 30f;

	private float nextBeatSample;
	private float samplePeriod;
	private float sampleOffset;
	private float currentSample;

	// NOTIFICATEURS
	
	ArrayList observers;
	
	public void Awake ()
	{
		this.observers = new ArrayList ();
		samplePeriod = (60f / (tempo * 0.5f)) * music.clip.frequency;
		nextBeatSample = 0f;
	}

	public void Start() {
		music.Play ();
		nextBeatSample = 0f;
		StartCoroutine(BeatCheck());
	}
	
	public void connect (TempoReceiver r)
	{
		this.observers.Add (r);
	}


	private void notifyChildren ()
	{
		Debug.Log ("Tap");

	}


	IEnumerator BeatCheck ()
	{
		while (music.isPlaying) {
			currentSample = (float)AudioSettings.dspTime * music.clip.frequency;
			
			if (currentSample >= (nextBeatSample + sampleOffset)) {
				this.notifyChildren();
				nextBeatSample += samplePeriod;
			}
			
			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}
	

	

}
