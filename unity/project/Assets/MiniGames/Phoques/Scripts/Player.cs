using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver {

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;

	private AudioSource clap;
	private Animator anim;
	private Transform tete;

	void Start () {
		playerEventListener.connect (this);
		playerActions.Connect (this);
		clap = GetComponent<AudioSource>();

		foreach (Transform s in transform) {
			tete = s;
		}

		anim = tete.GetComponent<Animator>();
	}

	public void changeColor(bool isGood) {
		tete.GetComponent<SpriteRenderer>().color = isGood ? new Color(0.7f,1,0.7f) : new Color(1,.7f,.7f);
	}

	public void OnFailure() {
		this.changeColor(false);
	}

	public void OnSuccess() {}
	public void OnSuccessLoop() {
		//Debug.Log ("good Loop!");
	}

	public void OnFinger (int type) {
		anim.SetTrigger ("change");
		clap.Play();
		this.changeColor(playerActions.IsGood(type));
	}

}
