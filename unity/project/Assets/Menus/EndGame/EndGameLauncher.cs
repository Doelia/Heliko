using UnityEngine;
using System.Collections;

public class EndGameLauncher : HelikoObject {

	public int idMiniGame;
	public int nbFails;
	public int pourcentSuccess;

	public void goEndGame() {
		GameObject.DontDestroyOnLoad(this.gameObject);
		this.GetComponent<LoadOnClick>().LoadScene(constantes.getNumSceneEndGame());
	}

}
