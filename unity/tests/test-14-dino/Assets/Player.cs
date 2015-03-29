using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Animator anim1;
	public Animator anim2;
	public Animator anim3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			anim1.SetTrigger("go");
			anim2.SetTrigger("go");
			anim3.SetTrigger("go");
		}
	}
}
