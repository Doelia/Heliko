using UnityEngine;
using System.Collections;

public class PNJ : MonoBehaviour, LevelScriptedReceiver {

	private Animator brasDroit;
	private Animator brasGauche;
	private Animator animCarapace;
	private Animator animChampiPNJ;

	public LevelScripted level;

	public Transform brasGaucheTrasform;
	public Transform brasDroitTrasform;
	public Transform carapace;
	public Transform champiPNJ;

	public Sprite content;
	public Sprite pasContent;

	public AudioSource sound;

	public void Start () {
		this.level.connect(this);
		brasDroit = brasDroitTrasform.GetComponent<Animator>();
		brasGauche = brasGaucheTrasform.GetComponent<Animator>();
		animCarapace = carapace.GetComponent<Animator>();
		animChampiPNJ = champiPNJ.GetComponent<Animator>();
	}
	
	public void onAction (int type) {
		if (type == 1) {
			brasDroit.SetTrigger ("Down");
		} else if (type == 2) {
			brasGauche.SetTrigger ("Down");
		} else if (type == 3) {
			brasDroit.SetTrigger ("Down");
			brasGauche.SetTrigger ("Down");
		}
		animCarapace.SetTrigger("Move");
		animChampiPNJ.SetTrigger("Move");
		sound.GetComponent<AudioSource>().Play();
		
	}

	public void onFailure() {
		animPasContent();
	}

	IEnumerator animPasContent() {
		this.setReaction(false);
		yield return new WaitForSeconds(1.0f);
		this.setReaction(true);
	}

	public void setReaction(bool isGood) {
		champiPNJ.GetComponent<SpriteRenderer>().sprite = isGood?content:pasContent;
	}


}
