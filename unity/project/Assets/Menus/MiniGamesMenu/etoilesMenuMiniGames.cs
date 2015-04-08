using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class etoilesMenuMiniGames : MonoBehaviour {

	private int nbrEtoiles;
	private int idMiniGame;
	// Use this for initialization
	void Start () {
		idMiniGame=this.transform.parent.GetComponent<numSceneLevel>().numLevel;
		nbrEtoiles=PlayerPrefs.GetInt("etoileLevel"+idMiniGame,0);
		for(int i=nbrEtoiles+1;i<=3;i++)
		{
			GameObject.Find ("s"+i).GetComponent<Image>().enabled=false;
		}
	}
	

}
