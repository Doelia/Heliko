using UnityEngine;
using System.Collections;

public class MovePlayerIndicator : HelikoObject, LevelScriptedReceiver {
	
	private Animator anim;
	public LevelScripted level;
	
	public Transform champiTransform;
	
	public void Start () {
		if (level != null)
			this.level.connect(this);
		anim = champiTransform.GetComponent<Animator>();
	}
	
	public void onAction (int type) {
		anim.SetTrigger ("Move");
	}
	
	public void onFailure() {
		
	}
	
}
