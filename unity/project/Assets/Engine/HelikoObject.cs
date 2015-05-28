using UnityEngine;
using System.Collections;

public class HelikoObject : MonoBehaviour {

	protected Constantes constantes;
	protected bool isStart = false;
	protected UnLockerManager unlockerManager = null;

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

	public TransitionScreen GetTransitionScreen() {
		if (GameObject.Find ("TransitionScreen") == null) {
			Debug.LogError("Impossible de trouver l'objet TransitionScreen dans la scène");
			return null;
		} else {
			TransitionScreen o = GameObject.Find ("TransitionScreen").GetComponent<TransitionScreen>();
			if (!o.isStart) o.Start ();
			return o;
		}
	}

	public BeatCounter GetBeatCounter() {
		if (GameObject.Find ("BeatCounter") == null) {
			Debug.LogError("Impossible de trouver l'objet BeatCounter dans la scène");
			return null;
		}
		BeatCounter o = GameObject.Find ("BeatCounter").GetComponent<BeatCounter>();
		if (!o.isStart) o.Start ();
		return o;
	}

	public PlayerActions GetPlayerActions() {
		if (GameObject.Find ("PlayerActions") == null) {
			Debug.LogError("Impossible de trouver l'objet PlayerActions dans la scène");
		}
		return GameObject.Find ("PlayerActions").GetComponent<PlayerActions>();
	}

	public PlayerEventListener GetPlayerEventListener() {
		if (GameObject.Find ("PlayerEventListener") == null) {
			Debug.LogError("Impossible de trouver l'objet PlayerEventListener dans la scène");
		}
		return GameObject.Find ("PlayerEventListener").GetComponent<PlayerEventListener>();
	}

	public UnLockerManager GetUnlockerManager() {
		if (unlockerManager == null)
			unlockerManager = new UnLockerManager();
		return unlockerManager;
	}

}