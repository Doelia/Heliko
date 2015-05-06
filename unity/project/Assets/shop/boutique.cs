
		using UnityEngine;
		using System.Collections;
		using System.Collections.Generic;
		using Soomla;
		namespace Soomla.Store.maBoutique {
	/// <summary>
	/// This class defines our game's economy, which includes virtual goods, virtual currencies
	/// and currency packs, virtual categories
	/// </summary>
	public class boutique : IStoreAssets{
		/// <summary>
		/// see parent.
		/// </summary>
		public int GetVersion() {
			return 0;
		}
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrency[] GetCurrencies() {
			return new VirtualCurrency[]{COIN_CURRENCY};
		}
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualGood[] GetGoods() {
			return new VirtualGood[] {NO_ADS_LTVG};
		}
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrencyPack[] GetCurrencyPacks() {
			return new VirtualCurrencyPack[] {};
		}
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCategory[] GetCategories() {
			return new VirtualCategory[]{GENERAL_CATEGORY};
		}
		/** Static Final Members **/
		public const string COIN_CURRENCY_ITEM_ID = "currency_coins";

		public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads";
		/** Virtual Currencies **/
		public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency(
			"Coins", // name
			"", // description
			COIN_CURRENCY_ITEM_ID // item id
			);
		/** LifeTimeVGs **/
		// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
		public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
			"No Ads", // name
			"No More Ads!", // description
			"no_ads", // item id
			new PurchaseWithMarket(NO_ADS_LIFETIME_PRODUCT_ID, 0.99)); // the way this virtual good is purchased
		
		/** Virtual Categories **/
		// The muffin rush theme doesn't support categories, so we just put everything under a general category.
		public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
			"General", new List<string>(new string[] {  })
			);
	}
}
