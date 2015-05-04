using UnityEngine;
using System.Collections;

public class AnimationTriggerer : HelikoObject, LevelScriptedReceiver {

	public Transform objet;
	private Animator animObjet;
	public LevelScripted level;
	
	public void OnAction (int type) {
		if (type > 0 && type < 4) {
			animObjet.SetTrigger("fade");
		} else {
			animObjet.ResetTrigger("reset");
		}
	}
	
	public new void Start () {
		base.Start();
		if (level != null)
			this.level.connect(this);
		animObjet = objet.GetComponent<Animator>();
	}
}
