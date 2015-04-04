using UnityEngine;
using System.Collections;

public class PlayerTesteur : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver {

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;
	public AudioSource clap;

	private Animator anim;


	void Start () {
		playerEventListener.connect (this);
		playerActions.Connect (this);
		anim = GetComponent < Animator >();
	}

	public void changeColor(bool isGood) {
		this.GetComponent<SpriteRenderer>().color = isGood ? new Color(0.3f,1,0.3f) : new Color(1,.3f,.3f);
	}

	public void OnFailure() {
		this.changeColor(false);
	}

	public void OnSuccess() {}
	public void OnSuccessLoop() {}

	public void OnFinger (int type) {
		anim.SetTrigger ("change");
		clap.Play();
		this.changeColor(playerActions.IsGood(type));
	}

}
