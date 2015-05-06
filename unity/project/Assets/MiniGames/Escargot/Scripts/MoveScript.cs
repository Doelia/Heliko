using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public Parallax [] parallax;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void avancer(float n) {
		foreach (Parallax p in parallax) {
			p.Move(n);
		}
	}
}
