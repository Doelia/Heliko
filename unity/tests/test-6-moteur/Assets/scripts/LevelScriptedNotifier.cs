using UnityEngine;
using System.Collections;

/*
 * Contient un script de niveau, reçoit les évenements rythmique du BPMControlor
 * 
 */

public class LevelScriptedNotifier : TempoReceiver {

	public TextAsset levelData;
	private int[] stepEvents;
	private int[] halfStepsEvents;
	private int eventIndex;

	public void loadData() {
		string [] tracks = levelData.text.Split ('\n');
		stepEvents = stringToIntEvents (tracks [0]);
		halfStepsEvents = stringToIntEvents (tracks [1]);
	}

	private int [] stringToIntEvents(string s) {
		string [] events = s.Split (' ');
		int [] toReturn = new int[events.Length];
		for (int i = 0; i < events.Length; i++) {
			toReturn [i] = int.Parse (events [i]);
		}
		return toReturn;
	}

	private void notifyChildren(int type) {
		foreach (Transform s1 in transform) {
			if (s1.GetComponent<LevelScriptedReceiver>() != null) {
				s1.GetComponent<LevelScriptedReceiver>().onEventType(type);
			} else {
				Debug.LogError(s1.name+" : GetComponent LevelScriptedReceiver null");
			}
				
		}
	}
	                                
	public override void onStep() {
		notifyChildren (stepEvents [eventIndex++]);
	}

	public override void onHalfStep() {
		notifyChildren (stepEvents [eventIndex]);
	}
}
