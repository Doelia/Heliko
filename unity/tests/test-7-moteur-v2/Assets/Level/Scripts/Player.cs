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
		this.renderer.material.color = new Color (1, 0, 0);
		Debug.Log("onFailure");
	}

	public void onFinger (int type) {
		Debug.Log ("Score finger  : " + beatCounter.getRelativeScore());
		if (!level.isGood(type)) {
			Debug.Log("is not good");
			this.renderer.material.color = new Color (1, 0, 0);
		} else {
			this.renderer.material.color = new Color (0, 1, 0);
		}
	}

}
