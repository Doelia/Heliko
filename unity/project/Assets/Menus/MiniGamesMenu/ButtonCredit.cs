using UnityEngine;
using System.Collections;

public class ButtonCredit : MonoBehaviour {

			public GameObject credits; 
			public GoogleAnalyticsV3 googleAnalytics;

		
		public void click() {
					googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("Button")
		.SetEventLabel("credit")
		.SetEventAction("click"));
		credits.SetActive(true);
		this.transform.parent.parent.gameObject.SetActive(false);
	}
}
