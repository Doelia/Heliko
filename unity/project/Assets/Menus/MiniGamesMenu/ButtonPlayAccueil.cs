using UnityEngine;
using System.Collections;

public class ButtonPlayAccueil : MonoBehaviour {

		public GameObject miniGamesMenu; 
		public GoogleAnalyticsV3 googleAnalytics;
		public AudioSource playSound;


		
		public void click() {
				playSound.Play();

					googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("Button")
		.SetEventLabel("play")
		.SetEventAction("click"));
		miniGamesMenu.SetActive(true);
				miniGamesMenu.GetComponent<AnimationGUI>().animIt();

		this.transform.parent.parent.gameObject.SetActive(false);
	}
	
		void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit();
		}
		
	}
}
