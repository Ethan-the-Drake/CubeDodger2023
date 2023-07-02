using UnityEngine;
using System.Collections;

public class staticCubeScript : MonoBehaviour {

	//This script should be attached to any cube that is static (Cube cutters, progressive steps, etc.)
	//This script handles switching between LOD sprite and the 3D cube for static cubes without the normal cube delete scripts.
	//This script works regardless of level, provided that the cube is a static cube and has a child object with a SpriteRenderer Component.

	public settingsScript settings; // Set these two variables in the editor, not at runtime. Loading time increase is drastic with this many static cubes to run at
	public GameObject gameCamera;   // Start();

	SpriteRenderer sRend;
	public spriteBillboard bill;
	Renderer rend;

	// Use this for initialization
	void Start () {

		bill = GetComponentInChildren<spriteBillboard> ();
		sRend = GetComponentInChildren<SpriteRenderer> ();
		rend = GetComponent<Renderer> ();
		//gameCamera = GameObject.Find("cameraREF");
		//settings = GameObject.Find ("settingsHolder").GetComponent<settingsScript>();

		InvokeRepeating ("Update1s", 0, 1);
	}
	
	// Update is called once per second.
	void Update1s () {

		if (!gameObject.activeInHierarchy) {
			return;
		}

		if (Vector3.Distance (gameCamera.transform.position, gameObject.transform.position) > settings.cubeDrawDistance) {
			if (rend.enabled) {
				rend.enabled = false;
				sRend.enabled = true;
				bill.updateDir ();
			}
		} else {
			if (!rend.enabled) {
				rend.enabled = true;
				sRend.enabled = false;
			}
		}
	
	}
}
