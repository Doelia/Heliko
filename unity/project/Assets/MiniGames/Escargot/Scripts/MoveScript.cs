using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public Parallax [] parallax;
	public AudioSource sound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void avancer(int n = 5) {
		foreach (Parallax p in parallax) {
			p.Move(n);
		}
		sound.Play ();
	}
}
