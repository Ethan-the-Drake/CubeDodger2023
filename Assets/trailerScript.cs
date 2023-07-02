using UnityEngine;
using System.Collections;

public class trailerScript : MonoBehaviour {

	GameObject redCube;
	GameObject blueCube;
	GameObject greenCube;

	float ZSpawn;
	float YSpawnGen = 0;
	int YSpawnMin = 45;
	int YSpawnMax = 55;
	int XSpawnMin = -150;
	int XSpawnMax = 150;
	int rand;
	GameObject newCube;
	int TimeDividend = 16;

	// Use this for initialization
	void Start () {

		Screen.SetResolution (1600, 900, true);
		Application.targetFrameRate = 30;

		redCube = GameObject.Find ("RedREF");
		greenCube = GameObject.Find ("GreenREF");
		blueCube = GameObject.Find ("BlueREF");
	
		generateCubes ();

		Time.timeScale = 1f/TimeDividend;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	
	}

	void generateCubes(){
		ZSpawn = -50;

		while (ZSpawn < 250) {
			rand = Random.Range (1, 4);
			if (rand == 1) {
				newCube = Instantiate (redCube);
			} else if (rand == 2) {
				newCube = Instantiate (greenCube);
			} else if (rand == 3) {
				newCube = Instantiate (blueCube);
			}
			newCube.transform.position = new Vector3 (Random.Range (XSpawnMin, XSpawnMax), YSpawnGen + Random.Range(YSpawnMin, YSpawnMax), ZSpawn);
			if (newCube.transform.position.y < 0.5f) {
				newCube.transform.position = new Vector3 (newCube.transform.position.x, 0.5f, newCube.transform.position.z);
			}
			if (newCube.transform.position.x >= -1.5f && newCube.transform.position.x <= 1.5f && newCube.transform.position.z >= -3f && newCube.transform.position.z <= 3f) {
				Destroy (newCube);
			}
			ZSpawn = ZSpawn + 0.05f;
			YSpawnGen = YSpawnGen + 0.05f;
		}
	}
}
