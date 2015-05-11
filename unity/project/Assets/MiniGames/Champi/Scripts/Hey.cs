using UnityEngine;
using System.Collections;

public class Hey : HelikoObject, LevelScriptedReceiver {

	public LevelScripted level;

	public AudioSource soundSignal;
	
	public new void Start () {
		//base.Start();
		this.level.connect(this);
	}
	
	public void OnAction (int type) {
		if (type == 1) {
			soundSignal.GetComponent<AudioSource>().Play();
		}
	}
	
	public void onFailure() {
		
	}
	
}
