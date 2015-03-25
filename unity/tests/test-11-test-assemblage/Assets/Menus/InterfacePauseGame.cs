using UnityEngine;
using System.Collections;

public class InterfacePauseGame : MonoBehaviour {

	public GameObject loadingScreen;
	public GameObject pauseMenu;

	public AudioSource music;

	public void Reload() {
		loadingScreen.SetActive(true);
		Application.LoadLevel(Application.loadedLevel);
	}

	public void UnPause() {
		pauseMenu.SetActive(false);
		music.Play();
	}

	public void Pause() {
		music.Pause ();
		pauseMenu.SetActive(true);
	}
}
