using UnityEngine;
using System.Collections;

public interface TempoReceiver
{
	void onStep ();
	void onSuccessWindowExit ();
	void onSuccessWindowEnter ();
}
