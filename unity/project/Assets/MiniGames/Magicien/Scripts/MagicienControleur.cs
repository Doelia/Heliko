using UnityEngine;
using System.Collections;

public class MagicienControleur : HelikoObject, PlayerEventReceiver {

	private PlayerActions playerActions;
	public Transform bras;

	private Animator animBras;

	public new void Start () {
		base.Start();
		playerActions = GetPlayerActions();
		
		GetPlayerEventListener().connect (this);
		animBras = bras.GetComponent<Animator>();
	}
	
	public void OnFinger (int type) {
		if (type == 1) {
			animBras.SetTrigger ("up");
		} else {
			animBras.SetTrigger ("down");
		}
	}
}
