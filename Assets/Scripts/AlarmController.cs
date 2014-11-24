using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlarmController : MonoBehaviour {

	public delegate void AlarmMode(bool alarmOn);
	public static event AlarmMode onAlarm;

	public Vector3 lastPlayerPosition;
	public Vector3 playerResetPosition = new Vector3(5000f, 5000f, 5000f);

	public float timer = 10f;
	private float timerCount;

	private List<int> detectedCount;

	void Awake() {
		lastPlayerPosition = playerResetPosition;
		detectedCount = new List<int>();
	}

	void Update() {

	}


	public void playerDetected(Vector3 playerPosition, int id) {
		if(!detectedCount.Contains(id)) {
			detectedCount.Add(id);
			Debug.Log ("Adds " + id + " size: " + detectedCount.Count);
		}
		lastPlayerPosition = playerPosition;
	}

	public void playerLostSight(int id) {
		detectedCount.Remove(id);
		Debug.Log ("Removes " + id + " size: " + detectedCount.Count);
		if(detectedCount.Count < 1) {
			StartCoroutine ("AlarmCooldown");
		}
	}

	IEnumerator AlarmCooldown() {
		yield return new WaitForSeconds(3f);
		lastPlayerPosition = playerResetPosition;
		StopCoroutine("AlarmCooldown");
	}
}
