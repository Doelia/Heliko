using UnityEngine;
using System.Collections;

public abstract class Feedback : HelikoObject, PlayerActionReceiver {

	public new void Start () {
		base.Start();
		getPlayerActions().connect(this);
	}

	abstract public void setReaction(bool isGood);

	private Coroutine inProgress = null;
	
	public void onFailure() {
		if (inProgress != null) {
			StopCoroutine(inProgress);
			inProgress = null;
		}
		inProgress = StartCoroutine(animPasContent());
	}
	
	public void onSuccess() {
		if (inProgress != null) {
			StopCoroutine(inProgress);
			inProgress = null;
		}
		this.setReaction(true);
	}
	
	IEnumerator animPasContent() {
		this.setReaction(false);
		yield return new WaitForSeconds(0.66f);
		this.setReaction(true);
	}
	

}
