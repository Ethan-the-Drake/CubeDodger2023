using UnityEngine;
using System.Collections;

public class blackHoleOrbitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (0, 0, 60 * Time.deltaTime);
	
	}
}
