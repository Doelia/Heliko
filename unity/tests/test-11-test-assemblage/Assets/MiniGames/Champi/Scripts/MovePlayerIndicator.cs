using UnityEngine;
using System.Collections;

public class MovePlayerIndicator : MonoBehaviour, LevelScriptedReceiver {
	
	private Animator anim;
	public LevelScripted level;
	
	public Transform champiTransform;
	
	public void Start () {
		this.level.connect(this);
		anim = champiTransform.GetComponent<Animator>();
	}
	
	public void onAction (int type) {
		anim.SetTrigger ("Move");
	}
	
	public void onFailure() {
		
	}
	
}
