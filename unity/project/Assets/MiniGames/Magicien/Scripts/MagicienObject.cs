using UnityEngine;
using System.Collections;

public class MagicienObject :  Feedback, LevelScriptedReceiver {

	public LevelScripted level;
	
	public Sprite content;
	public Sprite pasContent;

	private Animator anim;
	
	public new void Start () {
		base.Start ();
		this.level.connect(this);
		anim = this.GetComponent<Animator>();
	}

	public void OnAction (int type) {
		if (type == 1) {
			anim.SetTrigger ("Move");
		}
	}
	
	public override void SetReaction(bool isGood) {
		this.GetComponent<SpriteRenderer>().sprite = isGood?content:pasContent;
	}

}
