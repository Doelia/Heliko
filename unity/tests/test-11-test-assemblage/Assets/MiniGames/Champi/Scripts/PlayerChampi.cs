using UnityEngine;
using System.Collections;

public class PlayerChampi : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver {

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;
	public Transform soundGood;
	public Transform soundBad;

	public Transform mainGaucheTransform;
	public Transform mainDroiteTransform;
	public Transform carapace;
	public Transform champiPlayer;

	private Animator animGauche;
	private Animator animDroite;
	private Animator animCarapace;
	private Animator animChampiPlayer;

	void Start () {
		playerEventListener.connect (this);
		playerActions.connect (this);
		animGauche = mainGaucheTransform.GetComponent<Animator>();
		animDroite = mainDroiteTransform.GetComponent<Animator>();
		animChampiPlayer = champiPlayer.GetComponent<Animator>();
		animCarapace = carapace.GetComponent<Animator>();
	}

	public void changeColor(bool isGood) {
		carapace.GetComponent<SpriteRenderer>().color = isGood ? new Color(0.7f,1,0.7f) : new Color(1,.7f,.7f);
	}

	public void onFailure() {
		this.changeColor(false);
	}

	public void onFinger (int type) {
		if (type == 1) {
			animGauche.SetTrigger ("Down");
		} else if (type == 2) {
			animDroite.SetTrigger ("Down");
		} else if (type == 3) {
			animGauche.SetTrigger ("Down");
			animDroite.SetTrigger ("Down");
		}

		animCarapace.SetTrigger("Move");
		animChampiPlayer.SetTrigger("Move");


		bool isGood = playerActions.isGood(type);
		if (isGood) {
			soundGood.GetComponent<AudioSource>().Play();
		} else {
			soundBad.GetComponent<AudioSource>().Play();
		}
		this.changeColor(isGood);
	}

}
