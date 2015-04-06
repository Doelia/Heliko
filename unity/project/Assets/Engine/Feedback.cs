using UnityEngine;
using System.Collections;

public abstract class Feedback : HelikoObject, PlayerActionReceiver {

	public new void Start () {
		base.Start();
		GetPlayerActions().Connect(this);
	}

	abstract public void SetReaction(bool isGood);

	private Coroutine inProgress = null;
	
	public void OnFailure() {
		if (inProgress != null) {
			StopCoroutine(inProgress);
			inProgress = null;
		}
		inProgress = StartCoroutine(AnimPasContent());
	}
	
	public void OnSuccess() {
		if (inProgress != null) {
			StopCoroutine(inProgress);
			inProgress = null;
		}
		this.SetReaction(true);
	}

	public void OnSuccessLoop() {}
	
	IEnumerator AnimPasContent() {
		this.SetReaction(false);
		yield return new WaitForSeconds(0.66f);
		this.SetReaction(true);
	}
	

}
