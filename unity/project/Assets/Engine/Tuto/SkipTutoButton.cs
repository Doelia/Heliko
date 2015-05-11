using UnityEngine;
using System.Collections;

public class SkipTutoButton : MonoBehaviour {

	private int idMiniGame;
	void Start () {
	
			idMiniGame= GameObject.Find ("Tutorial").GetComponent<Tuto>().idGame;

			if (PlayerPrefs.GetInt("niveauReussi"+idMiniGame,-1)<=0) {
			GameObject.Destroy(this.transform.gameObject);
		}
	}
	
	
}
