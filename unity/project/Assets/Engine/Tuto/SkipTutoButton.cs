using UnityEngine;
using System.Collections;

public class SkipTutoButton : HelikoObject {

	private int idMiniGame;

	public new void Start () {

		base.Start ();

		if (constantes.skipTutoAlwaysEnable) {
			return;
		}
	
		idMiniGame = GameObject.Find ("Tutorial").GetComponent<Tuto>().idGame;

		if (PlayerPrefs.GetInt("niveauReussi"+idMiniGame,-1) <= 0) {
			GameObject.Destroy(this.transform.gameObject);
		}
	}
	
	
}
