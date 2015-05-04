using UnityEngine;
using System.Collections;

public class SwitcherObjectListener : HelikoObject, LevelScriptedReceiver {

	public MagicObject magicObject;
	public LevelScripted level;

	public void OnAction (int type) {
		if (type > 0 && type < 4) {
			magicObject.ChangeObject(type);
		}
	}

	public new void Start () {
		base.Start();
		if (level != null)
			this.level.connect(this);
	}



}
