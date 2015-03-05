using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour ,LevelScriptedReceiver, PlayerEventReceiver
{

	public LevelScriptedNotifier levelScriptNotifier;
	public PlayerEventListener playerEventListener;
	public Transform cube;

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
		//Debug.Log ("Fail!");
		//this.GetComponent<Animator> ().SetTrigger ("bad");
	}

	public void changeColorCube (bool good)
	{
		if (good) {
			cube.renderer.material.color = new Color (0, 1, 0);
		} else {
			cube.renderer.material.color = new Color (1, 0, 0);
		}
	}


	public void onFinger (int type)
	{
		Debug.Log ("Score = " + this.levelScriptNotifier.bpm.getRelativeScore ());
		if (this.levelScriptNotifier.isGood (type)) {
			Debug.Log ("Good ");
			this.GetComponent<Animator> ().SetBool ("good", true);
			this.changeColorCube (true);
		} else {
			Debug.Log ("Bad ");
			this.GetComponent<Animator> ().SetBool ("good", false);
			this.changeColorCube (false);
		}
	}
}
