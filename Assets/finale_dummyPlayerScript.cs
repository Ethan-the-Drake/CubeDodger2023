using UnityEngine;
using System.Collections;

public class finale_dummyPlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.eulerAngles = new Vector3 (transform.eulerAngles.x + 5, transform.eulerAngles.y + 5, transform.eulerAngles.z + 5);
	
	}
}
