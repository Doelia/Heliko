﻿#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using Soomla.Store.maBoutique;
public class initialisationBoutique : MonoBehaviour {

	public static bool alreadyInitialized=false;
	// Use this for initialization
	void Start () {
			if(alreadyInitialized)
			{
				GameObject.Destroy(this.gameObject);
			}
			else
			{
				Debug.Log ("initialisation boutique");
				SoomlaStore.Initialize(new boutique());
				DontDestroyOnLoad(gameObject);
				alreadyInitialized=true;
			}


	}
	

}
#endif
