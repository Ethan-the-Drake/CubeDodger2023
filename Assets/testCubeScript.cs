using UnityEngine;
using System.Collections;

public class testCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (GetComponent<Rigidbody>().velocity.z < 25)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,25);
		}
	
	}
}
