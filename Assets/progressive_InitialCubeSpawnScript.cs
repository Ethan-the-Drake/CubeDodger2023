using UnityEngine;
using System.Collections;

public class progressive_InitialCubeSpawnScript : MonoBehaviour {

	Rigidbody rb;
	ProgressiveSceneHandlerScript sceneHandler; 
	settingsScript settingsHolder; 

	// Use this for initialization
	void Start () 
	{
		rb = this.GetComponent<Rigidbody> ();
		sceneHandler = GameObject.Find ("SceneHandler").GetComponent<ProgressiveSceneHandlerScript> ();
		try {
			settingsHolder = GameObject.Find ("settingsHolder").GetComponent<settingsScript> ();
		} catch {
			Destroy (gameObject);
		}


		gameObject.transform.position = new Vector3 (0, 40, 0);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (rb.velocity.z < (sceneHandler.difficultyZVelocity*settingsHolder.modifierSpeedMultiplier))
		{
			rb.velocity = new Vector3(0,0,sceneHandler.difficultyZVelocity*settingsHolder.modifierSpeedMultiplier);
		}

		if (gameObject.transform.position.z > sceneHandler.cubeSpawnDistance)
		{
			Destroy(gameObject);
		}
	}
}
