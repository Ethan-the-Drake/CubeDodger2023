using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {

	AchievementsScript achievements;

    	float destroyTimer;
    	bool initiateDestroy;
    	float distanceToPlayer;

	ProgressiveSceneHandlerScript progressiveSceneHandle;
	SceneHandlerScript SceneHandle;
	int rand;

    	GameObject player;

	GameObject redExplosion;
	GameObject greenExplosion;
	GameObject blueExplosion;

	GameObject codeFragment1;
	GameObject codeFragment2;
	GameObject codeFragment3;
	GameObject codeFragment4;
	GameObject codeFragment5;

	GameObject newExplosion;

	// Use this for initialization
	void Start () {
		try {
			progressiveSceneHandle = GameObject.Find("SceneHandler").GetComponent<ProgressiveSceneHandlerScript>();

			codeFragment1 = GameObject.Find ("referenceCodeFragment1");
			codeFragment2 = GameObject.Find ("referenceCodeFragment2");
			codeFragment3 = GameObject.Find ("referenceCodeFragment3");
			codeFragment4 = GameObject.Find ("referenceCodeFragment4");
			codeFragment5 = GameObject.Find ("referenceCodeFragment5");
		} catch {
			progressiveSceneHandle = null;
		}

		try {
			SceneHandle = GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>();
		} catch {
			SceneHandle = null;
		}

		achievements = GameObject.Find ("settingsHolder").GetComponent<AchievementsScript> ();

        	player = GameObject.Find("playerREF");

		redExplosion = GameObject.Find ("referenceExplosionRed");
		greenExplosion = GameObject.Find ("referenceExplosionGreen");
		blueExplosion = GameObject.Find ("referenceExplosionBlue");
	}
	
	// Update is called once per frame
	void Update ()
    	{
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

		if (gameObject.GetComponent<Rigidbody> ().velocity.z < 100 && !initiateDestroy) {
			if ((SceneHandle != null && SceneHandle.gamePaused) || progressiveSceneHandle != null && progressiveSceneHandle.gamePaused) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			} else {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 100);
			}
            
		}

		if (distanceToPlayer >= 1000) {
			Destroy (gameObject);
		}

        if (initiateDestroy)
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.z != 0)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            if (destroyTimer < 1)
            {
                destroyTimer += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
	}

	void OnCollisionEnter(Collision collision) {
		if (!initiateDestroy) {
			if (collision.gameObject.GetComponent<Renderer> ().material.name == "cube_red (Instance)" || collision.gameObject.GetComponent<Renderer> ().material.name == "cube_green (Instance)" || collision.gameObject.GetComponent<Renderer> ().material.name == "cube_blue (Instance)") {
				if (progressiveSceneHandle != null) {
					if (progressiveSceneHandle.currentWorld == 1) {
						rand = Random.Range (1, 100);
						if (rand <= 25) {
							newExplosion = Instantiate (codeFragment1);
						} else if (rand > 25 && rand <= 50) {
							newExplosion = Instantiate (codeFragment2);
						} else if (rand > 50 && rand <= 75) {
							newExplosion = Instantiate (codeFragment3);
						} else if (rand > 75 && rand <= 95) {
							newExplosion = Instantiate (codeFragment4);
						} else if (rand > 95 && rand <= 100) {
							newExplosion = Instantiate (codeFragment5);
						}
					} else {
						if (collision.gameObject.GetComponent<Renderer> ().material.name == "cube_red (Instance)") {
							newExplosion = Instantiate (redExplosion);
						} else if (collision.gameObject.GetComponent<Renderer> ().material.name == "cube_green (Instance)") {
							newExplosion = Instantiate (greenExplosion);
						} else if (collision.gameObject.GetComponent<Renderer> ().material.name == "cube_blue (Instance)") {
							newExplosion = Instantiate (blueExplosion);
						}
					}
				} else if (collision.gameObject.GetComponent<Renderer> ().material.name == "cube_red (Instance)") {
					newExplosion = Instantiate (redExplosion);
				} else if (collision.gameObject.GetComponent<Renderer> ().material.name == "cube_green (Instance)") {
					newExplosion = Instantiate (greenExplosion);
				} else if (collision.gameObject.GetComponent<Renderer> ().material.name == "cube_blue (Instance)") {
					newExplosion = Instantiate (blueExplosion);
				}

				newExplosion.transform.position = collision.gameObject.transform.position;
				newExplosion.AddComponent<explosionScript> ();
				Destroy (collision.gameObject);
				GameObject.Find ("explosionSFX").transform.position = gameObject.transform.position;
				GameObject.Find ("explosionSFX").GetComponent<AudioSource> ().Play ();
				if (distanceToPlayer > 25) {
					gameObject.GetComponent<Renderer> ().enabled = false;
					initiateDestroy = true;
				} else {
					Destroy (gameObject);
				}

				if (!achievements.achShootCube) {
					achievements.achShootCube = true;
					achievements.queueAch (25);
					achievements.saveAchievements ();
				}
			} else {
				Destroy (gameObject);
			}

		}
    	}
}