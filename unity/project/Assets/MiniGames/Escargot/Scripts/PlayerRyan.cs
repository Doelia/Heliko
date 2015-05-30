using UnityEngine;
using System.Collections;

public class PlayerRyan : HelikoObject, PlayerEventReceiver {
	
	public AudioSource soundGood;
	public AudioSource soundBad;
	
	public Transform ryan;

	public MoveScript moveScript;

	public float amplitude;
	
	private PlayerActions playerActions;
	private bool leftPlayed;
	
	public new void Start () {
		base.Start();
		playerActions = GetPlayerActions();
		GetPlayerEventListener().connect (this);
	}

	private void moveRyan() {
		foreach(Transform a in ryan) {
			foreach(Transform t in a) {
				t.GetComponent<Animator>().SetTrigger("Move");
			}
		}
	}
	
	public void OnFinger (int type) {
		if (type == 1 || type == 2) {
			bool isGood = playerActions.IsGood(1);
			(isGood?soundGood:soundBad).GetComponent<AudioSource>().Play();
			moveRyan();
			moveScript.avancer(amplitude);
		}
	}
	
}
