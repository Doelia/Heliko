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
		switch (id) {
		case 1: // Champi
			return 1;
		case 2: // Ananas
			return 2;
		case 3: // Magicien
			return 3;
		case 4: // Phoques
			return 4;
		}
		Debug.LogWarning("Le jeu #"+id+" n'a pas de scène associé");
		return id;
	}

	public int getIdMiniGameFromNumScene(int numScene) {
		switch (numScene) {
		case 1: // Champi
			return 1;
		/*case 2: // Ananas
			return 2;*/
		case 3: // Magicien
			return 2;
		/*case 4: // Phoques
			return 4;*/
		case 5: // Escargot
			return 3;
		}
		Debug.LogWarning("La scène #"+numScene+" n'a pas de mini jeu associé");
		return numScene;
	}

	public int getTutoNumSceneFromIdMinigame(int id) {
		if (id == 1) {
			return 5;
		}
		if (id == 3) {
			return 6;
		}
		return this.getNumSceneFromIdMiniGame(id);
	}

}
