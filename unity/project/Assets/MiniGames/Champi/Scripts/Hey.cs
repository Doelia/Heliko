using UnityEngine;
using System.Collections;

public class Hey : MonoBehaviour, LevelScriptedReceiver {

	public LevelScripted level;

	public Sprite signal;
	
	public void Start () {
		this.level.connect(this);
	}
	
	public void OnAction (int type) {
		if (type == 1) {
			this.GetComponent<SpriteRenderer>().sprite = signal;
		}
	}
	
	public void onFailure() {
		
	}
	
}
