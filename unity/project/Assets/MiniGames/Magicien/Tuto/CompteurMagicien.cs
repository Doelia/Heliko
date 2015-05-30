using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompteurMagicien : HelikoObject, LevelScriptedReceiver {

	public LevelScripted level;
	private Text txt;
	private int valueCompteur;
	private int valueMax;
	bool isInit = false;

	public new void Start() {
		base.Start ();
		this.level.connect(this);
		txt = this.gameObject.GetComponent<Text>();
	}

	public void setMax(int max) {
		valueMax = max;
		valueCompteur = 0;
	}

	public void hide() {
		setString("");
	}

	private void setString(string s) {
		txt.text = s;
	}

	public void startIncrement() {
		isInit = true;
		valueCompteur = 0;
	}

	private void updateDisplay() {
		setString(valueCompteur+"");
	}

	public void OnAction (int type) {
		if (!isInit) {
			return;
		}

		if (type == 1) {
			valueCompteur++;
			if (valueCompteur >= valueMax || valueCompteur <= 0) {
				hide();
			} else {
				updateDisplay();
			}
		}
			
	}

	private void increment() {
		setString(valueCompteur+"");
	}



	
}
