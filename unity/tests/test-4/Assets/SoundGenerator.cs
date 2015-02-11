using UnityEngine;
using System.Collections;

public class SoundGenerator : MonoBehaviour {

	float tempo = 130.0f;
	public AudioSource clip;
	public Transform obj;

	float d = 0;
	float dFake = 0;
	float lastTime;

	float getDeltaTempo() {
		return (60.0f / tempo);
	}

	// Use this for initialization
	void Start () {
	
	}

	void playSound() {
		clip.Play ();
	}

	void playAnimation() {
		obj.animation.Stop ();
		obj.animation.Play ();
	}

	// Update is called once per frame
	void Update () {

		float delta = Time.time - lastTime;
		lastTime = Time.time;

		d += delta;
		dFake += delta;
		
		if (d > this.getDeltaTempo()) {

			Debug.Log("Play Animation: "+dFake+", delta="+delta+", deltaTempo="+this.getDeltaTempo());
			d = (d - this.getDeltaTempo());
			dFake = 0;
			playSound();
			playAnimation();
		}

	}
}
