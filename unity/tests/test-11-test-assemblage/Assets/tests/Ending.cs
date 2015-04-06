using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {
	public Transform container;
	public float x;
	public float y;
	public float minSize;

	private float cpt = 0;
	public float reduceFactor = 0.01f;

	void Start () {
		container.position = new Vector3(x, y, -9);
	}

	void Update () {
		if (cpt*reduceFactor <= minSize) {
			cpt++;
		}

		//sprite.position = new Vector3(x, getBounds(rond).center.y - getBounds(rond).center.y * cpt * reduceFactor, 0);

		container.localScale = new Vector3(1 - reduceFactor * cpt,
		                              1 - reduceFactor * cpt,
		                              1);
	}
}
