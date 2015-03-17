using UnityEngine;
using System.Collections;

public class IAModel : MonoBehaviour, LevelScriptedReceiver {

	private Animator anim;
	public LevelScripted level;

	public Transform animationTransform;
	public Transform carapace;

	public void Start () {
		this.level.connect(this);
		anim = animationTransform.GetComponent<Animator>();
	}
	
	public void onAction (int type) {
		if (type == 1) {
			anim.SetTrigger ("Move");
			this.GetComponent<AudioSource>().Play();
			carapace.GetComponent<Animator>().SetTrigger("Move");
		}
	}

	public void onFailure() {
		
	}

}
