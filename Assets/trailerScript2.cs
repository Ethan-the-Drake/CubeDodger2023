using UnityEngine;
using System.Collections;

public class trailerScript2 : MonoBehaviour {

	public GameObject focusCube;
	float ZPos = -0.8f;
	float YPos;
	public float YVel = -20;
	public float ZVel = -1;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate () {



		/*if (focusCube.transform.position.y > 8) {
			YPos = focusCube.transform.position.y;
			YVel = focusCube.GetComponent<Rigidbody> ().velocity.y;
			if (focusCube.transform.position.y < 85) {
				ZPos = ZPos - 1 * Time.deltaTime;
			}
		} else if (YVel < 0) {
			YVel = YVel + 125f * Time.deltaTime;
			ZVel = ZVel - 50f * Time.deltaTime;
			YPos = YPos + YVel * Time.deltaTime;
			ZPos = ZPos + ZVel * Time.deltaTime;
		} else {
			YPos = YPos + YVel * Time.deltaTime;
			ZPos = ZPos + ZVel * Time.deltaTime;
		}*/


		transform.position = new Vector3 (focusCube.transform.position.x, YPos, ZPos);
	}
}
