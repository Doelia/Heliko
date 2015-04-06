using UnityEngine;
using System.Collections;

public class EnableIfDevMode : HelikoObject {

	public new void Start () {
		if (isStart) return;
		base.Start();
		this.gameObject.SetActive(constantes.showDebugGUI);
	}
	

}
