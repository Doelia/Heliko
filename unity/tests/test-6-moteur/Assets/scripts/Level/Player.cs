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
		/*if (this.levelScripted.isGood (type)) {
			Debug.Log ("Good!");
		} else {
			Debug.Log ("Bad");
		}*/
	}


}
