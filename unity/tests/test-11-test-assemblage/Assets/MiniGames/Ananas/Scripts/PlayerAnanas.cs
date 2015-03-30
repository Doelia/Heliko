using UnityEngine;
using System.Collections;

public class PlayerAnanas : HelikoObject, PlayerEventReceiver  {

	private PlayerActions playerActions;

	public AnanasManager parentAnanas;
	public AudioSource good;
	public AudioSource bad;

	public void Start () {
		playerActions = getPlayerActions();
		getPlayerEventListener().connect (this);
	}

	public void onFinger (int type) {
		if (type == 1) {
			parentAnanas.removeLast();
			bool ok = playerActions.isGood(type);
			if (ok) {
				good.Play();
			} else {
				bad.Play();
			}
		}

	}

}
