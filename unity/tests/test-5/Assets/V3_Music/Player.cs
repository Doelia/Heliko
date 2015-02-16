using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Animator anim;
	public AudioSource sound;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator >();
	}
	
	void goMove() {
		anim.SetBool ("change", true);
		sound.Play();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("change", false);
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			goMove();
		}
		
	}

}
