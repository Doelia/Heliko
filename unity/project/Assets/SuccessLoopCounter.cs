using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuccessLoopCounter : MonoBehaviour {

	int max = 3;
	int nbrSuccess = 0;

	public void Start() {
		this.Reset(3);
		this.Hide ();
	}

	public void Reset(int max) {
		this.nbrSuccess = 0;
		this.max = max;
		updateDisplay();
	}

	public bool AllSuccess() {
		return (nbrSuccess >= max);
	}

	private void updateDisplay() {
		this.GetComponent<Text>().text = nbrSuccess+"/"+max;
	}

	public void AddSuccess() {
		this.nbrSuccess++;
		updateDisplay();
	}

	public void Hide() {
		this.gameObject.SetActive(false);
	}

	public void Show() {
		this.gameObject.SetActive(true);
	} 
}
