using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class etoilesMenuMiniGames : HelikoObject {

	private int nbrEtoiles;
	private int idMiniGame;
	// Use this for initialization
	new void Start () {
		base.Start();
		idMiniGame=constantes.getIdMiniGameFromNumScene(this.transform.parent.GetComponent<numSceneLevel>().numLevel);
		nbrEtoiles=PlayerPrefs.GetInt("etoileLevel"+idMiniGame,0);
		for(int i=nbrEtoiles;i<3;i++)
		{
			GetComponentsInChildren<Image>()[i].enabled=false;
		}
	}
	

}
