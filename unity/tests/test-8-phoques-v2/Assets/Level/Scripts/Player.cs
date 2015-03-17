using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver {

	public PlayerEventListener playerEventListener;
	public PlayerActions pa;

	private AudioSource clap;
	private Animator anim;
	private Transform tete;

	void Start () {
		playerEventListener.connect (this);
		pa.connect (this);
		clap = GetComponent<AudioSource>();

		foreach (Transform s in transform) {
			tete = s;
		}

		anim = tete.GetComponent<Animator>();
	}

	public void changeColor(bool isGood) {
		tete.GetComponent<SpriteRenderer>().color = isGood ? new Color(0.7f,1,0.7f) : new Color(1,.7f,.7f);
	}

	public void onFailure() {
		this.changeColor(false);
	}

	public void onFinger (int type) {
		anim.SetTrigger ("change");
		clap.Play();
		this.changeColor(pa.isGood(type));
	}

}
