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
			if(numEvent==2)
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
			if(numEvent==1)
			{
				etape++;
			}
			else
			{
				etape=0;
			}
		}
		else if(etape==4)
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
		else if(etape==5)
		{
			if(numEvent==2)
			{
				Application.LoadLevel(8);
			}
		}

	}
	
	void Update () {
	#if UNITY_EDITOR
			if (Input.GetKeyDown (KeyCode.O))
			sendEvent(1);
		if (Input.GetKeyDown (KeyCode.P))
			sendEvent(2);
			#endif
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
              
			} 		
		}
	}
	
}
