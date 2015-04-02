using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : HelikoObject {

	private int pourcent;
	private int nbrErreurs;

	public new void Start() {
		base.Start();
	}

	// Pour le test (mode dev)
	public void testIt() {
		this.Start ();
		this.setValues(80,4);
		this.startShowing();
	}

	public void setValues(int p_pourcent, int p_nbrErreurs) {
		this.pourcent = p_pourcent;
		this.nbrErreurs = p_nbrErreurs;
	}

	public int getRank() {
		if (nbrErreurs == 0) {
			return 3;
		}
		if (pourcent > 90) {
			return 2;
		}
		if (pourcent > 75) {
			return 1;
		}
		return 0;
	}

	public void closeIt() {
		this.gameObject.SetActive(false);
	}

	// AFFICHAGE (animations, etc)

	public void startShowing() {
		this.gameObject.SetActive(true);
		this.showStars();
		if (constantes.showDetailOnEndGame) {
			showTauxReussite();
			showNombreErreurs();
		}
	}

	private void showTauxReussite() {
		GameObject.Find ("PReussite").GetComponent<Text>().text = pourcent+"%";
	}

	private void showNombreErreurs() {
		GameObject.Find ("NErrors").GetComponent<Text>().text = nbrErreurs+"";
	}

	private void showSuccessText() {
		string txt = "undefined";
		switch (getRank ()) {
			case 0:
				txt = "Veuillez réésayer...";
				break;
			case 1:
				txt = "Niveau réussi, bravo !";
				break;
			case 2:
				txt = "Excelent !";
				break;
			case 3:
				txt = "Score parfait !!";
				break;
		}
		GameObject.Find ("SuccessText").GetComponent<Text>().text = txt;
	}

	private void showStars() {
		for (int i = 1; i <= 3; i++) {
			if (getRank() > i) {
				GameObject.Find ("Star"+i).SetActive(true);
			} else {
				GameObject.Find ("Star"+i).SetActive(false);
			}
		}
	}


}
