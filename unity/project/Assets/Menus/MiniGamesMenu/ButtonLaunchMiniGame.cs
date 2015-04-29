using UnityEngine;
using System.Collections;

public class ButtonLaunchMiniGame : MonoBehaviour {

	public void click() {
		int idMiniGame = this.transform.parent.GetComponent<IdMiniGame>().id;
		GameObject.Find ("Mini jeux").GetComponent<MiniGameLoader>().loadMiniGame(idMiniGame);
	}

}
