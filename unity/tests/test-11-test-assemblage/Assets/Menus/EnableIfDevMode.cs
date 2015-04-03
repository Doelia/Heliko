using UnityEngine;
using System.Collections;

public class EnableIfDevMode : HelikoObject {

	void Awake () {
		this.gameObject.SetActive(constantes.showDebugGUI);
	}
	

}
