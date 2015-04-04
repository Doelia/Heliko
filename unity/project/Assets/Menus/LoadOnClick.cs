using UnityEngine;
using System.Collections;

public class LoadOnClick : HelikoObject {

	private TransitionScreen loadingScreen;

	public new void Start() {
		base.Start ();
		loadingScreen = this.getTransitionScreen();
	}

	public void LoadScene(int level) {
		StartCoroutine(loadLevel(level));
	}

	public void ReloadScene() {
		StartCoroutine(loadLevel(Application.loadedLevel));
	}

	public IEnumerator loadLevel(int level) {
		yield return StartCoroutine(loadingScreen.openLoadingScene());
		loadingScreen.keepItAfterLoading();
		Application.LoadLevelAsync(level);
	}

}

