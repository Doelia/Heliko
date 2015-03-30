using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIDebug : HelikoObject {

	private PlayerActions playerActions;
	private BeatCounter bc;

	public Text failures;
	public Text percentage;
	public Slider avancement;
	public Slider slider;

	void Start () {
		base.Start();
		playerActions = getPlayerActions();
		bc = getBeatCounter();

		gameObject.SetActive(constantes.showDebugGUI);
	}

	public void setMusicPercentage() {
		bc.getMusic().setMusicPercentage(slider.value / 100.0f);	
	}
	
	// Update is called once per frame
	void Update () {
		failures.text = "Echecs : " + playerActions.getFailureCount();
		percentage.text = "Pourcentage : " + playerActions.getSuccessPercencage();
		avancement.value = bc.getMusic().getMusicPercentage();
	}
}
