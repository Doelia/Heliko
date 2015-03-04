using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour ,LevelScriptedReceiver, PlayerEventReceiver  {

	public LevelScriptedNotifier levelScriptNotifier;
	public PlayerEventListener playerEventListener;

	void Start() {
		playerEventListener.connect(this);
		levelScriptNotifier.connect (this);
	}

	public void onEventType (int type) {
	}

	int cpt = 0;

	public void onFinger(int type) {
		if (this.levelScriptNotifier.isGood (type)) {
			Debug.Log ("Good "+cpt++);
			this.GetComponent<Animator>().SetTrigger("good");
		} else {
			Debug.Log ("Bad "+cpt++);
			this.GetComponent<Animator>().SetTrigger("bad");
		}
	}


}
