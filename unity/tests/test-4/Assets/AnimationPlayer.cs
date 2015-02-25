using UnityEngine;
using System.Collections;

public class AnimationPlayer : MonoBehaviour {

	public AudioSource clip;
	public Transform obj;
	public Transform sn;

	void Start () {
	}

	void playSound() {
		clip.Play ();
	}

	void playAnimation() {
		obj.animation.Stop ();
		obj.animation.Play ();
	}

	public void onNotify() {
		playSound();
		playAnimation();
	}

	// Update is called once per frame
	void Update () {

	}
}
