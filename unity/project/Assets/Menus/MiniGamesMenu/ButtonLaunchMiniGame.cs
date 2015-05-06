using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonLaunchMiniGame : HelikoObject {

	public Sprite enable;
	public Sprite disable;

	public int idMiniGame;

	public void click() {
		if (GetUnlockerManager().isUnlocked(idMiniGame)) {
			GameObject.Find ("go").GetComponent<AudioSource>().Play();
			GameObject.Find ("Mini jeux").GetComponent<MiniGameLoader>().loadMiniGame(idMiniGame);
		} else {
			GameObject.Find ("bad").GetComponent<AudioSource>().Play();
		}
	}

	public new void Start() {
		base.Start();

		idMiniGame = this.transform.parent.GetComponent<IdMiniGame>().id;

		if (GetUnlockerManager().isUnlocked(idMiniGame)) {
			this.GetComponent<Image>().sprite = enable;
		} else {
			this.GetComponent<Image>().sprite = disable;
		}
	}



}
