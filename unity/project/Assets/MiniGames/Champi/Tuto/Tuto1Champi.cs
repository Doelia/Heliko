using UnityEngine;
using System.Collections;

public class Tuto1Champi : StepTuto, PlayerEventReceiver {

	public LevelScripted levelPlayer;
	public LevelScripted levelIA;

	public new void Start() {
		if (isStart) return;
		base.Start ();
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


}
