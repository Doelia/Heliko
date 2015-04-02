using UnityEngine;
using System.Collections;

public class AnimationGUI : MonoBehaviour {

	public void animIt() {
		this.GetComponent<Animator>().SetTrigger("go");
	}
}
