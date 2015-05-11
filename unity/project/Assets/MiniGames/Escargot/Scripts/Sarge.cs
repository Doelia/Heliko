using UnityEngine;
using System.Collections;

public class Sarge : Feedback, LevelScriptedReceiver, PlayerActionReceiver {
	
	public Transform sarge;

	public LevelScripted level; // utilisé pour se connecter uniquement
	
	public Sprite boucheWin;
	public Sprite boucheFail;
	public Sprite corpsWin;
	public Sprite corpsFail;
	public Transform bouche;
	public Transform corps;
	
	public new void Start () {
		base.Start();
		if (level != null) {
			this.level.connect (this);
		}
	}

	private void moveSarge() {
		foreach(Transform a in sarge) {
			foreach(Transform t in a) {
				t.GetComponent<Animator>().SetTrigger("Move");
			}
		}
	}
	
	public void OnAction (int type) {
		if (type == 1) {
			moveSarge();
		}
	}
	
	public override void SetReaction(bool isGood) {
		bouche.GetComponent<SpriteRenderer>().sprite = isGood?boucheWin:boucheFail;
		corps.GetComponent<SpriteRenderer>().sprite = isGood?corpsWin:corpsFail;
	}
	
}
