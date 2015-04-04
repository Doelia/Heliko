using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : HelikoObject {

	private int pourcent;
	private int nbrErreurs;
	private int idMiniGame;

	public void Awake() {
		this.gameObject.SetActive(true);
	}

	public new void Start() {
		base.Start();
		closeIt();
		EndGameLauncher endGameInformations = GameObject.Find ("EndGameLauncher").GetComponent<EndGameLauncher>();
		if (endGameInformations != null) {
			this.setValues(endGameInformations.pourcentSuccess, endGameInformations.nbFails, endGameInformations.idMiniGame);
		} else {
			Debug.LogWarning("Impossible de trouver l'objet EndGameParameters");
		}
	}

	// Pour le test (mode dev)
	public void testIt() {
		//this.setValues(95,1,2);
		Debug.Log ("Rank = "+getRank ());
		this.gameObject.SetActive(true);
		StartCoroutine(this.startShowing());
	}

	public void setValues(int p_pourcent, int p_nbrErreurs, int p_idMiniGame) {
		this.pourcent = p_pourcent;
		this.nbrErreurs = p_nbrErreurs;
		this.idMiniGame = p_idMiniGame;
	}

	public int getRank() {
		if (nbrErreurs == 0) {
			return 3;
		}
		if (pourcent >= 90) {
			return 2;
		}
		if (pourcent >= 75) {
			return 1;
		}
		return 0;
	}

	public void closeIt() {
		this.gameObject.SetActive(false);
	}

	// AFFICHAGE (animations, etc)

	IEnumerator startShowing() {
		GameObject bg = GameObject.Find ("ContainerEndGame");
		bg.GetComponent<AnimationGUI>().animIt();
		yield return new WaitForSeconds(.4f);
		StartCoroutine(showStars());
		yield return new WaitForSeconds(1);
		this.showSuccessText();
		if (constantes.showDetailOnEndGame) {
			showTauxReussite();
			showNombreErreurs();
		} else {
			GameObject.Find ("PReussite").SetActive(false);
			GameObject.Find ("NErrors").SetActive(false);
		}
	}

	IEnumerator showStars() {
		for (int i = 1; i <= 3; i++) {
			if (getRank() >= i) {
				GameObject.Find ("Star"+i).GetComponent<AnimationGUI>().animIt();
				yield return new WaitForSeconds(.3f);
			}
		}
	}

	private void showTauxReussite() {
		GameObject.Find ("PReussite").GetComponent<Text>().text = pourcent+"%";
	}

	private void showNombreErreurs() {
		GameObject.Find ("NErrors").GetComponent<Text>().text = nbrErreurs+"";
	}

	private void showSuccessText() {
		GameObject.Find ("SuccessText").GetComponent<GUISpriteSwitcher>().setSprite(getRank());
	}

	public void restartMiniGame() {
		this.GetComponent<LoadOnClick>().LoadScene(constantes.getNumSceneFromIdMiniGame(idMiniGame));
	}




}
