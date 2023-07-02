using UnityEngine;
using System.Collections;

public class testbedScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update (){
		if (GameObject.Find ("cameraREF").GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity < 0.95) {
			GameObject.Find ("cameraREF").GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity = GameObject.Find ("cameraREF").GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity + 0.125f * Time.deltaTime;
		} else {
			GameObject.Find ("cameraREF").GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity = 1;
		}
	}
}
