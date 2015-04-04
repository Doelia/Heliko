using UnityEngine;
using System.Collections;

public class TransitionScreen : HelikoObject {

	public new void Start() {
		base.Start ();
		this.gameObject.SetActive(false);
	}

	public void openLoadingScene() {
		this.gameObject.SetActive(true);
	}

	public void closeLoadingScene() {
		this.gameObject.SetActive(false);
	}

	public void fadeInCartoon() {

	}

	public void fadeOutCartoon() {

	}

	public void keepItAfterLoading() {
		GameObject.DontDestroyOnLoad(this);
	}

}
