using UnityEngine;
using System.Collections;

public class Constantes : MonoBehaviour {

	[HideInInspector]
	public bool showDebugGUI = false;
	[HideInInspector]
	public bool instantCalcul = false;
	[HideInInspector]
	public bool showDetailOnEndGame = false;

	public bool devMode = true;

	[HideInInspector]

	public int getNumSceneEndGame() {
		return 4;
	}

	public void Awake() {
		if (devMode) {
			showDebugGUI = true;
			instantCalcul = true;
			showDetailOnEndGame = true;
		}
	}

	public int getNumSceneFromIdMiniGame(int id) {
		return id;
	}

	public int getIdMiniGameFromNumScene(int numScene) {
		return numScene;
	}

}
