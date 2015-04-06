using UnityEngine;
using System.Collections;

public class PlayerEventListener : HelikoObject
{
	private BeatCounter bc;

	public bool onKeyDown = true;
	public float timeBeforeLongTouch;

	private ArrayList observers;

	#if UNITY_ANDROID || UNITY_IOS
	private bool touchScreen;
	private Vector2 mouvement;
	private float timeTouchTotal;
	#endif

	public new void Start()
	{
		base.Start();
		bc = GetBeatCounter();
		
		#if UNITY_ANDROID || UNITY_IOS
		touchScreen = false;
		timeTouchTotal = 0F;
		mouvement = Vector2.zero;
		timeBeforeLongTouch=0.18f;
		#endif
	}

	public void Awake () {
		this.observers = new ArrayList ();
	}

	public void connect (PlayerEventReceiver r) {
		this.observers.Add(r);
	}

	private void sendEvent(int type) {
		foreach (PlayerEventReceiver e in this.observers) {
			e.OnFinger (type);
		}
	}

	void Update () {
		if (bc.isInPause()) {
			return;
		}
		#if UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0) {
			switch (Input.GetTouch(0).phase) {
				case TouchPhase.Began:
				touchScreen = true;
				Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
				if (pos.x > 0) {
					sendEvent(1);
				}
				else {
					sendEvent(2);
				}
				break;  

				case TouchPhase.Ended:
					if (mouvement.magnitude>=12 && touchScreen) {
						sendEvent(4);
					}
					else if(timeTouchTotal>=timeBeforeLongTouch) {
						sendEvent(3);
					}
					timeTouchTotal = 0F;
					touchScreen=false; 
					mouvement=Vector2.zero;
				break;  

				case TouchPhase.Stationary: 
					timeTouchTotal+=Time.deltaTime;
					if (timeTouchTotal>=timeBeforeLongTouch && touchScreen) {
						touchScreen=false;
					}
				break;

				case TouchPhase.Moved: 
					mouvement+=Input.GetTouch(0).deltaPosition;	
					if (mouvement.magnitude <= 4) {
						timeTouchTotal += Time.deltaTime;
						if (timeTouchTotal >= timeBeforeLongTouch && touchScreen) {
							touchScreen=false;
						}
					}
				break;                
			}  		
		}
		#endif 
		

		if ((onKeyDown && Input.GetKeyDown (KeyCode.O)) || (!onKeyDown && Input.GetKey (KeyCode.O)))
			sendEvent(1);

		if ((onKeyDown && Input.GetKeyDown (KeyCode.P)) || (!onKeyDown && Input.GetKey (KeyCode.P)))
			sendEvent(2);
		
	}


}
