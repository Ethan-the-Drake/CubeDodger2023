using UnityEngine;
using System.Collections;

public class progressive_cubeDeleteScript : MonoBehaviour
{
	ProgressiveSceneHandlerScript scenehandlescript;
	settingsScript settingsScript;
	GameObject masterLight;
	public Rigidbody cubeRB;
	//GameObject gameCamera;
	//Renderer cubeRend;
	//SpriteRenderer spriteRend;

	//Vector3 cubePausedVelocity;
	//Vector3 cubePausedAngularVelocity;

	public bool isReferenceCube;// This script is attached to reference cubes for optimization so that the game doesn't have to add the script to every cube that's instantiated, but shouldn't run on those cubes.
	public bool isStaticCube;// Enable this if you don't want a cube does not have a sprite child object (like in the case of static cubes)
	public bool isBallCube;
	// Use this for initialization
	void Start ()
	{
		//try {
		//	settingsScript = GameObject.Find ("settingsHolder").GetComponent<settingsScript> ();
		//} catch {
		//	Destroy (gameObject);
		//	return;
		//}

		if (isReferenceCube) {
			return;
		}
		scenehandlescript = GameObject.Find ("SceneHandler").GetComponent<ProgressiveSceneHandlerScript>();

		masterLight = GameObject.Find ("masterLight");
		//gameCamera = GameObject.Find ("cameraREF");
		//cubeRend = gameObject.GetComponent<Renderer> ();

		settingsScript = GameObject.Find ("settingsHolder").GetComponent<settingsScript> ();
		//cubeRend = gameObject.GetComponent<Renderer> ();
		//spriteRend = gameObject.GetComponentInChildren<SpriteRenderer> ();
		//cubeRend.enabled = false;
		//if (spriteRend != null) {
		//	spriteRend.enabled = true;
		//}
		cubeRB = gameObject.GetComponent<Rigidbody>();
		if ((this.transform.position.x > 4999 || this.transform.position.x < -4999) || scenehandlescript.score < 0 && gameObject.transform.position.z < 30 || scenehandlescript.turnOffCubes)
		{
			Destroy (gameObject);
		}
		else if (cubeRB != null) {
			cubeRB.useGravity = true;
		}
		if (isBallCube) {
			cubeRB.freezeRotation = false;
			if (settingsScript.modifierAirdrop == false) {
				cubeRB.velocity = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (0, 10), Random.Range (-5, 5));
			} else if (!Application.isMobilePlatform && !settingsScript.simulateMobileOptimization){
				cubeRB.velocity = new Vector3(Random.Range(-5,5), 0, 0);
			}
		}
		InvokeRepeating ("cleanup", 0, 1);
	}

	void cleanup(){
		//Debug.Log ("this position " + this.transform.position + " playerPos + 10 " + scenehandlescriptplayerPos.z + 10);
		if ((masterLight.transform.localEulerAngles.x < 15) && (masterLight.transform.localEulerAngles.y == 0))
		{//If the sun is at certain angles where shadows are still being drawn for this object, wait longer to delete it so that shadows behind you don't disappear
			if (scenehandlescript.playerPos.z - 100 > this.transform.position.z)
			{
				if (isStaticCube && gameObject.activeSelf) {
					gameObject.SetActive (false);
				} else if (!isStaticCube) {
					Destroy (gameObject);
				}

			}
		}
		else
		{
			if (scenehandlescript.playerPos.z - 20 > this.transform.position.z)
			{				
				if (isStaticCube && gameObject.activeSelf) {
					gameObject.SetActive (false);
				} else if (!isStaticCube) {
					Destroy (gameObject);
				}
			}
		}

		/*//Debug.Log(Vector3.Distance (gameCamera.transform.position, gameObject.transform.position));
		if (Vector3.Distance (gameCamera.transform.position, gameObject.transform.position) > settingsScript.cubeDrawDistance && !isStaticCube) {
			if (cubeRend.enabled) {
				cubeRend.enabled = false;
				if (spriteRend != null) {
					spriteRend.enabled = true;
				}
			}
		} else {
			if (!cubeRend.enabled) {
				cubeRend.enabled = true;
				if (spriteRend != null) {
					spriteRend.enabled = false;
				}
			}
		}*/
	}


	// Update is called once per frame
	/*void Update ()
	{

		if (cubeRB != null)
		{
			if (scenehandlescript.gamePaused)
			{
				if (cubePausedVelocity == Vector3.zero)
				{
					cubePausedVelocity = cubeRB.velocity;
					cubePausedAngularVelocity = cubeRB.angularVelocity;
					cubeRB.useGravity = false;
					cubeRB.velocity = Vector3.zero;
					cubeRB.angularVelocity = Vector3.zero;

				}
				if (cubeRB.velocity != Vector3.zero)
				{
					cubeRB.velocity = Vector3.zero;
				}

			}
			else
			{
				if (cubePausedVelocity != Vector3.zero)
				{
					cubeRB.velocity = cubePausedVelocity;
					cubeRB.angularVelocity = cubePausedAngularVelocity;
					cubeRB.useGravity = true;
					cubePausedVelocity = Vector3.zero;
					cubePausedAngularVelocity = Vector3.zero;
				}
				if (cubeRB.velocity == Vector3.zero && gameObject.transform.position.y <= 1)
				{
					Destroy(cubeRB);
				}
			}
		}
	}*/
}

