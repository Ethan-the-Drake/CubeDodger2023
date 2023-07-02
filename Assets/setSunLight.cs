using UnityEngine;
using System.Collections;

public class setSunLight : MonoBehaviour {

	
	//Material sky;
	
	public Transform stars;
	//public Transform camera;
	//public Transform worldProbe;
	
	// Use this for initialization
	void Start () 
	{
		
		//sky = RenderSettings.skybox;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.position = camera.transform.position;
		//transform.position.z
		stars.transform.rotation = transform.rotation;
		
		/*Vector3 tvec = Camera.main.transform.position;
		worldProbe.transform.position = tvec;*/

		
	}
}