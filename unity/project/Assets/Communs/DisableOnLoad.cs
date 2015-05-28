using UnityEngine;
using System.Collections;

public class DisableOnLoad : MonoBehaviour {

	void Start () {
		this.gameObject.SetActive(false);
	}
	
	void Update () {
	
	}
}
