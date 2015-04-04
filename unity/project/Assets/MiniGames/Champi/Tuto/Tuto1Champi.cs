using UnityEngine;
using System.Collections;

public class Tuto1Champi : StepTuto, PlayerEventReceiver, PlayerActionReceiver {

	public LevelScripted levelPlayer;
	public LevelScripted levelIA;

	int nbrLoopSucces = 0;

	public new void Start() {
		if (isStart) return;
		base.Start ();
		GetPlayerActions().Connect (this);
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

	public void OnFailure() {

	}

	public void OnSuccess() {

	}

	public void OnSuccessLoop() {
		nbrLoopSucces++;
		if (nbrLoopSucces >= 1) {
			this.stopStep();
		}
	}

	private void stopStep() {
		GetBeatCounter().getMusic ().PauseMusic();
		endStep();
	}


}
