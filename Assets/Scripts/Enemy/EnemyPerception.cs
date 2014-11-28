using UnityEngine;
using System.Collections;

public class EnemyPerception : MonoBehaviour {

	public float fieldOfView = 100f;
	public bool playerInSight;

	public Vector3 lastPlayerPosition;
	public Vector3 previousSighting;

	private SphereCollider perceptionCollider;
	private AlarmController alarmController;
	private GameObject player;


	void Awake() {
		perceptionCollider = GetComponent<SphereCollider>();
		alarmController = GameObject.FindGameObjectWithTag("GameController").GetComponent<AlarmController>();
		player = GameObject.FindGameObjectWithTag("Player");

		lastPlayerPosition = alarmController.lastPlayerPosition;
		previousSighting = alarmController.playerResetPosition;
	}

	void Update() {
		if(alarmController.lastPlayerPosition != previousSighting) {
			lastPlayerPosition = alarmController.lastPlayerPosition;
		}

		previousSighting = alarmController.lastPlayerPosition;

		//if(renderer.isVisible) {
		//	renderer.material.color = Color.red;
		//}
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject == player) {

			playerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfView / 2) {

				RaycastHit hit;

				if(Physics.Raycast(transform.position, direction.normalized, out hit, perceptionCollider.radius * 3)) {

					if(hit.collider.gameObject == player) {
						playerInSight = true;

							Quaternion lookRotation = Quaternion.LookRotation(direction);
							transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);

						alarmController.playerDetected(player.transform.position, GetHashCode());
						shoot();
					} else {
						OnTriggerExit(player.collider);
					}
				}
			}
		}
	}

	void shoot() {
		Debug.DrawLine(transform.position, player.transform.position, Color.red, 2f);
	}

	void OnTriggerExit (Collider other) {
		if(other.gameObject == player) {
			playerInSight = false;
			alarmController.playerLostSight(GetHashCode());
		}
	}
}
