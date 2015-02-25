using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour ,LevelScriptedReceiver,PlayerEventReceiver  {

	public LevelScriptedNotifier levelScriptNotifier;
	public PlayerEventListener playerEventListener;

	void Start() {
		playerEventListener.connect(this);
		levelScriptNotifier.connect (this);
	}

	public void onEventType (int type) {

	}

	public void onFinger(int type) {
		if (this.levelScriptNotifier.isGood (type)) {
			this.renderer.material.color = new Color(0,1,0);
		} else {
			this.renderer.material.color = new Color(1,0,0);
		}
	}


}
