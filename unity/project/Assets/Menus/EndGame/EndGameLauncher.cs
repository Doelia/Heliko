using UnityEngine;
using System.Collections;

public class EndGameLauncher : HelikoObject, TempoReceiver  {

	public int idMiniGame;
	public CartoonTransition cartoonTransition;

	[HideInInspector] public int nbFails;
	[HideInInspector] public int pourcentSuccess;

	public new void Start() {
		base.Start ();
		GetBeatCounter().Connect(this);
	}

	private void goEndGame() {
		StartCoroutine(endGame ());
	}

	public void testEndGame() {
		nbFails = 10;
		pourcentSuccess = 90;
		this.goEndGame();
	}

	private IEnumerator endGame() {
		GameObject.DontDestroyOnLoad(this.gameObject);
		cartoonTransition.gameObject.SetActive(true);
		yield return StartCoroutine(cartoonTransition.goAnim());
		yield return null;
		this.GetComponent<LoadOnClick>().LoadScene(constantes.getNumSceneEndGame());
	}

	public void OnEndMusic() {
		nbFails = this.GetPlayerActions().getFailureCount();
		pourcentSuccess = this.GetPlayerActions().getSuccessPercencage();
		this.goEndGame();
	}

	public void OnStep(int n) {}

}
