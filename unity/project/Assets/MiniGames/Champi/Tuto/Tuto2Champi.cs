using UnityEngine;
using System.Collections;

public class Tuto2Champi : StepTuto, PlayerEventReceiver, PlayerActionReceiver {

	public LevelScripted levelPlayer;
	public LevelScripted levelIA;
	public Transform skipTuto2;

	public SuccessLoopCounter successLoopCounter;
	public AudioSource successStep;
	public AudioSource successLoop;

	public Canvas buttons;

	public new void Start() {
		if (isStart) return;
		base.Start ();
	}

	public override void play () {
		successLoopCounter.Reset (3);
		successLoopCounter.Show ();

		showButtons();

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
		GetPlayerActions().IsGood(type);
	}

	public void OnFailure() {

	}

	public void OnSuccess() {

	}

	public void showButtons() {
		GameObject.Find ("Player").GetComponent<PlayerChampi>().disableOrange = false;
		buttons.gameObject.SetActive(true);
		GameObject.Find ("SkipTuto1").gameObject.SetActive(false);
		skipTuto2.gameObject.SetActive(true);
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
	}


}
