using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteSkipTuto : HelikoObject {

	public Sprite skipSprite;
	public Sprite quitSprite;

	public new void Start () {

		base.Start ();

		int idMiniGame = GameObject.Find ("Tutorial").GetComponent<Tuto>().idGame;
		this.GetComponent<Image>().sprite	 = GetUnlockerManager().haveSuccessTuto(idMiniGame)?
				skipSprite:quitSprite;
	}
	

}
