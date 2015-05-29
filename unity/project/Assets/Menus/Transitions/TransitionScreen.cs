using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionScreen : HelikoObject {

	public Transform loadingImage;
	public Transform loadingText;
	public Transform heliko;

	public AsyncOperation loading;
	public new void Start() {
		if (isStart) return;
		base.Start ();
		heliko.gameObject.SetActive(false);
	}

	public bool LoadingScreenIsOpen() {
		return this.GetComponent<Canvas>().enabled;
	}

	// Commencer le chargement
	public IEnumerator FadeInBlackScreen() {
		this.loading = loading;
		this.GetComponent<Canvas>().enabled = true;
		Image img = loadingImage.GetComponent<Image>();

		for (float f = 0f; f <= 1.1f; f += 0.1f) {
			Color c = img.color;
			c.a = f;
			img.color = c;
			yield return null;
		}
		loadingText.gameObject.SetActive(true);
		heliko.gameObject.SetActive(true);
	}

	// Finir le chargement
	public IEnumerator FadeOutBlackScreen() {
		this.GetComponent<Canvas>().enabled = true;
		loadingText.gameObject.SetActive(false);
		heliko.gameObject.SetActive(false);
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
