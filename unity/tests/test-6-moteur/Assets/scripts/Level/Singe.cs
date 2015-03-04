using UnityEngine;
using System.Collections;

public class Singe : MonoBehaviour, LevelScriptedReceiver
{

	public int typeListen;
	public LevelScriptedNotifier levelScripted;
	public AudioSource audio;

	void Start ()
	{
		levelScripted.connect (this);
	}

	public void onEventType (int type)
	{
		if (type == 1) {
			audio.Play();
		}

		if (typeListen == type) {
			Debug.Log ("pouf");
			foreach (Transform s1 in transform) {
				s1.GetComponent<Animator> ().SetTrigger ("jump");
			}
		}
	} 

	public void onFailure ()
	{

	}
}
