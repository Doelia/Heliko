using UnityEngine;
using System.Collections;

public abstract class TempoReceiver : MonoBehaviour {
	
	public abstract void onStep ();
	public abstract void onHalfStep ();
}
