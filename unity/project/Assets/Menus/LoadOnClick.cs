using UnityEngine;
using System.Collections;

public class LoadOnClick : HelikoObject {

	private TransitionScreen loadingScreen;
	public AsyncOperation loadingScene;

	public new void Start() {
		base.Start ();
		loadingScreen = this.GetTransitionScreen(); // Auto créé s'il n'existe pas
	}

	public void LoadScene(int level) {
		StartCoroutine(loadLevel(level));
	}

	public void ReloadScene() {
		StartCoroutine(loadLevel(Application.loadedLevel));
	}

	public IEnumerator loadLevel(int level) {
		loadingScene = Application.LoadLevelAsync(level);
		yield return StartCoroutine(loadingScreen.FadeInBlackScreen(loadingScene));
		loadingScreen.KeepItAfterLoading();
	}

}

