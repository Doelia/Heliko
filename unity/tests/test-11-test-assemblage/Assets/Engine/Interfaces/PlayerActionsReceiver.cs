using UnityEngine;
using System.Collections;

public interface PlayerActionReceiver
{
	void onFailure();
	void onSuccess();
}

