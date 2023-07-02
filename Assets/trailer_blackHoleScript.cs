using UnityEngine;
using System.Collections;

public class trailer_blackHoleScript : MonoBehaviour {

	public GameObject camera;

	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (camera.transform.position);
		gameObject.transform.Rotate (new Vector3 (0, 180, 0));

	}
}
