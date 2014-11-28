using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AbilityChooser : MonoBehaviour {

	private List<Ability> abilities;
	private int currentAbility;

	private GUIText gui;

	void Awake() {
		abilities = new List<Ability>();
		gui = GameObject.Find ("HUD").GetComponent<GUIText>();

		//hur fan gör man...
		abilities.Add(new SeeThroughAbility(GameObject.Find ("SecondaryCamera").camera));
		abilities.Add(new InvisibilityAbility());
	}

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			abilities[currentAbility].execute();
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0) {
			currentAbility = (currentAbility + 1) % abilities.Count;
		} else if(Input.GetAxis("Mouse ScrollWheel") < 0) {
			if(currentAbility != 0) {
				currentAbility = (currentAbility - 1) % abilities.Count;
			}
		}
		gui.text = abilities[currentAbility].ToString();
	}
}
