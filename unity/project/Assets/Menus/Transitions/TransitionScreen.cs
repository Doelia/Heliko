using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionScreen : HelikoObject {

	public Transform loadingImage;

	public new void Start() {
		if (isStart) return;
		base.Start ();
	}

	public bool LoadingScreenIsOpen() {
		return this.GetComponent<Canvas>().enabled;
	}

	public IEnumerator openLoadingScene() {
		this.GetComponent<Canvas>().enabled = true;
		Image img = loadingImage.GetComponent<Image>();
		for (float f = 0f; f <= 1.1f; f += 0.1f) {
			Color c = img.color;
			c.a = f;
			img.color = c;
			yield return null;
		}
	}

	public IEnumerator closeLoadingScene() {
		this.GetComponent<Canvas>().enabled = true;
		Image img = loadingImage.GetComponent<Image>();
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = img.color;
			c.a = f;
			img.color = c;
			yield return null;
		}
		this.GetComponent<Canvas>().enabled = false;
	}

	public void fadeInCartoon() {

	}

	public void fadeOutCartoon() {

	}

	public void keepItAfterLoading() {
		GameObject.DontDestroyOnLoad(this);
	}

}
