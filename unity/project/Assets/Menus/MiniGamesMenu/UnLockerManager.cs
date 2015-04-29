using UnityEngine;
using System.Collections;

public class UnLockerManager {

	public bool isUnlocked(int miniGame) {
		return true;
	}

	public int nbrEtoiles(int miniGame) {
		return PlayerPrefs.GetInt("etoileLevel"+miniGame, 0);
	}
}
