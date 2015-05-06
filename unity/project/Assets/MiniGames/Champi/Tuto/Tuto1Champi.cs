using UnityEngine;
using System.Collections;

public class Tuto1Champi : StepTuto, PlayerEventReceiver, PlayerActionReceiver {

	public LevelScripted levelPlayer;
	public LevelScripted levelIA;

	public SuccessLoopCounter successLoopCounter;
	public AudioSource successStep;
	public AudioSource successLoop;

	public new void Start() {
		if (isStart) return;
		base.Start ();
	}

	public override void play () {
		Debug.Log ("Start playing Tuto1Champi");
		successLoopCounter.Reset (3);
		successLoopCounter.Show ();

		levelIA.connect(GameObject.Find("IA").GetComponent<PNJ>());

		// Pour que le playerAction connaise le niveau à vérifier
		GetPlayerActions().level = levelPlayer;
		// Pour que le playerAction reçoive les évents du niveau
		GetPlayerActions().level.connect (GetPlayerActions());
		// Pour recevoir les évents du joueur
		GetPlayerEventListener().connect(this);
		// Pour recevoir la réussite et les echec du joueurs
		GetPlayerActions().Connect (this);

		// COmmencer à lire la musique
		GetBeatCounter().StartCount();
	}

	public void OnFinger(int type) {
		GetPlayerActions().IsGood(1);
	}

	public void OnFailure() {

	}

	public void OnSuccess() {

	}

	public void OnSuccessLoop() {
		successLoopCounter.AddSuccess();
		successLoop.Play ();
		if (successLoopCounter.AllSuccess()) {
			this.StopStep();
		}
	}

	private void StopStep() {
		GetBeatCounter().getMusic ().PauseMusic();
		endStep();
		successStep.Play();
		successLoopCounter.Hide();
		GetPlayerActions().Disconnect(this);
		GetPlayerEventListener().Disconnect(this);
		GetPlayerActions().level.Disconnect(GetPlayerActions());
	}


}
