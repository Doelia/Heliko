using UnityEngine;
using System.Collections;

public class ChampiFan : MonoBehaviour,LevelScriptedReceiver  {

	private Animator anim;
	public LevelScripted level;
	
	public void Start () {
		this.level.connect(this);
		anim = this.GetComponent<Animator>();
	}
	
	public void onAction (int type) {
		if (type == 1) {
			anim.SetTrigger ("Move");
		}
	}
	
	public void onFailure() {
		
	}

}
