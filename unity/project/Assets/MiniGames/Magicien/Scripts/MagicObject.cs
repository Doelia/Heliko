using UnityEngine;
using System.Collections;

public class MagicObject : HelikoObject {

	public Sprite[] sprites;
	public ParticleSystem stars;
	public ParticleSystem apparition;

	public AudioSource sonApparition;

	private Animator animObjet;

	public new void Start() {
		base.Start();
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
		apparition.Play();
		sonApparition.GetComponent<AudioSource>().Play();
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
		animObjet.SetTrigger("fade");
	}

	private void changeSprite(int iTab) {
		Sprite sp;
		sp = sprites[iTab];
		this.GetComponent<SpriteRenderer>().sprite = sp;
		animObjet.ResetTrigger("fade");
		
	}
}
