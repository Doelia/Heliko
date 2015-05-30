using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompteurMagicien : HelikoObject, LevelScriptedReceiver {

	public LevelScripted level;
	private Text txt;
	private int valueCompteur;
	private int valueMax;
	bool isInit = false;
	public AudioSource ticSound;
	public AudioSource tacSound;

	public new void Start() {
		base.Start ();
		this.level.connect(this);
		txt = gameObject.GetComponent<Text>();
		setGreen();
	}

	public void setMax(int max) {
		valueMax = max;
		valueCompteur = 0;
		isInit = false;
	}

	public void reset() {
		isInit = false;
		this.hide();
	}

	public void startIncrement() {
		isInit = true;
		if (valueMax == 7)
			valueCompteur = -1;
		else
			valueCompteur = 0;
	}

	private void setString(string s) {
		txt.text = s;
	}

	public void hide() {
		setString("");
	}

	public void setGreen() {
		this.txt.color = new Color(0,255,0);
	}

	public void setRed() {
		if (valueCompteur == valueMax-1) {
			setGreen();
			return;
		}
		if (valueCompteur == valueMax) {
			setYellow();
			return;
		}
		this.txt.color = new Color(255,0,0);
	}

	public void setYellow() {
		this.txt.color = new Color(255,255,0);
	}

	public void OnAction (int type) {
		if (!isInit) {
			hide();
			return;
		}

		if (type == 1) {
			valueCompteur++;
			if (valueCompteur > valueMax || valueCompteur <= 0) {
				hide();
			} else {

				if (valueCompteur == valueMax) {
					setString(valueCompteur+"!");
					tacSound.Play();
					setYellow();
				} else {
					ticSound.Play();
					setString(valueCompteur+"");
				}
			}
		}
			
	}


	
}
