using UnityEngine;
using System.Collections;

public class explosionScript : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerREF");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.z < player.transform.position.z - 10) {
			Destroy (gameObject);
		}
	
	}
}
