using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public AudioSource music;

	double getDelayTick() {
		double timeInMusic = (double)music.timeSamples / (double)music.clip.frequency;
		return timeInMusic % this.getTimePerTicks();
	}

	float getTimePerTicks() {
		return 60f / 266f;
	}

	// Use this for initialization
	void Start() {
		print ("timePerTicks="+getTimePerTicks());
		print("Starting " + Time.time);

		Debug.Log("Before WaitAndPrint Finishes " + Time.time);

		for (int i = 0; i < 200; i++) {
			float voulu = i*getTimePerTicks();
			StartCoroutine(WaitAndPrint(voulu));
		}
	}

	IEnumerator WaitAndPrint(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		double delay = getDelayTick() * 1000;
		Debug.Log("Delay " + delay+"ms");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
