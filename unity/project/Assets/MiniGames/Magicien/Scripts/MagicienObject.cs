using UnityEngine;
using System.Collections;

public class MagicienObject :  Feedback {

	private Animator anim;
	
	public Sprite content;
	public Sprite pasContent;
	
	public new void Start () {
		base.Start ();
		anim = this.GetComponent<Animator>();
	}
	
	public override void SetReaction(bool isGood) {
		this.GetComponent<SpriteRenderer>().sprite = isGood?content:pasContent;
	}

}
