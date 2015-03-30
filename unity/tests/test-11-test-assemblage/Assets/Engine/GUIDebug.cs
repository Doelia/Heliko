using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIDebug : MonoBehaviour {
	public PlayerActions p;
	public Text failures;
	public Text percentage;
	public Slider avancement;
	public Slider slider;
	public BeatCounter bc;

	// Use this for initialization
	void Start () {
		
	}

	public void setMusicPercentage() {
		bc.getMusic().setMusicPercentage(slider.value / 100.0f);	
	}
	
	// Update is called once per frame
	void Update () {
		failures.text = "Échecs : " + p.getFailureCount();
		percentage.text = "Pourcentage : " + p.getSuccessPercencage();
		avancement.value = bc.getMusic().getMusicPercentage();
	}
}
