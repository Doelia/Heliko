using UnityEngine;
using System.Collections;

public class MagicObject : MonoBehaviour {

	public Sprite[] sprites;
	public ParticleSystem stars;

	private Animator animObjet;

	public new void Start() {
		animObjet = this.GetComponent<Animator>();
	}

	private int idCurentObject = 0;


	// int n = 1, 2 ou 3
	// Jouer l'animation d'apparition
	public void ChangeObject(int n) {
		idCurentObject = n;
		int iInTab = n-1;
		changeSprite(iInTab);
		animObjet.SetTrigger("reset");
		animObjet.ResetTrigger("down");
		//animObjet.ResetTrigger ("up");
	}

	// Jouer animation pour transformer, puis le faire disparaitre peu après
	// Pour la disparition, jouer avec la transparence du SpriteRender ?
	public void Transformer(bool isGood) {
		int iNTab = 6;
		if (isGood) {
			iNTab = idCurentObject + 2;
		}
		changeSprite(iNTab);
		stars.Play();
	}

	private void changeSprite(int iTab) {
		Sprite sp;
		sp = sprites[iTab];
		this.GetComponent<SpriteRenderer>().sprite = sp;
	}
}
