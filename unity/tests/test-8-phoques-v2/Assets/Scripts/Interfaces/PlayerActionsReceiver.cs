using UnityEngine;
using System.Collections;

public interface PlayerActionReceiver
{
	void onEventType (int type);
	void onFailure();
}
