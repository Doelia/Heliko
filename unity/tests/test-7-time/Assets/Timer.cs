using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	float getTimePerTicks() {
		return 60f / 266f;
	}

	// Use this for initialization
	void Start() {
		print ("timePerTicks="+getTimePerTicks());
		print("Starting " + Time.time);

		Debug.Log("Before WaitAndPrint Finishes " + Time.time);

		for (int i = 0; i < 5; i++) {
			float voulu = i*getTimePerTicks();
			print ("voulu = "+voulu);
			StartCoroutine(WaitAndPrint(voulu));
		}
	}

	IEnumerator WaitAndPrint(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		Debug.Log("WaitAndPrint " + Time.time);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
