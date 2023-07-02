using UnityEngine;
using System.Collections;

public class circusLightsHolderScript : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerREF");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
	}
}
