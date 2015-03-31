using UnityEngine;
using System.Collections;

public class PlayerChampi : HelikoObject, PlayerEventReceiver {

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

	public void Start () {
		base.Start();
		playerActions = getPlayerActions();

		getPlayerEventListener().connect (this);
		animGauche = mainGaucheTransform.GetComponent<Animator>();
		animDroite = mainDroiteTransform.GetComponent<Animator>();
		animChampiPlayer = champiPlayer.GetComponent<Animator>();
		animCarapace = carapace.GetComponent<Animator>();
	}

	public void onFinger (int type) {
		if (type == 1) {
			animGauche.SetTrigger ("Down");
		} else if (type == 3) {
			animDroite.SetTrigger ("Down");
		} else {
			return;
		}

		animCarapace.SetTrigger("Move");
		animChampiPlayer.SetTrigger("Move");

		bool isGood = playerActions.isGood(type);
		if (isGood) {
			soundGood.GetComponent<AudioSource>().Play();
		} else {
			soundBad.GetComponent<AudioSource>().Play();
		}
	}

}
