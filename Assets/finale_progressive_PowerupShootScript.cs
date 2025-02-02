using UnityEngine;
using System.Collections;

public class finale_progressive_PowerupShootScript : MonoBehaviour {

    // Use this for initialization

    finale_ProgressiveSceneHandlerScript sceneScript;
    GameObject player;
    public float distanceToPlayer;

    void Start()
    {
        sceneScript = GameObject.Find("SceneHandler").GetComponent<finale_ProgressiveSceneHandlerScript>();
        player = GameObject.Find("playerREF");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distanceToPlayer <= 3)
        {
            sceneScript.currentPowerup = 3;
            GameObject.Find("powerupSFX").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }

}
