using UnityEngine;
using System.Collections;

public class Tuto1Champi : StepTuto, LevelScriptedReceiver, PlayerEventReceiver {

	public LevelScripted levelMetronome;
	public LevelScripted levelPlayer;
	public LevelScripted levelIA;

	public AudioSource tic;
	public AudioSource tac;

	public new void Start() {
		levelMetronome.connect(this);
	}

	public override void play () {
		levelIA.connect(GameObject.Find("IA").GetComponent<PNJ>());

		GetPlayerActions().level = levelPlayer;
		GetPlayerActions().level.connect (GetPlayerActions());

		GetPlayerEventListener().connect(this);
		GetBeatCounter().StartCount();
	}

	public void OnFinger(int type) {
		GetPlayerActions().IsGood(type);
	}

	public void OnAction(int type) {
		if (type == 1) {
			tic.Play();
		} else {
			tac.Play();
		}
	}

}
