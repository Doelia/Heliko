using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	private int pourcent;
	private int nbrErreurs;

	public void testIt() {
		this.setValues(80,4);
		this.show();
	}

	public void setValues(int p_pourcent, int p_nbrErreurs) {
		this.pourcent = p_pourcent;
		this.nbrErreurs = p_nbrErreurs;
	}

	public void show() {
		this.gameObject.SetActive(true);
		this.showTauxReussite();
		this.showNombreErreurs();
	}

	public void closeIt() {
		this.gameObject.SetActive(false);
	}

	private void showTauxReussite() {
		GameObject.Find ("PReussite").GetComponent<Text>().text = pourcent+"%";
	}

	private void showNombreErreurs() {
		GameObject.Find ("NErrors").GetComponent<Text>().text = nbrErreurs+"";
	}


}
