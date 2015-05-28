using UnityEngine;
using System.Collections;

public class backMiniGames : MonoBehaviour {

	public GameObject accueil;
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			accueil.SetActive(true);
			this.transform.gameObject.SetActive(false);
		}
	}
	
	public void click() {
		accueil.SetActive(true);
		this.transform.gameObject.SetActive(false);
	}

}
