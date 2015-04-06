using UnityEngine;
using System.Collections;

public class MovePlayerIndicator : HelikoObject, LevelScriptedReceiver {
	
	private Animator anim;
	public LevelScripted level;
	
	public Transform champiTransform;
	
	public new void Start () {
		if (level != null)
			this.level.connect(this);
		anim = champiTransform.GetComponent<Animator>();
	}
	
	public void OnAction (int type) {
		anim.SetTrigger ("Move");
	}
	
	public void onFailure() {
		
	}
	
}
