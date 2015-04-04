using UnityEngine;
using System.Collections;

public class ForceLoad : MonoBehaviour {

	public GameObject[] objects;

	public void Awake () {
		foreach (GameObject item in objects) {
			item.SetActive(true);
		}
	}
	

}
