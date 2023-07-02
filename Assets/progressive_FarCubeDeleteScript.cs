using UnityEngine;
using System.Collections;

public class progressive_FarCubeDeleteScript : MonoBehaviour
{
	ProgressiveSceneHandlerScript sceneHandle;

	// Use this for initialization
	void Start ()
	{
		sceneHandle = GameObject.Find ("SceneHandler").GetComponent<ProgressiveSceneHandlerScript> ();
		if (sceneHandle.showNumberOfActiveCubes == true)
		{
			sceneHandle.numberOfActiveCubes = sceneHandle.numberOfActiveCubes + 1;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("this position " + this.transform.position + " playerPos + 10 " + sceneHandle.playerPos.z + 10);
		if ((GameObject.Find ("masterLight").transform.localEulerAngles.x < 15) && (GameObject.Find ("masterLight").transform.localEulerAngles.y == 0))
		{
			if (sceneHandle.playerPos.z - 100 > this.transform.position.z)
			{
				//Debug.Log ("Deleting at player.z - 100");
				if (sceneHandle.showNumberOfActiveCubes == true)
				{
					sceneHandle.numberOfActiveCubes = sceneHandle.numberOfActiveCubes - 1;
				}
				Destroy (gameObject);
			}
		}
		else
		{
			if (sceneHandle.playerPos.z - 20 > this.transform.position.z)
			{
				//Debug.Log ("Deleting at player.z - 20");
				if (sceneHandle.showNumberOfActiveCubes == true)
				{
					sceneHandle.numberOfActiveCubes = sceneHandle.numberOfActiveCubes - 1;
				}				
				Destroy (gameObject);
			}
		}
	}
}