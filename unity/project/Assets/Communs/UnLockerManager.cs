using UnityEngine;
using System.Collections;

public class UnLockerManager : HelikoObject {

	public UnLockerManager() {
		base.Start ();
	}

	public bool haveSuccessTuto(int idMiniGame) {
		if (constantes.devMode)
			return true;

		return PlayerPrefs.GetInt("niveauReussi"+idMiniGame,-1) != -1;
	}

	// Pour un mini jeu, donne le mini jeu qu'il faut débloquer (0 si aucun)ç
	public int getUnlocker(int idMinigame) {
		if (constantes.unlockAll)
			return 0;

		switch (idMinigame) {
		case 1: //Champi
			return 3;
		case 2: // ananas
			return 0;
		case 3: // Magicien
			return 5;
		case 4: // phoques
			return 0;
		case 5: // escargot
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
