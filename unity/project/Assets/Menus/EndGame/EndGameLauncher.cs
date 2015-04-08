using UnityEngine;
using System.Collections;

public class EndGameLauncher : HelikoObject, TempoReceiver  {

	public int idMiniGame;
	[HideInInspector] public int nbFails;
	[HideInInspector] public int pourcentSuccess;

	public new void Start() {
		base.Start ();
		GetBeatCounter().Connect(this);
	}

	public void goEndGame() {
		GameObject.DontDestroyOnLoad(this.gameObject);
		this.GetComponent<LoadOnClick>().LoadScene(4);
	}

	public void OnEndMusic() {
		nbFails = this.GetPlayerActions().getFailureCount();
		pourcentSuccess = this.GetPlayerActions().getSuccessPercencage();
		this.goEndGame();
	}

	public void OnStep(int n) {}

}
