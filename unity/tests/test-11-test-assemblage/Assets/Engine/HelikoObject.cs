using UnityEngine;
using System.Collections;

public class HelikoObject : MonoBehaviour {

	protected Constantes constantes;

	public void Start() {
		if (GameObject.Find ("Constantes") == null) {
			Debug.LogError("Impossible de trouver l'objet Constantes dans la scène");
		} else {
			constantes = GameObject.Find ("Constantes").GetComponent<Constantes>();
		}
		Debug.Log (gameObject.name+" started");
	}

	public BeatCounter getBeatCounter() {
		if (GameObject.Find ("BeatCounter") == null) {
			Debug.LogError("Impossible de trouver l'objet BeatCounter dans la scène");
		}
		return GameObject.Find ("BeatCounter").GetComponent<BeatCounter>();
	}

	public PlayerActions getPlayerActions() {
		if (GameObject.Find ("PlayerActions") == null) {
			Debug.LogError("Impossible de trouver l'objet PlayerActions dans la scène");
		}
		return GameObject.Find ("PlayerActions").GetComponent<PlayerActions>();
	}

	public PlayerEventListener getPlayerEventListener() {
		if (GameObject.Find ("PlayerEventListener") == null) {
			Debug.LogError("Impossible de trouver l'objet PlayerEventListener dans la scène");
		}
		return GameObject.Find ("PlayerEventListener").GetComponent<PlayerEventListener>();
	}


}