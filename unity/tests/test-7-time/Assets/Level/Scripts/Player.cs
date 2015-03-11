using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver {

	public PlayerEventListener playerEventListener;
	public BeatCounter beatCounter;

	void Start () {
		playerEventListener.connect (this);
	}

	public void onFinger (int type) {
		Debug.Log ("Score finger : " + beatCounter.getRelativeScore());
	}

}
