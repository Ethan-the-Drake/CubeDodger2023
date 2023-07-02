using UnityEngine;
using System.Collections;

public class playerTrailerScript : MonoBehaviour {

	float velocity = 35;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (0, 0, transform.position.z + velocity * Time.deltaTime);
	}
}
