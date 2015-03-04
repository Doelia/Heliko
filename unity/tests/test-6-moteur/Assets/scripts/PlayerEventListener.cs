using UnityEngine;
using System.Collections;

public class PlayerEventListener : MonoBehaviour {

	void Start() {
	}

	public void Awake() {
		this.observers = new ArrayList ();
	}

	ArrayList observers;

	public void connect(PlayerEventReceiver r) {
		this.observers.Add (r);
	}

	void Update() {
		if (Input.GetKey(KeyCode.O))
		foreach (PlayerEventReceiver e in this.observers) {
			e.onFinger (2);
		}

		if (Input.GetKey(KeyCode.P))
		foreach (PlayerEventReceiver e in this.observers) {
			e.onFinger (1);
		}
	}

}
