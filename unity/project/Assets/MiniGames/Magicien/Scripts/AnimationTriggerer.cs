using UnityEngine;
using System.Collections;

public class AnimationTriggerer : HelikoObject, LevelScriptedReceiver {

	public Transform objet;
	private Animator animObjet;
	public LevelScripted level;
	
	public void OnAction (int type) {
		if (type > 0 && type < 4) {
			hideObject();
		}
	}
	
	public new void Start () {
		base.Start();
		if (level != null)
			this.level.connect(this);
		animObjet = objet.GetComponent<Animator>();
	}

	public void hideObject() {
		animObjet.SetTrigger("fade");
	}
}
