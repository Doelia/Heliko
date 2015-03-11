using UnityEngine;
using System.Collections;

public class Beat : MonoBehaviour, LevelScriptedReceiver {

	public LevelScripted level;

	public AudioSource snare;

	public void Start () {
		this.level.connect(this);
	}
	
	void Update () {
		
	}

	public void onEventType (int type) {
		if (type == 1) {
			this.GetComponent<Animator> ().SetTrigger ("Move");
			snare.Play();
		}
	}

	public void onFailure() {
		
	}

}
