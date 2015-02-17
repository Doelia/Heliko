using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {

	Vector2 positionFirst;

	void Start() {
		foreach (Transform s in transform) {
			positionFirst = s.localPosition;
		}

	}
	
	void Update()
	{
		foreach (Transform s in transform) {
			s.Translate(new Vector2(0.003f,0));
			if (!s.renderer.IsVisibleFrom(Camera.main)) {
				s.localPosition = positionFirst;
			}
		}
	}

}
