using UnityEngine;
using System.Collections;

public class SingeJaune : LevelScriptedReceiver {

	public override void onEventType (int type) {
		if (type == 1) {
			foreach (Transform s1 in transform) {
				s1.GetComponent<Animator>().SetTrigger("jump");
			}
		}
	}


}
