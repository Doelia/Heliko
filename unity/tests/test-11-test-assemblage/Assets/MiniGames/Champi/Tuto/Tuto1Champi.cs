using UnityEngine;
using System.Collections;

public class Tuto1Champi : StepTuto, LevelScriptedReceiver, PlayerEventReceiver {

	public LevelScripted levelMetronome;
	public LevelScripted levelPlayer;
	public LevelScripted levelIA;

	public AudioSource tic;
	public AudioSource tac;

	public void Start() {
		levelMetronome.connect(this);
	}

	public override void play () {
		levelIA.connect(GameObject.Find("IA").GetComponent<PNJ>());

		getPlayerActions().level = levelPlayer;
		getPlayerActions().level.connect (getPlayerActions());

		getPlayerEventListener().connect(this);
		getBeatCounter().StartCount();
	}

	public void onFinger(int type) {
		getPlayerActions().isGood(type);
	}

	public void onAction(int type) {
		if (type == 1) {
			tic.Play();
		} else {
			tac.Play();
		}
	}

}
