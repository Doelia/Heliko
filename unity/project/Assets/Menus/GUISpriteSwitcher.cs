using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUISpriteSwitcher : MonoBehaviour {

	public Sprite[] sprites;

	public void setSprite(int n) {
		this.GetComponent<Image>().sprite = sprites[n];
	}
}
