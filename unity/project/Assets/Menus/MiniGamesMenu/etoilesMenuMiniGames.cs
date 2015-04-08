using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class etoilesMenuMiniGames : MonoBehaviour {

	private int nbrEtoiles;
	public int idMiniGame;
	// Use this for initialization
	void Start () {
		nbrEtoiles=PlayerPrefs.GetInt("etoileLevel"+idMiniGame,0);
		for(int i=nbrEtoiles+1;i<=3;i++)
		{
			GameObject.Find ("s"+i).GetComponent<Image>().enabled=false;
		}
	}
	

}
