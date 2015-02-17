using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform bpmControlor;
	public AudioSource sound;

	BPMControlor bc;
	Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator >();
		bc = bpmControlor.GetComponent<BPMControlor> ();
	}

	void changeColor(float x) {
		this.GetComponent<SpriteRenderer>().color = new Color (1-x, x, 0);
	}
	
	void goMove() {
		float d = bc.getScore();
		this.changeColor(d);

		anim.SetBool ("change", true);
		sound.Play();
	}

	float lastTime = 0;
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("change", false);

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				goMove ();
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			float diff = Time.time - lastTime;
			lastTime = Time.time;
			goMove();
		}
	}

}
