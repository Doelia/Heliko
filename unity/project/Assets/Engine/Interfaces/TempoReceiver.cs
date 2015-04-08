using UnityEngine;
using System.Collections;

public interface TempoReceiver
{
	void OnStep (int nStep);
	void OnEndMusic();
}
