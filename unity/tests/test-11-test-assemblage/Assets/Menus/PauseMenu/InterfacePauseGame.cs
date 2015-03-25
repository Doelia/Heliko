using UnityEngine;
using System.Collections;

public class InterfacePauseGame : MonoBehaviour {

	public GameObject loadingScreen;
	public GameObject pauseMenu;

	public GameObject pauseIcon;
	public GameObject playIcon;

	public AudioSource music;

	public void Reload() {
		loadingScreen.SetActive(true);
		Application.LoadLevel(Application.loadedLevel);
	}

	public void UnPause() {
		pauseMenu.SetActive(false);
		music.Play();
		playIcon.SetActive(false);
		pauseIcon.SetActive(true);
	}

	public void Pause() {
		music.Pause ();
		pauseMenu.SetActive(true);
		playIcon.SetActive(true);
		pauseIcon.SetActive(false);
	}
}
