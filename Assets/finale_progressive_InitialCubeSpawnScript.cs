using UnityEngine;
using System.Collections;

public class finale_progressive_InitialCubeSpawnScript : MonoBehaviour {

	Rigidbody rb;
	finale_ProgressiveSceneHandlerScript sceneHandler; 
	settingsScript settingsHolder; 

	// Use this for initialization
	void Start () 
	{
		rb = this.GetComponent<Rigidbody> ();
		sceneHandler = GameObject.Find ("SceneHandler").GetComponent<finale_ProgressiveSceneHandlerScript> ();
		try {
			settingsHolder = GameObject.Find ("settingsHolder").GetComponent<settingsScript> ();
		} catch {
			Destroy (gameObject);
		}

		gameObject.transform.position = new Vector3 (0, 0, 150);
	
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
