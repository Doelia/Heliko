using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionScreen : HelikoObject {

	public Transform loadingImage;
	public Transform loadingText;

	public new void Start() {
		if (isStart) return;
		base.Start ();
	}

	public bool LoadingScreenIsOpen() {
		return this.GetComponent<Canvas>().enabled;
	}

	public IEnumerator FadeInBlackScreen() {
		this.GetComponent<Canvas>().enabled = true;
		Image img = loadingImage.GetComponent<Image>();
		for (float f = 0f; f <= 1.1f; f += 0.1f) {
			Color c = img.color;
			c.a = f;
			img.color = c;
			yield return null;
		}
		loadingText.gameObject.SetActive(true);
	}

	public IEnumerator FadeOutBlackScreen() {
		this.GetComponent<Canvas>().enabled = true;
		loadingText.gameObject.SetActive(false);
		yield return null;
		Image img = loadingImage.GetComponent<Image>();
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = img.color;
			c.a = f;
			img.color = c;
			yield return null;
		}
		this.GetComponent<Canvas>().enabled = false;
	}

	public IEnumerator FadeInCartoon() {
		yield return null;
	}

	public IEnumerator FadeOutCartoon() {
		yield return null;
	}

	public void KeepItAfterLoading() {
		GameObject.DontDestroyOnLoad(this);
	}

}
