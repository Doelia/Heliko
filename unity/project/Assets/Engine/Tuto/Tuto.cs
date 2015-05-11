using UnityEngine;
using System.Collections;

public class Tuto : HelikoObject {

	public StepTuto[] steps;
	int nStep = 0;
	public int idGame;
	public GoogleAnalyticsV3 googleAnalytics;
	public Canvas skipTutoCanvas;

	public new void Start() {
		base.Start ();
		initTuto();
		startTuto();
	}

	public void initTuto() {
		foreach (StepTuto t in steps) {
			t.init(this);
		}
	}

	public void startTuto() {
		next();
	}

	public void next() {
		if (nStep >= steps.Length) {
			startLevel();
		}
		steps[nStep].play();
		nStep++;
	}

	private void startLevel() {
		googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("Tuto")
		.SetEventAction("End")
		.SetEventLabel("End the tuto")
		.SetEventValue(nStep));
		this.GetComponent<LoadOnClick>().LoadScene(constantes.getNumSceneFromIdMiniGame(idGame));
	}

	public void skipTuto() {
		googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("Tuto")
		.SetEventAction("Skip")
		.SetEventLabel("skip the tuto")
		.SetEventValue(nStep));
		this.startLevel();
	}

	public void openSkipPopup() {
		GetBeatCounter().getMusic().PauseMusic();
		skipTutoCanvas.gameObject.SetActive(true);
		GameObject go = GameObject.Find("OpenPopup");
		if (go != null) {
			go.GetComponent<AudioSource>().Play();
		}
	}

	public void closeSkipPopup() {
		GetBeatCounter().getMusic().UnPauseMusic();
		skipTutoCanvas.gameObject.SetActive(false);
	}

}
