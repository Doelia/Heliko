using UnityEngine;
using System.Collections;
using ChartboostSDK;
using Soomla.Store;
using Soomla.Store.maBoutique;
using System;

public class EndGameLauncher : HelikoObject, TempoReceiver  {

	public int idMiniGame;
	public CartoonTransition cartoonTransition;

	[HideInInspector] public int nbFails;
	[HideInInspector] public int pourcentSuccess;

	public new void Start() {
		base.Start ();
		GetBeatCounter().Connect(this);
		chargeAdvertise();
	}

	private void goEndGame() {
		StartCoroutine(endGame ());
	}

	public void testEndGame() {
		nbFails = 0;
		pourcentSuccess = 100;
		this.goEndGame();
	}
	
	private void chargeAdvertise() {
		try
		{
			if (StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId) <= 0) {
				Chartboost.cacheInterstitial(CBLocation.Default);
			}
		}
		catch(Exception e)
		{
			Debug.Log ("SOOMLA/UNITY " + e.Message);
		}
	}

	private IEnumerator endGame() {
		Debug.Log("Start end game  #"+idMiniGame+"...");
		GameObject.DontDestroyOnLoad(this.gameObject);
		cartoonTransition.gameObject.SetActive(true);
		yield return StartCoroutine(cartoonTransition.goAnim());
		yield return null;
		Debug.Log("Animation cartoon OK. Chargement de la scène...");
		this.GetComponent<LoadOnClick>().LoadScene(constantes.getNumSceneEndGame());
	}

	public void OnEndMusic() {
		nbFails = this.GetPlayerActions().getFailureCount();
		pourcentSuccess = this.GetPlayerActions().getSuccessPercencage();
		this.goEndGame();
	}

	public void OnStep(int n) {}

}
