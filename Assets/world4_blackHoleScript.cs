using UnityEngine;
using System.Collections;

public class world4_blackHoleScript : MonoBehaviour {

	GameObject player;
	GameObject cameraREF;

	public float playerDistance;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("playerREF");
		cameraREF = GameObject.Find ("cameraREF");
	}
	
	// Update is called once per frame
	void Update () {
		playerDistance = Vector3.Distance (player.transform.position, transform.position);
		transform.position = new Vector3 (player.transform.position.x, (playerDistance/5), transform.position.z);

		if (playerDistance < 250) {
			cameraREF.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().enabled = true;
			cameraREF.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = -5f;
		}
		else if (playerDistance < 10000) {
			if (cameraREF.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().enabled == false) {
				cameraREF.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().enabled = true;
			}
			if (250 - playerDistance / 8 >= 0) {
				cameraREF.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().chromaticAberration = 250 - (playerDistance / 8);
			}
			if (250 - playerDistance / 8 >= 60 && 250 - playerDistance / 8 <= 120) {
				cameraREF.GetComponent<Camera> ().fieldOfView = 250 - (playerDistance/8);
			}
		}
	}
}
