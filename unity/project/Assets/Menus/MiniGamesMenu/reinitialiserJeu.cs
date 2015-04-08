using UnityEngine;
using System.Collections;

public class reinitialiserJeu : MonoBehaviour {

	// Use this for initialization
	public void Reinitialiser () {
		PlayerPrefs.DeleteAll ();
		Application.LoadLevel (0);
	}
	

}
