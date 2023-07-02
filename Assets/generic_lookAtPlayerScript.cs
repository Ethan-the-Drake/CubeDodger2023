using UnityEngine;
using System.Collections;

public class generic_lookAtPlayerScript : MonoBehaviour {

	GameObject player;
	Light light;

	Quaternion playerLookAt;
	float timer;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerREF");
		light = gameObject.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (light.enabled) {
			if (timer <= 2 && player.transform.position.z < 4950 && player.transform.position.z > -4950) {
				playerLookAt = Quaternion.LookRotation (player.transform.position - gameObject.transform.position);
				gameObject.transform.rotation = Quaternion.Slerp (gameObject.transform.rotation, playerLookAt, Time.deltaTime * 1f);
				timer = timer + 1 * Time.deltaTime;
			} else if (timer > 2) {
				gameObject.transform.LookAt (player.transform.position);
			}

		}

		if (Vector3.Distance (gameObject.transform.position, player.transform.position) >= 7500) {
			if (light.enabled) {
				light.enabled = false;
			}
		} else if (!light.enabled) {
			light.enabled = true;
		}
	}
}
