using UnityEngine;
using System.Collections;

public class IATesteur : MonoBehaviour, LevelScriptedReceiver {

	private Animator anim;
	public LevelScripted level;

	public void Start () {
		this.level.connect(this);
		anim = GetComponent < Animator >();
	}
	
	public void onAction (int type) {
		if (type == 1) {
			Debug.Log ("change");
			anim.SetTrigger ("change");
		}
	}


}
