using UnityEngine;
using System.Collections;

public class cube : MonoBehaviour {

	public Transform evenement;

	public void action1()
	{
		print ("1");
		Transform myEvent= Transform.Instantiate(evenement) as Transform;
		myEvent.GetComponent<TextMesh>().text = "1";

	}
	public void action2()
	{
		print ("2");
		Transform myEvent= Transform.Instantiate(evenement) as Transform;
		myEvent.GetComponent<TextMesh>().text = "2";
	}
	public void action3()
	{
		print ("3");
		Transform myEvent= Transform.Instantiate(evenement) as Transform;
		myEvent.GetComponent<TextMesh>().text = "3";
	}
	public void action4()
	{
		print ("4");
		Transform myEvent= Transform.Instantiate(evenement) as Transform;
		myEvent.GetComponent<TextMesh>().text = "4";;

	}
}
