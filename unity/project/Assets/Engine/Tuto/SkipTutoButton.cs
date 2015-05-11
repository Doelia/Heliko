using UnityEngine;
using System.Collections;

public class SkipTutoButton : HelikoObject {

	private int idMiniGame;

	public new void Start () {

		base.Start ();

		idMiniGame = GameObject.Find ("Tutorial").GetComponent<Tuto>().idGame;

		if (GetUnlockerManager().haveSuccessTuto(idMiniGame)) {
			GameObject.Destroy(this.transform.gameObject);
		}
	}
	
	
}
