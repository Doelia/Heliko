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
		this.GetComponent<Animator> ().SetTrigger ("bad");
	}


	int cpt = 0;

	public void onFinger (int type)
	{
		Debug.Log("ScoreRelatif = "+this.levelScriptNotifier.bpm.getScoreRelatif()+", Score="+this.levelScriptNotifier.bpm.getScore());
		if (this.levelScriptNotifier.isGood (type)) {
			//Debug.Log ("Good " + cpt++);
			this.GetComponent<Animator> ().SetTrigger ("good");
		} else {
			//Debug.Log ("Bad " + cpt++);
			this.GetComponent<Animator> ().SetTrigger ("bad");
		}
	}
}
