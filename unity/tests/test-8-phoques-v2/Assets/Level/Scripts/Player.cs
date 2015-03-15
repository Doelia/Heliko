using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver, LevelScriptedReceiver {

	public PlayerEventListener playerEventListener;
	public LevelScripted level;

	public AudioSource clap;
	private Animator anim;

	void Start () {
		playerEventListener.connect (this);
		level.connect (this);
		clap = GetComponent<AudioSource>();
	}

	public void onEventType(int type) {

	}

	public void onFailure() {
		Debug.Log("onFailure");
	}

	public void onFinger (int type) {
		anim.SetTrigger ("change");
		clap.Play();
		if (level.isGood(type)) {

		}
	}

}
