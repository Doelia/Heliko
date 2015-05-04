using UnityEngine;
using System.Collections;

public class MagicienControleur : HelikoObject, PlayerEventReceiver, PlayerActionReceiver {

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
		animObjet.ResetTrigger("reset");
		animObjet.ResetTrigger("fade");
		
		if (type == 1) {
			animBras.ResetTrigger("down");
			animObjet.ResetTrigger("down");
			animBras.SetTrigger ("up");
			animObjet.SetTrigger ("up");
			bool isGood = playerActions.IsGood (2);
			MagicObject o = GameObject.Find ("magicObject").GetComponent<MagicObject>();
			o.Transformer(isGood);
		} else if (type == 2) {
			animBras.ResetTrigger("up");
			animObjet.ResetTrigger("up");
			animBras.SetTrigger ("down");
			animObjet.SetTrigger ("down");
			playerActions.IsGood (1);
		}
	}
}
