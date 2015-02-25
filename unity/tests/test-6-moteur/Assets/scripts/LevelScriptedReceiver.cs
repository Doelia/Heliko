using UnityEngine;
using System.Collections;

/*
 * Reçoit les évenement d'un niveau. Doit avoir comment parent un LevelScriptedNotifier
 * 
 */


public abstract class LevelScriptedReceiver : MonoBehaviour {

	// Type de 1 à N
	public abstract void onEventType (int type);

}
