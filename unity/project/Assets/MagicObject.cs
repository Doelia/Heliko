using UnityEngine;
using System.Collections;

public class MagicObject : MonoBehaviour {

	public Sprite[] sprites;

	public void ChangeObject(int n) {
		Sprite sp;
		sp = sprites[n-1];
		this.GetComponent<SpriteRenderer>().sprite = sp;
	}
}
