using UnityEngine;
using System.Collections;

public class CameraDetection : MonoBehaviour {

	private GameObject player;
	private AlarmController alarmController;
	private Light cameraLight;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		alarmController = GameObject.FindGameObjectWithTag("GameController").GetComponent<AlarmController>();
		cameraLight = transform.parent.GetComponent<Light>();

		AlarmController.onAlarm += onAlarm;
	}

	void onAlarm(bool alarm) {
		if(alarm) {
			cameraLight.color = Color.red;
		} else {
			cameraLight.color = Color.green;
		}
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject == player) {

			Vector3 playerDir = player.transform.position - transform.position;
			RaycastHit hit;

			if(Physics.Raycast(transform.position, playerDir, out hit)) {
				if(hit.collider.gameObject == player) {
					alarmController.playerDetected(player.transform.position, GetHashCode());
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject == player) {
			alarmController.playerLostSight(GetHashCode());
		}
	}
}
