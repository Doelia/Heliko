using UnityEngine;
using System.Collections;

public class LoadOnClick : HelikoObject {

	private TransitionScreen loadingScreen;

	public new void Start() {
		base.Start ();
		loadingScreen = this.getTransitionScreen();
	}

	public void LoadScene(int level) {
		Debug.Log ("LoadScene");
		StartCoroutine(loadLevel(level));
	}

	public IEnumerator loadLevel(int level) {
		yield return StartCoroutine(loadingScreen.openLoadingScene());
		loadingScreen.keepItAfterLoading();
		Application.LoadLevelAsync(level);
	}

	public void ReloadScene() {
		loadingScreen.openLoadingScene();
		loadingScreen.keepItAfterLoading();
		Application.LoadLevel(Application.loadedLevel);
	}
}

