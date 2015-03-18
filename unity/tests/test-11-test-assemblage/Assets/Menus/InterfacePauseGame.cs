using UnityEngine;
using System.Collections;

public class InterfacePauseGame : MonoBehaviour {

	public GameObject loadingScreen;
	public GameObject pauseMenu;

	public void Reload() {
		loadingScreen.SetActive(true);
		Application.LoadLevel(Application.loadedLevel);
	}

	public void UnPause() {
		pauseMenu.SetActive(false);
	}

	public void Pause() {
		pauseMenu.SetActive(true);
	}
}
