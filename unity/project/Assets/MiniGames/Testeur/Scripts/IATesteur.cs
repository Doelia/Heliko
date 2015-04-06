using UnityEngine;
using System.Collections;

public class IATesteur : MonoBehaviour, LevelScriptedReceiver {

	private Animator anim;
	public LevelScripted level;
	public AudioSource clap;

	public void Start () {
		this.level.connect(this);
		anim = GetComponent < Animator >();
	}
	
	public void OnAction (int type) {
		if (type == 1) {
			anim.SetTrigger ("change");
			clap.Play();
		}
	}


}
