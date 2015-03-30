using UnityEngine;
using System.Collections;

public class PlayerAnanas : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver  {

	public AnanasManager parentAnanas;

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;

	public AudioSource good;
	public AudioSource bad;

	void Start () {
		playerEventListener.connect (this);
		playerActions.connect (this);
	}

	public void onFailure() { }

	public void onSuccess() {}

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
