using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform sn;
	BPMControlor bc;

	// Use this for initialization
	void Start () {
		bc = this.sn.GetComponent<BPMControlor> ();
	}

	void changeColor(float x) {
		 this.renderer.material.color = new Color (1-x, x, 0);
	}

	int cpt=0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			float d = bc.getScore();
			this.changeColor(d);
			cpt = 0;
		}
		cpt++;

		if (cpt > 10) {
				this.renderer.material.color = new Color (0, 0, 0);
			cpt = 0;
		}

	}
}
