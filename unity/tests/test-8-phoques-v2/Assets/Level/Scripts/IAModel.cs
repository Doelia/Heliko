﻿using UnityEngine;
using System.Collections;

public class IAModel : MonoBehaviour, LevelScriptedReceiver {

	private Animator anim;
	public AudioSource audio;
	public LevelScripted level;

	public void Start () {
		this.level.connect(this);
		anim = GetComponent < Animator >();
	}
	
	void Update () {
		
	}

	public void onEventType (int type) {
		Debug.Log("Event type "+type);
		if (type == 1) {
			anim.SetTrigger ("change");
		}
	}

	public void onFailure() {
		
	}

}