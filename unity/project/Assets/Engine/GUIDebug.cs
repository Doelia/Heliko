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

	public new void Start () {
		base.Start();
		playerActions = GetPlayerActions();
		bc = GetBeatCounter();
	}

	public void SetMusicPercentage() {
		bc.getMusic().SetMusicPercentage(slider.value / 100.0f);	
	}
	
	void Update () {
		failures.text = "Echecs : " + playerActions.getFailureCount();
		percentage.text = "Pourcentage : " + playerActions.getSuccessPercencage();
		avancement.value = bc.getMusic().GetMusicPercentage();
	}
}
