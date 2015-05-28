using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionScreen : HelikoObject {

	public Transform loadingImage;
	public Transform loadingText;
	public Transform heliko;
	public Transform container;

	private AsyncOperation loadingScene;

	public new void Start() {
		if (isStart) return;
		base.Start ();
		heliko.gameObject.SetActive(false);
	}

	public bool LoadingScreenIsOpen() {
		return this.GetComponent<Canvas>().enabled;
	}

	// Commencer le chargement
	public IEnumerator FadeInBlackScreen(AsyncOperation loadingScene) {
		this.GetComponent<Canvas>().enabled = true;
		Image img = loadingImage.GetComponent<Image>();

		for (float f = 0f; f <= 1.1f; f += 0.1f) {
			Color c = img.color;
			c.a = f;
			img.color = c;
			yield return null;
		}
		yield return null;
		loadingText.gameObject.SetActive(true);
		yield return null;
		this.loadingScene = loadingScene;
		StartCoroutine(loop());
		yield return null;
		heliko.gameObject.SetActive(true);
	}

	// Finir le chargement
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

	public IEnumerator loop() {
		while(!loadingScene.isDone) {
			Debug.Log("move");
			heliko.localPosition = new Vector3(container.GetComponent<RectTransform>().rect.size.x * 0.5f - container.GetComponent<RectTransform>().rect.size.x * loadingScene.progress,
			                                   heliko.localPosition.y,
			                                   heliko.localPosition.z);
			yield return new WaitForSeconds(0.001f);
		}
		heliko.localPosition = new Vector3(container.GetComponent<RectTransform>().rect.size.x * 0.5f,
								            heliko.localPosition.y,
								            heliko.localPosition.z);
		heliko.gameObject.SetActive(false);
	}
}
