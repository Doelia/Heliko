using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver, LevelScriptedReceiver {

	public PlayerEventListener playerEventListener;
	public LevelScripted level;

	private AudioSource clap;
	private Animator anim;
	private Transform tete;

	void Start () {
		playerEventListener.connect (this);
		level.connect (this);
		clap = GetComponent<AudioSource>();

		foreach (Transform s in transform) {
			tete = s;
		}

		anim = tete.GetComponent<Animator>();
	}

	public void changeColor(bool isGood) {
		tete.GetComponent<SpriteRenderer>().color = isGood ? new Color(0,1,0) : new Color(1,0,0);
	}

	public void onEventType(int type) {

	}

	public void onFailure() {
		this.changeColor(false);
	}

	public void onFinger (int type) {
		anim.SetTrigger ("change");
		clap.Play();
		this.changeColor(level.isGood(type));
	}

}
