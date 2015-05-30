using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChartboostSDK;
using Soomla.Store;
using Soomla.Store.maBoutique;


public class EndGame : HelikoObject {

	private int pourcent;
	private int nbrErreurs;
	private int idMiniGame;
	public GoogleAnalyticsV3 googleAnalytics;
	
	public AudioSource soundReussi;
	public AudioSource soundRate;

	public void Awake() {
		this.gameObject.SetActive(true);
	}

	public new void Start() {
		base.Start();
		closeIt();
		if (GameObject.Find ("EndGameLauncher") != null) {
			Debug.Log ("Launch EndGameLauncher OK");
			EndGameLauncher endGameInformations = GameObject.Find ("EndGameLauncher").GetComponent<EndGameLauncher>();
			showAdvertise();
			this.setValues(endGameInformations.pourcentSuccess, endGameInformations.nbFails, endGameInformations.idMiniGame);
			this.gameObject.SetActive(true);
			StartCoroutine(this.startShowing());
			Destroy(endGameInformations.gameObject);
		} else {
			Debug.LogWarning("Impossible de trouver l'objet EndGameLauncher");
		}
	}

	// Pour le test (mode dev)
	public void testIt() {
		this.setValues(20,1,2);
		Debug.Log ("Rank = "+getRank ());
		this.gameObject.SetActive(true);
		StartCoroutine(this.startShowing());
	}

	public void setValues(int p_pourcent, int p_nbrErreurs, int p_idMiniGame) {
		this.pourcent = p_pourcent;
		this.nbrErreurs = p_nbrErreurs;
		this.idMiniGame = p_idMiniGame;

		Debug.Log ("idMiniGame = "+idMiniGame+", nbrErreurs="+nbrErreurs+", pourcent="+pourcent);
		
		//enregistrement résultat
		int nbrEtoiles = getRank();
		if (nbrEtoiles > PlayerPrefs.GetInt("etoileLevel"+p_idMiniGame,-1)) {
			PlayerPrefs.SetInt("etoileLevel"+p_idMiniGame,nbrEtoiles);
		}
		sendGoogleAnnalyticsInfo();
		
	}
	
	public void showAdvertise() {
	#if UNITY_IOS || UNITY_ANDROID
		if (StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId)<=0) {
			Chartboost.showInterstitial(CBLocation.Default);
		}
	#endif
	}
	
	public void sendGoogleAnnalyticsInfo()
	{
		googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("EndGame")
		.SetEventAction("Pourcent")
		.SetEventValue(pourcent));
		
		googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("EndGame")
		.SetEventAction("nbrErreurs")
		.SetEventValue(nbrErreurs));
		
		googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("EndGame")
		.SetEventAction("EndingGame")
		.SetEventValue(idMiniGame));
	}

	private void playSoundStar(int i) {
		GameObject.Find ("soundStar"+i).GetComponent<AudioSource>().Play();
	}

	public int getRank() {
		if (nbrErreurs == 0) {
			return 3;
		}
		if (pourcent >= 80) {
			return 2;
		}
		if (pourcent >= 60) {
			return 1;
		}
		return 0;
	}

	public void closeIt() {
		this.gameObject.SetActive(false);
	}



	// AFFICHAGE (animations, etc)

	IEnumerator startShowing() {
		Debug.Log ("startShowing");
		GameObject bg = GameObject.Find ("ContainerEndGame");
		bg.GetComponent<AnimationGUI>().animIt();

		if (getRank () == 0) {
			soundRate.Play();
		} else {
			soundReussi.Play();
		}

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
				playSoundStar(i);
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
		string texte = "";
		switch (getRank ()) {
			case 3: // Parfait
				texte = "PARFAIT !!"; break;
			case 2: // Parfait
				texte = "BRAVO !!"; break;
			case 1: // Parfait
				texte = "REUSSI !"; break;
			case 0: // Parfait
				texte = "PERDU..."; break;
		} 

		GameObject.Find ("SuccessText").GetComponent<Text>().text = texte;
	}

	public void restartMiniGame() {
		if (constantes.devMode)
			Debug.Log ("restartMiniGame "+idMiniGame);
		this.GetComponent<LoadOnClick>().LoadScene(constantes.getNumSceneFromIdMiniGame(idMiniGame));
	}

}
