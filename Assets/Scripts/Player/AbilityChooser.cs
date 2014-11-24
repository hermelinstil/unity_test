using UnityEngine;
using System.Collections;

public class AbilityChooser : MonoBehaviour {

	private string[] abilities;
	private int currentAbility;

	private GUIText gui;

	void Awake() {
		abilities = new string[5];
		gui = GameObject.Find ("HUD").GetComponent<GUIText>();

		for(int i = 0; i < abilities.Length; ++i) {
			abilities[0] = "Ability " + (i + 1);
		}
	}

	void Update() {
		if(Input.GetAxis("Mouse ScrollWheel") > 0) {
			currentAbility = (currentAbility + 1) % abilities.Length;
			gui.text = abilities[currentAbility];
		} else {
			currentAbility = (currentAbility + 1) % abilities.Length;
		}
	}
}
