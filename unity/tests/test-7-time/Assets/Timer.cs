using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public AudioSource music;

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

	IEnumerator BeatCheck ()
	{
		while (audioSource.isPlaying) {
			currentSample = (float)AudioSettings.dspTime * audioSource.clip.frequency;
			
			if (currentSample >= (nextBeatSample + sampleOffset)) {
				foreach (GameObject obj in observers) {
					obj.GetComponent<BeatObserver>().BeatNotify(beatType);
				}
				nextBeatSample += samplePeriod;
			}

			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
