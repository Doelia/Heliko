using UnityEngine;
using System.Collections;

public class WindowOpen : Window {

	new public void Awake() {
		this.right = 1;
		base.Awake();
	}

	protected override void notifyChildren () {
		foreach (TempoReceiver e in this.observers) {
			e.onSuccessWindowEnter();
		}
	}

}
