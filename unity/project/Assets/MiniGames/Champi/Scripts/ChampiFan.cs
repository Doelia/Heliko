using UnityEngine;
using System.Collections;

public class ChampiFan : Feedback, LevelScriptedReceiver  {

	private Animator anim;
	public LevelScripted level;

	public Sprite content;
	public Sprite pasContent;
	
	public new void Start () {
		base.Start ();
		this.level.connect(this);
		anim = this.GetComponent<Animator>();
	}
	
	public void onAction (int type) {
		if (type == 1) {
			anim.SetTrigger ("Move");
		}
	}
	
	public override void setReaction(bool isGood) {
		this.GetComponent<SpriteRenderer>().sprite = isGood?content:pasContent;
	}

}
