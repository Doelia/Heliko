using UnityEngine;
using System.Collections;

public class ButtonPlayAccueil : MonoBehaviour {

		public GameObject miniGamesMenu; 
			public GoogleAnalyticsV3 googleAnalytics;

		
		public void click() {
					googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("Button")
		.SetEventLabel("play")
		.SetEventAction("click"));
		miniGamesMenu.SetActive(true);
		this.transform.parent.parent.gameObject.SetActive(false);
	}
}
