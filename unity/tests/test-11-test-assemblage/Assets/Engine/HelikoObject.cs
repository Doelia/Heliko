using UnityEngine;
using System.Collections;

public class HelikoObject : MonoBehaviour {

	protected Constantes constantes;
	protected bool isStart = false;

	public void Start() {
		if (isStart) return;
		isStart = true;
		//Debug.Log ("Starting "+this.gameObject.name);
		if (GameObject.Find ("Constantes") == null) {
			Debug.LogError("Impossible de trouver l'objet Constantes dans la scène");
		} else {
			constantes = GameObject.Find ("Constantes").GetComponent<Constantes>();
		}
	}

	public TransitionScreen getTransitionScreen() {
		if (GameObject.Find ("TransitionScreen") == null) {
			Debug.LogError("Impossible de trouver l'objet TransitionScreen dans la scène");
		}
		TransitionScreen o = GameObject.Find ("TransitionScreen").GetComponent<TransitionScreen>();
		if (!o.isStart) o.Start ();
		return o;
	}

	public BeatCounter getBeatCounter() {
		if (GameObject.Find ("BeatCounter") == null) {
			Debug.LogError("Impossible de trouver l'objet BeatCounter dans la scène");
		}
		BeatCounter o = GameObject.Find ("BeatCounter").GetComponent<BeatCounter>();
		if (!o.isStart) o.Start ();
		return o;
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