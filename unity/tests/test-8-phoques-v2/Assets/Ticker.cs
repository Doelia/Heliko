using UnityEngine;
using System.Collections;

public class Ticker : MonoBehaviour, LevelScriptedReceiver {

	public LevelScripted level;
	private AudioSource tick;

	public void Start () {
		this.level.connect(this);
		tick = GetComponent < AudioSource >();
	}
	
	public void onEventType (int type) {
		if (type == 1) {
			tick.Play();
		}
	}

	public void onFailure() {
		
	}
}
