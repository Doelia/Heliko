using UnityEngine;
using System.Collections;

public class MiniGameLoader : HelikoObject {


	public void loadMiniGame(int idMiniGame) {
		this.GetComponent<LoadOnClick>().LoadScene(constantes.getTutoNumSceneFromIdMinigame(idMiniGame));
	}
}
