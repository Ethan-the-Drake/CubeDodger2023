using UnityEngine;
using System.Collections;

public class trailer_cubeScript : MonoBehaviour {

	GameObject player;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerREF");
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.z >= -50 && !rb.useGravity) {
			rb.useGravity = true;
		}
	}
}
