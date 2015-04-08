using UnityEngine;
using System.Collections;

public class GoogleAnalyticsReportScene : MonoBehaviour {
	
	public GoogleAnalyticsV3 googleAnalytics;
	public string nomScene;
	// Use this for initialization
	void Start () {
	googleAnalytics.LogScreen(nomScene);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
