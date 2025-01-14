using UnityEngine;
using System.Collections;

public class finale_projectileScript : MonoBehaviour {

	AchievementsScript achievements;

    	float destroyTimer;
    	bool initiateDestroy;
    	float distanceToPlayer;

	finale_ProgressiveSceneHandlerScript progressiveSceneHandle;
	SceneHandlerScript SceneHandle;
	int rand;

    	GameObject player;

	GameObject whiteExplosion;

	GameObject newExplosion;

	// Use this for initialization
	void Start () {
		try {
			progressiveSceneHandle = GameObject.Find("SceneHandler").GetComponent<finale_ProgressiveSceneHandlerScript>();
		} catch {
			progressiveSceneHandle = null;
		}

		achievements = GameObject.Find ("settingsHolder").GetComponent<AchievementsScript> ();

        	player = GameObject.Find("playerREF");

		whiteExplosion = GameObject.Find ("referenceExplosionWhite");
	}
	
	// Update is called once per frame
	void Update ()
	{
		distanceToPlayer = Vector3.Distance (gameObject.transform.position, player.transform.position);

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

		if (initiateDestroy) {
			if (gameObject.GetComponent<Rigidbody> ().velocity.z != 0) {
				gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}

			if (destroyTimer < 1) {
				destroyTimer += Time.deltaTime;
			} else {
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (!initiateDestroy) {
			if (progressiveSceneHandle != null) {
				newExplosion = Instantiate (whiteExplosion);
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

		}
	}
}