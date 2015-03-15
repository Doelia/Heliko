using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, PlayerEventReceiver, LevelScriptedReceiver {

	public PlayerEventListener playerEventListener;
	public BeatCounter beatCounter; // DEBUG
	public LevelScripted level;

	void Start () {
		playerEventListener.connect (this);
		level.connect (this);
	}

	public void onEventType(int type) {

	}

	public void onFailure() {
		Debug.Log("onFailure");
	}

	public void onFinger (int type) {
		Debug.Log ("Score finger  : " + beatCounter.getRelativeScore());
		
	}

}
