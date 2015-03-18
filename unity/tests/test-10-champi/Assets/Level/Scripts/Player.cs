using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver {

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;

	private AudioSource clap;

	public Transform mainGaucheTransform;
	public Transform mainDroiteTransform;
	public Transform carapace;

	private Animator animGauche;
	private Animator animDroite;

	void Start () {
		playerEventListener.connect (this);
		playerActions.connect (this);
		clap = GetComponent<AudioSource>();
		animGauche = mainGaucheTransform.GetComponent<Animator>();
		animDroite = mainDroiteTransform.GetComponent<Animator>();
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

		carapace.GetComponent<Animator>().SetTrigger("Move");
		clap.Play();
		this.changeColor(playerActions.isGood(type));
	}

}
