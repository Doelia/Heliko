using UnityEngine;
using System.Collections;

public class AnimatedSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public int delay = 20; 

	int cpt = 0;

	void playAnimation() {
		foreach (Transform child in transform) {
			child.animation. Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cpt % delay == 0) {
			playAnimation();
		}
		cpt++;
	}
}
