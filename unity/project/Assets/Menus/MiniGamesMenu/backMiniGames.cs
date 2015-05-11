using UnityEngine;
using System.Collections;

public class backMiniGames : MonoBehaviour {

	public GameObject accueil;
	
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			accueil.SetActive(true);
			this.transform.gameObject.SetActive(false);
		}
		
	}
}
