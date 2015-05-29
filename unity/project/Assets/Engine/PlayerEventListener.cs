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
	private Rect rectanglePause;
	#endif
	

	public new void Start()
	{
		base.Start();
		bc = GetBeatCounter();



		#if UNITY_ANDROID || UNITY_IOS
		RectTransform pauseIcon=null;

		if (GameObject.Find ("PauseIcon") == null) {
			//Debug.LogError("Impossible de trouver l'objet PauseIcon dans la scène");
		}
		else {
			pauseIcon=GameObject.Find ("PauseIcon").GetComponent<RectTransform>();
			Vector3[] coinsPauseIcons;
			coinsPauseIcons=new Vector3[4];
			pauseIcon.GetWorldCorners(coinsPauseIcons);
			rectanglePause= new Rect();
			rectanglePause.xMin=coinsPauseIcons[0].x;
			rectanglePause.xMax=coinsPauseIcons[2].x;
			rectanglePause.yMin=coinsPauseIcons[0].y;
			rectanglePause.yMax=coinsPauseIcons[1].y;
		}

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

	public void Disconnect (PlayerEventReceiver r) {
		this.observers.Remove(r);
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
				Vector2 posDoigt = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

				//si c'est sur le bouton Pause
				if (rectanglePause.Contains(Input.GetTouch(0).position)) {
					return;
				}
			
				touchScreen = true;
				if (posDoigt.x > 0) {
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
					else  {
						sendEvent(5);
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
		

		if (Input.GetKeyDown (KeyCode.O))
			sendEvent(1);
		if (Input.GetKeyDown (KeyCode.P))
			sendEvent(2);
		if (Input.GetKeyUp (KeyCode.O) || Input.GetKeyUp (KeyCode.P)) {
			sendEvent(3);
		}
		
	}


}
