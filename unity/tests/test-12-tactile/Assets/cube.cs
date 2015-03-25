using UnityEngine;
using System.Collections;

public class cube : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void action1()
	{
		if (this.transform.localScale != Vector3.one * 3) {
						this.transform.localScale = Vector3.one * 3;
				} else {
			this.transform.localScale = Vector3.one;

		}
	}
	public void action2()
	{
		if (this.transform.localScale != Vector3.one / 3) {
			this.transform.localScale = Vector3.one / 3;
		} else {
			this.transform.localScale = Vector3.one;
			
		}	}
	public void action3()
	{
		if (this.transform.position != new Vector3 (1, 0, 0)) {
						this.transform.position = new Vector3 (1, 0, 0);
				} else {
				this.transform.position = new Vector3 (0, 0, 0);

				}
		
	}
	public void action4()
	{
		if (this.transform.position != new Vector3 (-1, 0, 0)) {
			this.transform.position = new Vector3 (-1, 0, 0);
		} else {
			this.transform.position = new Vector3 (0, 0, 0);
			
		}
	}
}
