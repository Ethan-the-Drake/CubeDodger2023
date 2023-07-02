using UnityEngine;
using System.Collections;

public class powerupJumpScript : MonoBehaviour {

    // Use this for initialization

    SceneHandlerScript sceneScript;
    GameObject player;
    public float distanceToPlayer;

	void Start ()
    {
        sceneScript = GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>();
        player = GameObject.Find("playerREF");
	}

    // Update is called once per frame
    void Update()
    {
       distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distanceToPlayer <= 3)
        {
            sceneScript.currentPowerup = 1;
            GameObject.Find("powerupSFX").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
    
}
