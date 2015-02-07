using UnityEngine;
using System.Collections;

public class AnimatedSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public int delay = 20; 

	int cpt = 0;
	bool playing = false;

	int cptAnimation = 0;
	int dureeAnimation = 7;

	int getSizeFromStep() {
		int v = 0;
		if (cptAnimation < dureeAnimation/2) {
			v = cptAnimation;
		} else {
			v = dureeAnimation - cptAnimation;
		}

		if (cptAnimation > dureeAnimation) {
			playing = false;
		} else {
			cptAnimation++;
		}

		return v;
	}

	void playAnimation() {
		float size = getSizeFromStep () / 10f;
		this.transform.localScale = new Vector3 (.2f + size, this.transform.localScale.y, this.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (cpt % delay == 0) {
			playing = true;
			cptAnimation = 0;
		}

		if (playing)
			playAnimation();

		cpt++;
	}
}
