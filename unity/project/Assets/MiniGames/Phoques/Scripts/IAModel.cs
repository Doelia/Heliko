using UnityEngine;
using System.Collections;

public class IAModel : MonoBehaviour, LevelScriptedReceiver {

	private Animator anim;
	public LevelScripted level;

	public void Start () {
		this.level.connect(this);
		anim = GetComponent < Animator >();
	}
	
	public void OnAction (int type) {
		if (type == 1) {
			anim.SetTrigger ("change");
		}
	}

	public void onFailure() {
		
	}

}
