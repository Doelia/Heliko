using UnityEngine;
using System.Collections;

public class accesCheatZone : MonoBehaviour {

	private int etape;
		private bool touchScreen;
	private Vector2 mouvement;
	private float timeTouchTotal;
		public float timeBeforeLongTouch;

	
	void Start()
	{
		etape=0;
	}
	
	
	// Update is called once per frame
	void sendEvent(int numEvent)
	{
		if(etape==0)
		{
			if(numEvent==1)
			{
				etape++;
			}
		}
		else if(etape==1)
		{
			if(numEvent==2)
			{
				etape++;
			}
			else
			{
				etape=0;
			}
		}
		else if(etape==2)
		{
			if(numEvent==4)
			{
				etape++;
			}
			else
			{
				etape=0;
			}
		}
		else if(etape==3)
		{
			if(numEvent==3)
			{
				Application.LoadLevel("cheatZone");
			}
			else
			{
				etape=0;
			}
		}
	}
	
	void Update () {
	if (Input.touchCount > 0) {
		

			switch (Input.GetTouch(0).phase) {
				case TouchPhase.Began:
				Vector2 posDoigt = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

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
					else 
					{
						//sendEvent(5);
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
	}
}
