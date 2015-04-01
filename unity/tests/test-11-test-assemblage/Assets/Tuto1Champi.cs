using UnityEngine;
using System.Collections;

public class Tuto1Champi : StepTuto, LevelScriptedReceiver {

	public LevelScripted levelMetronome;
	public AudioSource tic;
	public AudioSource tac;

	public void Start() {
		levelMetronome.connect(this);
	}

	public override void play () {
		GameObject.Find("IAStep1").GetComponent<LevelScripted>().connect(GameObject.Find("IA").GetComponent<PNJ>());
		getPlayerActions().level = GameObject.Find("PlayerStep1").GetComponent<LevelScripted>();
		getPlayerActions().level.connect (getPlayerActions());
		getBeatCounter().StartCount();
	}

	public void onAction(int type) {
		if (type == 1) {
			tic.Play();
		} else {
			tac.Play();
		}
	}

}
