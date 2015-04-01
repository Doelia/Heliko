using UnityEngine;
using System.Collections;

public abstract class StepTuto : HelikoObject {

	Tuto tuto;

	// Appelé à la construction du tuto
	public void init(Tuto t) {
		this.tuto = t;
	}

	// Appelé par le tuto quand il faut jouer la step
	public abstract void play();

	// A appeler par la classe fille quand la step est terminée
	public void endStep() {
		tuto.next();
	}
}
