using UnityEngine;
using System.Collections;

public class trtailer_cameraScript2 : MonoBehaviour {

	public GameObject focusCube;
	float ZPos = -264f;
	float YPos = 345f;
	public float YVel = 0;
	public float ZVel = -30;

	float timer;

	// Use this for initialization
	void Start () {

		Screen.SetResolution (1600, 900, true);
		Application.targetFrameRate = 30;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (timer < 5) {
			timer = timer + Time.deltaTime;
			return;
		}
		YPos = YPos + YVel * Time.deltaTime;
		ZPos = ZPos + ZVel * Time.deltaTime;

		transform.position = new Vector3 (focusCube.transform.position.x, YPos, ZPos);
	}
}
