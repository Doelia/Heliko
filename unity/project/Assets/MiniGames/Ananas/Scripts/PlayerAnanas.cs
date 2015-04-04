using UnityEngine;
using System.Collections;

public class PlayerAnanas : HelikoObject, PlayerEventReceiver  {

	private PlayerActions playerActions;

	public AnanasManager parentAnanas;
	public AudioSource good;
	public AudioSource bad;

	public new void Start () {
		playerActions = GetPlayerActions();
		GetPlayerEventListener().connect (this);
	}

	public void OnFinger (int type) {
		if (type == 1) {
			parentAnanas.removeLast();
			bool ok = playerActions.IsGood(type);
			if (ok) {
				good.Play();
			} else {
				bad.Play();
			}
		}

	}

}
