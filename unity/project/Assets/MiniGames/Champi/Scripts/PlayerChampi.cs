using UnityEngine;
using System.Collections;

public class PlayerChampi : HelikoObject, PlayerEventReceiver {

	public AudioSource soundGoodBlue;
	public AudioSource soundGoodOrange;
	public AudioSource soundBad;

	public Transform mainGaucheTransform;
	public Transform mainDroiteTransform;
	public Transform carapace;
	public Transform champiPlayer;

	public ParticleSystem leftParticles;
	public ParticleSystem rightParticles;
	public ParticleSystem rightFailParticles;
	public ParticleSystem leftFailParticles;

	private Animator animGauche;
	private Animator animDroite;
	private Animator animCarapace;
	private Animator animChampiPlayer;

	private PlayerActions playerActions;
	private bool leftPlayed;

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
			leftPlayed = false;
		} else if (type == 2) {
			animDroite.SetTrigger ("Down");
			leftPlayed = true;
		} else {
			return;
		}
		


		animCarapace.SetTrigger("Move");
		animChampiPlayer.SetTrigger("Move");

		bool isGood = playerActions.IsGood(type);
		if (isGood) {
			if(leftPlayed) {
				soundGoodOrange.Play();
				leftParticles.Play();
			} else {
				soundGoodBlue.Play();
				rightParticles.Play();
			}
		} else {
			soundBad.GetComponent<AudioSource>().Play();
			if(leftPlayed) {
				leftFailParticles.Play();
			} else {
				rightFailParticles.Play();
			}
		}
	}

}
