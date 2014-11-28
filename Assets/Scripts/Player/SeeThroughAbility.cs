using UnityEngine;
using System.Collections;

public class SeeThroughAbility : Ability {


	private Camera toggleCamera;

	public SeeThroughAbility(Camera camera) {
		toggleCamera = camera;
		toggleCamera.enabled = false;
	}

	public void execute() {
		toggleCamera.enabled = !toggleCamera.enabled;
	}
}
