using UnityEngine;
using System.Collections;

public class TutoMagicien : StepTuto, PlayerEventReceiver, PlayerActionReceiver {

	public LevelScripted level;
	public LevelScripted itemLevel;
	public LevelScripted animLevel;

	public AnimationTriggerer animTriggerer;
	public SwitcherObjectListener switcher;

	public SuccessLoopCounter successLoopCounter;
	public AudioSource successStep;
	public AudioSource successLoop;

	private int lastAction;

	public new void Start() {
		if (isStart) return;
		base.Start ();
		lastAction = 0;
		
		successLoopCounter.Reset (3);
	}

	public override void play () {
		successLoopCounter.Reset (3);
		successLoopCounter.Show ();

		animTriggerer.level = animLevel;
		animTriggerer.level.connect(animTriggerer);
		switcher.level = itemLevel;
		switcher.level.connect(switcher);

		// Pour que le playerAction connaise le niveau à vérifier
		GetPlayerActions().level = level;
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
		lastAction = type;
	}

	public void OnFailure() {
		lastAction = 0;
	}

	public void OnSuccess() {
		if(lastAction == 1) {
			successLoopCounter.AddSuccess();
			successLoop.Play ();
			if (successLoopCounter.AllSuccess()) {
				this.StopStep();
			}
		}
	}

	public void OnSuccessLoop() {

	}

	private void StopStep() {
		GetBeatCounter().getMusic ().PauseMusic();
		endStep();
		successStep.Play();
		successLoopCounter.Hide();
		
		successLoopCounter.Reset (3);
		lastAction = 0;

		animTriggerer.hideObject();

		animTriggerer.level.Disconnect(animTriggerer);
		switcher.level.Disconnect(switcher);
		GetPlayerActions().level.Disconnect (GetPlayerActions());
		GetPlayerEventListener().Disconnect(this);
		GetPlayerActions().Disconnect (this);
	}


}
