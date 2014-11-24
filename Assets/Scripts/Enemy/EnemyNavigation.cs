using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyNavigation : MonoBehaviour {

	private Vector3 target;
	private Vector3 playerPosition;

	public Transform path;
	private Transform[] pathList;
	private int currentNodeInPath;
	private AlarmController alarmController;

	private NavMeshAgent nav;

	void Awake () {
		nav = GetComponent<NavMeshAgent>();
		alarmController = GameObject.FindGameObjectWithTag("GameController").GetComponent<AlarmController>();
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

		if(path != null) {
			List<Transform> tmp = new List<Transform>();
			foreach(Transform pathNode in path) {
				tmp.Add(pathNode);
			}
			pathList = tmp.ToArray();
		} else {
			Debug.Log ("Path for enemy is not set");
		}
	}

	void Update () {
		if(alarmController.lastPlayerPosition == alarmController.playerResetPosition) {
			followPath();
			Debug.Log("BYTER");
		} else {
			chasePlayerAtLastLocation();
		}
		nav.SetDestination(target);
	}

	void chasePlayerAtLastLocation() {
		nav.stoppingDistance = 4f;
		nav.speed = 5f;
		//target = alarmController.lastPlayerPosition;
		target = playerPosition;
	}

	void followPath() {
		nav.stoppingDistance = 0f;
		nav.speed = 2f;
		if(nav.remainingDistance < 0.7f) {
			currentNodeInPath = (currentNodeInPath + 1) % pathList.Length;
		}
		target = pathList[currentNodeInPath].position;
	}
}
