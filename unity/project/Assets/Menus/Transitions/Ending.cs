using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {
	public Transform container;
	public float x;
	public float y;
	public float minSize;
	public Transform north;
	public Transform south;
	public Transform east;
	public Transform west;

	private float cpt = 0;
	public float reduceFactor = 0.01f;
	
	void Start () {
		container.position = new Vector3(x, y, -9);
		Physics2D.IgnoreCollision(north.GetComponent<BoxCollider2D>(), east.GetComponent<BoxCollider2D>());
		Physics2D.IgnoreCollision(north.GetComponent<BoxCollider2D>(), west.GetComponent<BoxCollider2D>());
		Physics2D.IgnoreCollision(south.GetComponent<BoxCollider2D>(), east.GetComponent<BoxCollider2D>());
		Physics2D.IgnoreCollision(south.GetComponent<BoxCollider2D>(), west.GetComponent<BoxCollider2D>());
	}

	Bounds getBounds(Transform t) {
		return t.GetComponent<BoxCollider2D>().bounds;
	}
	
	void Update () {
		if (cpt*reduceFactor <= minSize) {
			cpt++;
		}
		int force = 100;
		Vector3 forceDirection = new Vector3(0, -force, 0);
		north.GetComponent<Rigidbody2D>().velocity = forceDirection;

		forceDirection = new Vector3(0, force, 0);
		south.GetComponent<Rigidbody2D>().velocity = forceDirection;

		forceDirection = new Vector3(force, 0, 0);
		east.GetComponent<Rigidbody2D>().velocity = forceDirection;

		forceDirection = new Vector3(-force, 0, 0);
		west.GetComponent<Rigidbody2D>().velocity = forceDirection;

		container.localScale = new Vector3(1 - reduceFactor * cpt,
		                                   1 - reduceFactor * cpt,
		                                   1);
	}
}
