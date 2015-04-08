using UnityEngine;
using System.Collections;

public class Tuto1Champi : StepTuto, PlayerEventReceiver, PlayerActionReceiver {

	public LevelScripted levelPlayer;
	public LevelScripted levelIA;

	public SuccessLoopCounter successLoopCounter;

	public new void Start() {
		if (isStart) return;
		base.Start ();
		GetPlayerActions().Connect (this);
	}

	public override void play () {
		successLoopCounter.Reset (3);
		successLoopCounter.Show ();


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
		successLoopCounter.AddSuccess();
		if (successLoopCounter.AllSuccess()) {
			this.StopStep();
		}
	}

	private void StopStep() {
		GetBeatCounter().getMusic ().PauseMusic();
		endStep();
	}


}
