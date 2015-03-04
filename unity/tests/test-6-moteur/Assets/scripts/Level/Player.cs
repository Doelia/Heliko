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
			Debug.Log ("Good ");
			this.GetComponent<Animator> ().SetBool("good", true);
		} else {
			Debug.Log ("Bad ");
			this.GetComponent<Animator> ().SetBool("good", false);
		}
	}
}
