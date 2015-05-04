using UnityEngine;
using System.Collections;

public class MagicienControleur : HelikoObject, PlayerEventReceiver {

	private PlayerActions playerActions;
	public Transform bras;
	public Transform objet;

	private Animator animBras;
	private Animator animObjet;

	public new void Start () {
		base.Start();
		playerActions = GetPlayerActions();
		
		GetPlayerEventListener().connect (this);
		animBras = bras.GetComponent<Animator>();
		animObjet = objet.GetComponent<Animator>();
	}
	
	public void OnFinger (int type) {
		if (type == 1) {
			animBras.ResetTrigger("down");
			animObjet.ResetTrigger("down");
			animBras.SetTrigger ("up");
			animObjet.SetTrigger ("up");
			bool isGood = this.GetPlayerActions().IsGood(2);
			MagicObject o = GameObject.Find ("magicObject").GetComponent<MagicObject>();
			o.transform(isGood);
		} else {
			animBras.ResetTrigger("up");
			animObjet.ResetTrigger("up");
			animBras.SetTrigger ("down");
			animObjet.SetTrigger ("down");
		}
	}
}
