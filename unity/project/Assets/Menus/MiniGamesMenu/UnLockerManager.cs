using UnityEngine;
using System.Collections;

public class UnLockerManager {

	public bool haveSuccessTuto(int idMiniGame) {
		return PlayerPrefs.GetInt("niveauReussi"+idMiniGame,-1) <= 0;
	}

	// Pour un mini jeu, donne le mini jeu qu'il faut débloquer (0 si aucun)
	public int getUnlocker(int idMinigame) {
		return 0;
		switch (idMinigame) {
		case 1: //Champi
			return 4;
		case 2: // ananas
			return 4;
		case 3: // Magicine
			return 0;
		case 4: // phoques
			return 0;
		}
		return 0;
	}

	public bool isUnlocked(int miniGame) {
		int toSuccess = getUnlocker(miniGame);
		if (toSuccess == 0)
			return true;
		return (isSuccess(toSuccess));
	}

	public bool isSuccess(int miniGame) {
		return (nbrEtoiles (miniGame) > 0);
	}

	public int nbrEtoiles(int miniGame) {
		return PlayerPrefs.GetInt("etoileLevel"+miniGame, 0);
	}
}
