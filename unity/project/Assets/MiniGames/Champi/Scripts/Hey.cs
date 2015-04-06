using UnityEngine;
using System.Collections;

public class Hey : MonoBehaviour, LevelScriptedReceiver {
	
	private Animator brasDroit;
	private Animator brasGauche;
	public LevelScripted level;
	
	public Transform brasGaucheTrasform;
	public Transform brasDroitTrasform;
	
	public void Start () {
		this.level.connect(this);
		brasDroit = brasDroitTrasform.GetComponent<Animator>();
		brasGauche = brasGaucheTrasform.GetComponent<Animator>();
	}
	
	public void OnAction (int type) {
		if (type == 1) {
			brasDroit.SetTrigger ("Up");
			brasGauche.SetTrigger ("Up");
		}
	}
	
	public void onFailure() {
		
	}
	
}
