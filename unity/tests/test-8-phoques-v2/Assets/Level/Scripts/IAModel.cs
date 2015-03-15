using UnityEngine;
using System.Collections;

public class IAModel : MonoBehaviour, LevelScriptedReceiver {

	private Animator anim;
	public LevelScripted level;
	public AudioSource snare;

	public void Start () {
		this.level.connect(this);
		anim = GetComponent < Animator >();
	}
	
	void Update () {
		
	}

	public void onEventType (int type) {
		Debug.Log("Event type "+type);
		if (type == 1) {
			snare.Play();
			anim.SetTrigger ("change");
		}
	}

	public void onFailure() {
		
	}

}
