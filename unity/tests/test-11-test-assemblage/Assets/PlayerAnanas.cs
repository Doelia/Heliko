using UnityEngine;
using System.Collections;

public class PlayerAnanas : MonoBehaviour, PlayerEventReceiver, PlayerActionReceiver  {

	public AnanasManager parentAnanas;

	public PlayerEventListener playerEventListener;
	public PlayerActions playerActions;

	void Start () {
		playerEventListener.connect (this);
		playerActions.connect (this);
	}

	public void onFailure() {

	}

	public void onFinger (int type) {
		if (type == 1) {
			parentAnanas.removeLast();
		}
	}

}
