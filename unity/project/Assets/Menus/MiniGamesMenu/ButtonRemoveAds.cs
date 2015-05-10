using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla.Store.maBoutique;
using System;

public class ButtonRemoveAds : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	void Start()
	{
		if(StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId)>0)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
	// Use this for initialization
	public void click () {
			googleAnalytics.LogEvent(new EventHitBuilder()
		.SetEventCategory("Button")
		.SetEventLabel("removeAds")
		.SetEventAction("click"));
		if(StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId)<=0)
		{
			try {
					SoomlaStore.RestoreTransactions();
				} catch (Exception e) {
					Debug.LogError ("SOOMLA/UNITY " + e.Message);
				}
			try {
			StoreInventory.BuyItem (boutique.NO_ADS_LTVG.ItemId);
			} catch (Exception e) {
				Debug.LogError ("SOOMLA/UNITY " + e.Message);
			}
		}

	}
	

}
