using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EtoilesMenuMiniGames : HelikoObject {

	private int nbrEtoiles;
	private int idMiniGame;

	public new void Start () {
		base.Start();
		idMiniGame = constantes.getIdMiniGameFromNumScene(this.transform.parent.GetComponent<NumSceneLevel>().numLevel);
		nbrEtoiles = PlayerPrefs.GetInt("etoileLevel"+idMiniGame, 0);
		for (int i = nbrEtoiles; i < 3; i++) {
			GetComponentsInChildren<Image>()[i].enabled=false;
		}
	}
	

}
