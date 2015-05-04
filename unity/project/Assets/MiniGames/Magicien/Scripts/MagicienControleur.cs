using UnityEngine;
using System.Collections;

public class MagicienControleur : HelikoObject, PlayerEventReceiver, PlayerActionReceiver {

	public Transform soundWin;
	public Transform soundFail;
	public Transform soundWoosh;

	private PlayerActions playerActions;
	public Transform bras;
	public Transform objet;

	private Animator animBras;
	private Animator animObjet;

	public new void Start () {
		base.Start();
		playerActions = GetPlayerActions();
		playerActions.Connect (this);
		
		GetPlayerEventListener().connect (this);
		animBras = bras.GetComponent<Animator>();
		animObjet = objet.GetComponent<Animator>();
	}

	public void OnFailure() {
		
	}

	public void OnSuccess() {

	}

	public void OnSuccessLoop() {

	}
	
	public void OnFinger (int type) {
		if (type == 1) {
			animBras.ResetTrigger("down");
			animObjet.ResetTrigger("down");
			animBras.SetTrigger ("up");
			animObjet.SetTrigger ("up");
			soundWoosh.GetComponent<AudioSource>().Play();
			bool isGood = playerActions.IsGood (2);
			if (isGood) {
				soundWin.GetComponent<AudioSource>().Play();
			} else {
				soundFail.GetComponent<AudioSource>().Play();
			}
			MagicObject o = GameObject.Find ("magicObject").GetComponent<MagicObject>();
			o.Transformer(isGood);
		} else if (type == 2) {
			animBras.ResetTrigger("up");
			animObjet.ResetTrigger("up");
			animBras.SetTrigger ("down");
			animObjet.SetTrigger ("down");
			soundWoosh.GetComponent<AudioSource>().Play();
			playerActions.IsGood (1);
		}

	}
}
