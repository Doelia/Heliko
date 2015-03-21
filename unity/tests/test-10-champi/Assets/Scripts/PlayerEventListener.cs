using UnityEngine;
using System.Collections;

public class PlayerEventListener : MonoBehaviour
{
	public bool onKeyDown = true;

	void Start () {
	}

	public void Awake () {
		this.observers = new ArrayList ();
	}

	ArrayList observers;
	public void connect (PlayerEventReceiver r) {
		this.observers.Add(r);
	}

	void Update () {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				foreach (PlayerEventReceiver e in this.observers) {
					e.onFinger (1);
				}
			}
		}

		if ((onKeyDown && Input.GetKeyDown (KeyCode.O)) || (!onKeyDown && Input.GetKey (KeyCode.O)))
		foreach (PlayerEventReceiver e in this.observers) {
			e.onFinger (1);
		}

		if ((onKeyDown && Input.GetKeyDown (KeyCode.P)) || (!onKeyDown && Input.GetKey (KeyCode.P)))
		foreach (PlayerEventReceiver e in this.observers) {
			e.onFinger (2);
		}

		if ((onKeyDown && Input.GetKeyDown (KeyCode.I)) || (!onKeyDown && Input.GetKey (KeyCode.I)))
		foreach (PlayerEventReceiver e in this.observers) {
			e.onFinger (3);
		}
	}
}