using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla.Store.maBoutique;
using UnityEngine.UI;

public class lectureCode : HelikoObject {
	
	public Text feedBack;
	public void OnEndEdit(string code)
	{
		if(code.Equals("OUMH_04_AD"))
		{
			#if UNITY_ANDROID || UNITY_EDITOR ||UNITY_IOS
			if(StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId)==0)
			{
				StoreInventory.GiveItem(boutique.NO_ADS_LTVG.ItemId,1);
				feedBack.text="Suppression des pubs";
			}
			#endif
		}
		else if(code.StartsWith("OUMH_04_UNLOCK_"))
		{
			int numLevelADebloquer;
			int numLevel=int.Parse(""+code[15]);
			print(numLevel);
			numLevelADebloquer=GetUnlockerManager().getUnlocker(numLevel);
			feedBack.text="Debloquage du niveau n°"+numLevelADebloquer;
			if(numLevelADebloquer!=0)
			{
				PlayerPrefs.SetInt("etoileLevel"+numLevelADebloquer,3);
			}
		}
		else if(code.StartsWith("OUMH_04_TUTO_"))
		{
			feedBack.text="Debloquage du tuto n°"+code[15];
			PlayerPrefs.SetInt("niveauReussi"+code[15],1);
			
		}
		else if(code.Equals("OUMH_04_RESET"))
		{
			feedBack.text="Réinitialisation du jeu";
			PlayerPrefs.DeleteAll();
			#if UNITY_ANDROID || UNITY_EDITOR ||UNITY_IOS
			if(StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId)>0)
			{
				StoreInventory.TakeItem(boutique.NO_ADS_LTVG.ItemId,StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId));
			}
			#endif
		}
		else 
		{
			feedBack.text="mauvais code";
		}
	}
}
