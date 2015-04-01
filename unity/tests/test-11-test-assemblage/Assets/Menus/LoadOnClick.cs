using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingScreen;

	public void LoadScene(int level) {
		loadingScreen.SetActive(true);
		Application.LoadLevelAsync(level);
	}
}

