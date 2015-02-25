using UnityEngine;
using System.Collections;

public class TextTest : MonoBehaviour {

	public TextAsset txtFile;
	public string [] fileData;

	// Use this for initialization
	void Start () {
		fileData = txtFile.text.Split ('\n');
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (fileData[0]);
	}
}
