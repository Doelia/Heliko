using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver {

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;

	private AudioSource clap;

	public Transform animationTransform;
	public Transform carapace;

	private Animator handAnimation;

	void Start () {
		playerEventListener.connect (this);
		playerActions.connect (this);
		clap = GetComponent<AudioSource>();
		handAnimation = animationTransform.GetComponent<Animator>();
	}

	public void changeColor(bool isGood) {
		carapace.GetComponent<SpriteRenderer>().color = isGood ? new Color(0.7f,1,0.7f) : new Color(1,.7f,.7f);
	}

	public void onFailure() {
		this.changeColor(false);
	}

	public void onFinger (int type) {
		handAnimation.SetTrigger ("Move");
		carapace.GetComponent<Animator>().SetTrigger("Move");
		clap.Play();
		this.changeColor(playerActions.isGood(type));
	}

}
