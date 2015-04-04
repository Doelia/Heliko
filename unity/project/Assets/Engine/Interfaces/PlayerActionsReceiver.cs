using UnityEngine;
using System.Collections;

public interface PlayerActionReceiver
{
	void OnFailure();
	void OnSuccess();
	void OnSuccessLoop();
}

