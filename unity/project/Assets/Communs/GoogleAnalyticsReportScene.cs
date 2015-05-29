using UnityEngine;
using System.Collections;

public class GoogleAnalyticsReportScene : MonoBehaviour {
	
	public GoogleAnalyticsV3 googleAnalytics;
	public string nomScene;

	void Start () {
		googleAnalytics.LogScreen(nomScene);
		/*googleAnalytics.LogScreen(new AppViewHitBuilder()
		                          .SetScreenName(nomScene));*/
	}
	
}
