using UnityEngine;
using System.Collections;

public class PlayerChampi : HelikoObject, PlayerEventReceiver {

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

	private PlayerActions playerActions;

	public new void Start () {
		base.Start();
		playerActions = GetPlayerActions();

		GetPlayerEventListener().connect (this);
		animGauche = mainGaucheTransform.GetComponent<Animator>();
		animDroite = mainDroiteTransform.GetComponent<Animator>();
		animChampiPlayer = champiPlayer.GetComponent<Animator>();
		animCarapace = carapace.GetComponent<Animator>();
	}

	public void OnFinger (int type) {
		if (type == 1) {
			animGauche.SetTrigger ("Down");
		} else if (type == 2) {
			animDroite.SetTrigger ("Down");
		} else {
			return;
		}

		animCarapace.SetTrigger("Move");
		animChampiPlayer.SetTrigger("Move");

		bool isGood = playerActions.IsGood(type);
		if (isGood) {
			soundGood.GetComponent<AudioSource>().Play();
		} else {
			soundBad.GetComponent<AudioSource>().Play();
		}
	}

}
