using UnityEngine;
using System.Collections;

public class AnimatedSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float tempo = 144;
	float d;

	float getDeltaTempo() {
		return 60.0f / tempo;
	}

	int deltaTimeMilli() {
		return (int) (Time.deltaTime * 1000);
	}

	void playAnimation() {
		foreach (Transform child in transform) {
			foreach (Transform cchild in child.transform) {
				cchild.animation. Play();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("d="+d);
		d += Time.deltaTime;

		if (d > this.getDeltaTempo()) {
			Debug.Log("Play Animation!");
			d = 0;
			playAnimation();
		}

	}
}
