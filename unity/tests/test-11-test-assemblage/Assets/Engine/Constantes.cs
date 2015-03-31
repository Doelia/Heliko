using UnityEngine;
using System.Collections;

public class Constantes : MonoBehaviour {

	public bool showDebugGUI = false;
	public bool instantCalcul = false;
	public bool devMode = true;

	public void Awake() {
		if (devMode) {
			showDebugGUI = true;
			instantCalcul = true;
		}
	}

}
