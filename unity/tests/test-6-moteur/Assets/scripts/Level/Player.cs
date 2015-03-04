using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour ,LevelScriptedReceiver, PlayerEventReceiver
{

	public LevelScriptedNotifier levelScriptNotifier;
	public PlayerEventListener playerEventListener;

	void Start ()
	{
		playerEventListener.connect (this);
		levelScriptNotifier.connect (this);
	}

	public void onEventType (int type)
	{

	}

	public void onFailure ()
	{
		///Debug.Log ("Fail!");
		//this.GetComponent<Animator> ().SetTrigger ("bad");
	}


	public void onFinger (int type)
	{
		Debug.Log ("Score = "+this.levelScriptNotifier.bpm.getRelativeScore());
		if (this.levelScriptNotifier.isGood (type)) {
			//Debug.Log ("Good " + cpt++);
			this.GetComponent<Animator> ().SetTrigger ("good");
		} else {
			//Debug.Log ("Bad " + cpt++);
			this.GetComponent<Animator> ().SetTrigger ("bad");
		}
	}
}
