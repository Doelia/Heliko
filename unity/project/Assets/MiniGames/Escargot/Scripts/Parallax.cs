using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	public float speed;
	public bool loop;

	private bool [] visibleOnce;

	// Use this for initialization
	void Start () {
		visibleOnce = new bool[transform.childCount];
		for(int i = 0; i < transform.childCount; i++) {
			visibleOnce[i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Move (Time.deltaTime);
	}

	public void Move(float amplitude) {
		amplitude *= speed;
		int i = 0;
		foreach(Transform e in 	transform) {
			e.Translate(amplitude , 0, 0);
			if(e.GetComponent<Renderer>().IsVisibleFrom(Camera.main)) {
				visibleOnce[i] = true;
			} else if (loop && visibleOnce[i]){
				e.Translate((GetComponent<Renderer>().bounds.size.x + e.GetComponent<Renderer>().bounds.size.x) * ((speed > 0)?-1:1), 0, 0);
				visibleOnce[i] = false;
			}
			i++;
		}
	}
}
