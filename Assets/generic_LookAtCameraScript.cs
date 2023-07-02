using UnityEngine;
using System.Collections;

public class generic_LookAtCameraScript : MonoBehaviour {

	GameObject camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("cameraREF");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (camera.transform.position);
		gameObject.transform.Rotate (new Vector3 (0, 180, 0));

	}
}
