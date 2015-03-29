using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Animator anim1;
	public Animator anim2;

	public Transform ananasPrefab;
	public AnanasManager parentAnanas;


	// Use this for initialization
	void Start () {
		anim2.SetTrigger("go");
	}

	void lancerAnanas() {
		Transform ananas = Instantiate(ananasPrefab);
		ananas.SetParent(parentAnanas.transform);
		ananas.GetComponent<Animator>().SetTrigger("go");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.O)) {
			anim1.SetTrigger("go");

		}

		if (Input.GetKeyDown (KeyCode.P)) {
			lancerAnanas ();
			anim1.SetTrigger("next");
			anim2.SetTrigger("go");
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			parentAnanas.removeLast();
			
		}


	}
}
