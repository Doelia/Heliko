using UnityEngine;
using System.Collections;

public class InterfacePauseGame : HelikoObject {

	public GameObject pauseMenu;

	public GameObject pauseIcon;
	public GameObject playIcon;

	public AudioSource music;
	public AudioSource openMenuSound;
	public AudioSource closeMenuSound;
	
	public bool pause;
	public new void Start() {
		base.Start();
		music = GetBeatCounter().getMusic().GetComponent<AudioSource>();
	}

	public void UnPause() {
		pause=false;
		closeMenuSound.Play();
		pauseMenu.SetActive(false);
		music.Play();
		playIcon.SetActive(false);
		pauseIcon.SetActive(true);
	}
	
	public void Pause() {
		pause=true;
		openMenuSound.Play();
		music.Pause ();
		pauseMenu.SetActive(true);
		playIcon.SetActive(true);
		pauseIcon.SetActive(false);
	}
	
	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if(!pause)
			{
				Pause();
			}
			else
			{
				UnPause();
			}
		}
	}
}
