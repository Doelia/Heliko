using UnityEngine;
using System.Collections;

/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */


public class LevelScriptedNotifier : TempoReceiver {

	//

	public void notifChilds(int type) {
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<LevelScriptedReceiver>() != null) {
				s1.GetComponent<LevelScriptedReceiver>().onEventType(type);
			} else {
				Debug.LogError(s1.name+" : GetComponent LevelScriptedReceiver null");
			}
				
		}
	}
	                                
	public override void onStep() {
		this.notifChilds (1); // TEST
	}

	public override void onHalfStep() {

	}
}
