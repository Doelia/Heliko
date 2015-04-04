using UnityEngine;
using System.Collections;

public class TransitionScreen : HelikoObject {

	public Transform loadingImage;

	public new void Start() {
		if (isStart) return;
		base.Start ();
	}

	public bool LoadingScreenIsOpen() {
		return this.GetComponent<Canvas>().enabled;
	}

	public void openLoadingScene() {
		Debug.Log ("openLoadingScene");
		this.GetComponent<Canvas>().enabled = true;
		loadingImage.GetComponent<Animator>().SetTrigger("show");
	}

	public void closeLoadingScene() {
		Debug.Log ("closeLoadingScene");
		loadingImage.GetComponent<Animator>().SetTrigger("hide");
		//this.GetComponent<Canvas>().enabled = false;
	}

	public void fadeInCartoon() {

	}

	public void fadeOutCartoon() {

	}

	public void keepItAfterLoading() {
		GameObject.DontDestroyOnLoad(this);
	}

}
