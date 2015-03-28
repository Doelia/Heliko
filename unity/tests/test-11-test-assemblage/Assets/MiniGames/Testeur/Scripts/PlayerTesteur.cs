using UnityEngine;
using System.Collections;

public class PlayerTesteur : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver {

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;
	public AudioSource clap;

	void Start () {
		playerEventListener.connect (this);
		playerActions.connect (this);
	}

	public void changeColor(bool isGood) {
		this.GetComponent<SpriteRenderer>().color = isGood ? new Color(0.7f,1,0.7f) : new Color(1,.7f,.7f);
	}

	public void onFailure() {
		this.changeColor(false);
	}

	public void onFinger (int type) {
		clap.Play();
		this.changeColor(playerActions.isGood(type));
	}

}
