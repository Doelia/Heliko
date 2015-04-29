using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonLaunchMiniGame : MonoBehaviour {

	public Sprite enable;
	public Sprite disable;

	public void click() {
		int idMiniGame = this.transform.parent.GetComponent<IdMiniGame>().id;
		GameObject.Find ("Mini jeux").GetComponent<MiniGameLoader>().loadMiniGame(idMiniGame);
	}

	public void Start() {
		if (true) {
			this.GetComponent<Image>().sprite = enable;
		} else {
			this.GetComponent<Image>().sprite = disable;
		}
	}



}
