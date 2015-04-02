using UnityEngine;
using System.Collections;

public class InterfacePauseGame : HelikoObject {

	public GameObject loadingScreen;
	public GameObject pauseMenu;

	public GameObject pauseIcon;
	public GameObject playIcon;

	public AudioSource music;
	public AudioSource openMenuSound;
	public AudioSource closeMenuSound;

	public new void Start() {
		base.Start();
		//music = getBeatCounter().getMusic().GetComponent<AudioSource>();
	}

	public void UnPause() {
		closeMenuSound.Play();
		pauseMenu.SetActive(false);
		music.Play();
		playIcon.SetActive(false);
		pauseIcon.SetActive(true);
	}

	public void Pause() {
		openMenuSound.Play();
		music.Pause ();
		pauseMenu.SetActive(true);
		playIcon.SetActive(true);
		pauseIcon.SetActive(false);
	}
}
