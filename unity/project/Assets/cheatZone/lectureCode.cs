using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla.Store.maBoutique;

public class lectureCode : HelikoObject {
	
	public void OnEndEdit(string code)
	{
		if(code.Equals("OUMH_04_AD"))
		{
			if(StoreInventory.GetItemBalance(boutique.NO_ADS_LTVG.ItemId)<=0)
			{
				StoreInventory.GiveItem(boutique.NO_ADS_LTVG.ItemId,1);
			}
		}
		else if(code.StartsWith("OUMH_04_UNLOCK_"))
		{
			int numLevelADebloquer;
			int numLevel=int.Parse(""+code[15]);
			numLevelADebloquer=GetUnlockerManager().getUnlocker(numLevel);
			if(numLevelADebloquer!=0)
			{
				PlayerPrefs.SetInt("etoileLevel"+numLevelADebloquer,3);
			}
		}
		else if(code.StartsWith("OUMH_04_TUTO_"))
		{
			PlayerPrefs.SetInt("niveauReussi"+code[15],1);
			
		}
		else if(code.Equals("OUMH_04_RESET"))
		{
			PlayerPrefs.DeleteAll();
		}
		else 
		{
			print("mauvais code");
		}
	}
}
