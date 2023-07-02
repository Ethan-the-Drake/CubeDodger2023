using UnityEngine;
using System.Collections;

public class cubeMenuDeleteScript : MonoBehaviour
{
	GameObject player;
	//menuHandlerScript menuhandlescript;
	settingsScript settingsScript;

	//GameObject gameCamera;
	//Renderer cubeRend;
	//SpriteRenderer spriteRend;
	//spriteBillboard bill;
	Rigidbody cubeRB;
	public bool isReferenceCube;// This script is attached to reference cubes for optimization so that the game doesn't have to add the script to every cube that's instantiated, but shouldn't run on those cubes.
	//public bool isStaticCube;// Static cubes, such as cube cutters, should omit parts of this script's code. For instance, cube cutter cubes should not be deleted.
	public bool isBallCube;

	void Start ()
	{
		if (isReferenceCube) {
			return;
		}

		//menuhandlescript = GameObject.Find ("menuHandler").GetComponent<menuHandlerScript>();
		player = GameObject.Find("playerREF");
		//gameCamera = GameObject.Find ("cameraREF");
		settingsScript = GameObject.Find ("settingsHolder").GetComponent<settingsScript> ();
		//cubeRend = gameObject.GetComponent<Renderer> ();
		//spriteRend = gameObject.GetComponentInChildren<SpriteRenderer> ();
		//bill = GetComponentInChildren<spriteBillboard> ();
		//cubeRend.enabled = false;
		//spriteRend.enabled = true;
		cubeRB = gameObject.GetComponent<Rigidbody>();
		if (cubeRB != null)
		{
			cubeRB.useGravity = true;
			if (isBallCube && (!Application.isMobilePlatform && !settingsScript.simulateMobileOptimization))
			{
				cubeRB.freezeRotation = false;
				cubeRB.velocity = new Vector3(Random.Range(-2,0), Random.Range(0,10), Random.Range(-5,5));

			}
		}
		if (!IsInvoking("update1s")) {
			InvokeRepeating ("Update1s", 0, 1);
		}
	}

	void Update1s(){
		if (player.transform.position.z - 300 > this.transform.position.z || gameObject.transform.position.z > 5000 || gameObject.transform.position.z < -5000 || gameObject.transform.position.y < -5)
		{

			Destroy (gameObject);
		}

		/*if (cubeRB != null)
		{
			if (cubeRB.velocity == Vector3.zero && transform.position.y <= 1)
			{
				Destroy(cubeRB);
			}
		}*/

		/*if (Vector3.Distance (gameCamera.transform.position, gameObject.transform.position) > settingsScript.cubeDrawDistance) {
			if (cubeRend.enabled) {
				cubeRend.enabled = false;
				spriteRend.enabled = true;
				bill.updateDir ();
			}
		} else {
			if (!cubeRend.enabled) {
				cubeRend.enabled = true;
				spriteRend.enabled = false;
			}
		}*/
	}

	/*void FixedUpdate(){
		//Physics
		if (transform.position.y > 0) {
			if (transform.position.y + cubeVelocity.y > 0) {
				cubeVelocity = new Vector3 (cubeVelocity.x, cubeVelocity.y + Physics.gravity.y, cubeVelocity.z);
				transform.Translate (cubeVelocity);
			} else {
				cubeVelocity = new Vector3 (cubeVelocity.x, 0, cubeVelocity.z);
				transform.Translate (cubeVelocity.x, 0, cubeVelocity.z);
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
			}
		} else {
			cubeVelocity = new Vector3 (cubeVelocity.x, 0, cubeVelocity.z);
			transform.Translate (cubeVelocity.x, 0, cubeVelocity.z);
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		}
	}*/
}

