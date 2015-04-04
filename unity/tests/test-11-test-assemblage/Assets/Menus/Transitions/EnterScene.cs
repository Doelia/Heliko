using UnityEngine;
using System.Collections;

public class EnterScene : HelikoObject {

	private TransitionScreen loadingScreen;

	public new void Start() {
		base.Start ();
		loadingScreen = this.getTransitionScreen();
		loadingScreen.closeLoadingScene();
	}

	
}
