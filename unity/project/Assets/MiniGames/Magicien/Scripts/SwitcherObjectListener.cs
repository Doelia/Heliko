using UnityEngine;
using System.Collections;

public class SwitcherObjectListener : HelikoObject, LevelScriptedReceiver {

	public MagicObject magicObject;
	public LevelScripted level;

	public CompteurMagicien compteur = null;

	public void OnAction (int type) {
		if (type > 0 && type < 4) {
			magicObject.ChangeObject(type);
			if (compteur != null) {
				compteur.startIncrement();
			} else {
				Debug.LogWarning("Compteur not found");
			}
		}
	}

	public new void Start () {
		base.Start();
		if (level != null)
			this.level.connect(this);
	}



}
