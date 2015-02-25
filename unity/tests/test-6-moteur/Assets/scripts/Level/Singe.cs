using UnityEngine;
using System.Collections;

public class Singe : LevelScriptedReceiver {

	public int typeListen;

	public override void onEventType (int type) {
		if (typeListen == 1) {
			foreach (Transform s1 in transform) {
				s1.GetComponent<Animator>().SetTrigger("jump");
			}
		}
	}


}
