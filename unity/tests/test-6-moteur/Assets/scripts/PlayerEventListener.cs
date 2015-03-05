using UnityEngine;
using System.Collections;

public class PlayerEventListener : MonoBehaviour
{
	public bool onKeyDown = true;

	void Start ()
	{
	}

	public void Awake ()
	{
		this.observers = new ArrayList ();
	}

	ArrayList observers;

	public void connect (PlayerEventReceiver r)
	{
		this.observers.Add (r);
	}

	void Update ()
	{
	

		if ((onKeyDown && Input.GetKeyDown (KeyCode.O)) || (!onKeyDown && Input.GetKey (KeyCode.O)))
			foreach (PlayerEventReceiver e in this.observers) {
				e.onFinger (1);
			}

		if ((onKeyDown && Input.GetKeyDown (KeyCode.P)) || (!onKeyDown && Input.GetKey (KeyCode.P)))
			foreach (PlayerEventReceiver e in this.observers) {
				e.onFinger (2);
			}
	}
}
