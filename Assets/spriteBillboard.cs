using UnityEngine;
using System.Collections;

public class spriteBillboard : MonoBehaviour {

	GameObject gameCamera;
	//Vector3 dir;

	// Use this for initialization
	void Start () {
		gameCamera = GameObject.Find ("cameraREF");

		updateDir (true);
	}

	public void updateDir (bool predictVector = false) {
		if (predictVector) {
			if (Application.loadedLevelName == "menu" || Application.loadedLevelName == "credits_menu") {
				transform.forward = Vector3.left;
			} else {
				transform.forward = Vector3.forward;
			}
		} else {
			transform.forward = -gameCamera.transform.forward;
		}
	}
}
