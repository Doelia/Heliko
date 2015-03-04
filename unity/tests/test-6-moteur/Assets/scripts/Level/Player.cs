using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour ,LevelScriptedReceiver,PlayerEventReceiver
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
		Debug.Log ("Fail!");
	}

	public void onFinger (int type)
	{
		if (this.levelScriptNotifier.isGood (type)) {
			renderer.material.color = Color.green;
		} else {
			renderer.material.color = Color.red;
		}
	}
}
