using UnityEngine;
using System.Collections;

public class Lanceur : HelikoObject,LevelScriptedReceiver {

	public Animator anim1;
	public Animator anim2;
	public Transform ananasPrefab;
	public AnanasManager parentAnanas;
	public LevelScripted level;

	public AudioSource preparerSound;
	public AudioSource lancerSound;

	public new void Start () {
		this.level.connect(this);
	}


	void lancerAnanas() {
		Transform ananas = Instantiate(ananasPrefab);
		ananas.SetParent(parentAnanas.transform);
		ananas.GetComponent<Animator>().SetTrigger("go");
	}

	public void OnAction (int type) {
		if (type == 1) {
			anim1.SetTrigger("go");
			preparerSound.Play();
		}
		if (type == 2) {
			lancerAnanas ();
			lancerSound.Play ();
			anim1.SetTrigger("next");
			anim2.SetTrigger("go");
		}
	}
	

}
