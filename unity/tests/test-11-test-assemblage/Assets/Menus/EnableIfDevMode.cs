using UnityEngine;
using System.Collections;

public class EnableIfDevMode : HelikoObject {

	public void Start () {
		base.Start();
		Debug.Log ("Start "+gameObject.name);
		this.gameObject.SetActive(constantes.showDebugGUI);
	}
	

}
