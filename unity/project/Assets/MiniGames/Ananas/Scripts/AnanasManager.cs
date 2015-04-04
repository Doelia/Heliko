using UnityEngine;
using System.Collections;

public class AnanasManager : MonoBehaviour {

	public void removeLast() {
		Transform last = null;

		foreach (Transform child in transform) {
			last = child;
		}

		if (last != null) {
			Destroy(last.gameObject);
		}

	}

}
