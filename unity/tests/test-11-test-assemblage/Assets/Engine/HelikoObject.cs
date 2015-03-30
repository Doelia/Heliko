using UnityEngine;
using System.Collections;

public class HelikoObject : MonoBehaviour {

	public BeatCounter getBeatCounter() {
		return GameObject.Find ("BeatCounter").GetComponent<BeatCounter>();
	}

	public PlayerActions getPlayerActions() {
		return GameObject.Find ("PlayerActions").GetComponent<PlayerActions>();
	}

	public PlayerEventListener getPlayerEventListener() {
		return GameObject.Find ("PlayerEventListener").GetComponent<PlayerEventListener>();
	}


}