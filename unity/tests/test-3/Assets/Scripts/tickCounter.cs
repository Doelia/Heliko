using UnityEngine;
using System.Collections;

public class tickCounter : MonoBehaviour {

	int cpt = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown ("a")) {
			this.guiText.text = "TPB: "+cpt;
			cpt = 0;
		}

		cpt++;

	}
}
