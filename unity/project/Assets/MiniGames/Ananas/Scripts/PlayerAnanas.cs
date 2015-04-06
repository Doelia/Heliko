using UnityEngine;
using System.Collections;

public class PlayerAnanas : HelikoObject, PlayerEventReceiver  {

	private PlayerActions playerActions;

	public AnanasManager parentAnanas;
	public AudioSource good;

	private Animator animBody;
	public Animator animLangue;

	public new void Start () {
		playerActions = GetPlayerActions();
		GetPlayerEventListener().connect (this);
		animBody = GetComponent<Animator>();
	}

	public void OnFinger (int type) {
		if (type == 1 || type == 2) {
			animBody.SetTrigger("go");
			animLangue.SetTrigger("go");
			parentAnanas.removeLast();
			bool ok = playerActions.IsGood(1);
			if (ok) {
				good.Play();
			}
		}

	}

}
