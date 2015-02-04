using UnityEngine;
using System.Collections;

public class InitScene : MonoBehaviour {

	// Use this for initialization
	public Transform prefab;

	void Start () {
		for (int i = 0; i < 100; i++) {
			Instantiate(prefab, new Vector3(i * 5, 1, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
