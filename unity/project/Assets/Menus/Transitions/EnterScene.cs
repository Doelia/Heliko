using UnityEngine;
using System.Collections;

public class EnterScene : HelikoObject {

	private TransitionScreen loadingScreen;
	public GameObject prefab;

	public void Awake() {
		if (GameObject.Find ("TransitionScreen") == null) {
			GameObject o =Instantiate(prefab);
			o.name = "TransitionScreen";
		}
	}

	public new void Start() {
		base.Start ();
		loadingScreen = this.GetTransitionScreen();
		if (loadingScreen.LoadingScreenIsOpen())
			StartCoroutine(loadingScreen.FadeOutBlackScreen());
	}

	
}
