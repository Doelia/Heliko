using UnityEngine;
using System.Collections;

public class MagicienObject :  Feedback, LevelScriptedReceiver {

	public LevelScripted level;
	
	public Sprite content;
	public Sprite pasContent;
	
	public new void Start () {
		base.Start ();
		this.level.connect(this);
	}

	public void OnAction (int type) {

	}
	
	public override void SetReaction(bool isGood) {
		this.GetComponent<SpriteRenderer>().sprite = isGood?content:pasContent;
	}

}
