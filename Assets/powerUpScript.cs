using UnityEngine;
using System.Collections;

public class powerUpScript : MonoBehaviour
{

    SceneHandlerScript scenehandlescript;
    settingsScript settingsScript;
    GameObject masterLight;
    public Rigidbody cubeRB;

    Vector3 cubePausedVelocity;
    Vector3 cubePausedAngularVelocity;

    GameObject player;
    // Use this for initialization
    void Start()
    {
        scenehandlescript = GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>();
        settingsScript = GameObject.Find("settingsHolder").GetComponent<settingsScript>();
        masterLight = GameObject.Find("masterLight");
        player = GameObject.Find("player");


        if ((this.transform.position.x > 4999 || this.transform.position.x < -4999) || scenehandlescript.score < 0 && gameObject.transform.position.z < 30)
        {
            Destroy(gameObject);
        }
        else
        {
            if (settingsScript.modifierAirdrop)
            {
                gameObject.AddComponent<Rigidbody>();
                cubeRB = gameObject.GetComponent<Rigidbody>();
                cubeRB.freezeRotation = true;
            }

            if (scenehandlescript.showNumberOfActiveCubes == true)
            {
                Debug.Log("spawning power up");
                scenehandlescript.numberOfActiveCubes = scenehandlescript.numberOfActiveCubes + 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log ("this position " + this.transform.position + " playerPos + 10 " + scenehandlescriptplayerPos.z + 10);
        if ((masterLight.transform.localEulerAngles.x < 15) && (masterLight.transform.localEulerAngles.y == 0))
        {
            if (scenehandlescript.playerPos.z - 100 > this.transform.position.z)
            {
                //Debug.Log ("Deleting at player.z - 100");
                if (scenehandlescript.showNumberOfActiveCubes == true)
                {
                    scenehandlescript.numberOfActiveCubes = scenehandlescript.numberOfActiveCubes - 1;
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (scenehandlescript.playerPos.z - 20 > this.transform.position.z)
            {
                //Debug.Log ("Deleting at player.z - 20");
                if (scenehandlescript.showNumberOfActiveCubes == true)
                {
                    scenehandlescript.numberOfActiveCubes = scenehandlescript.numberOfActiveCubes - 1;
                }
                Destroy(gameObject);
            }
        }

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
                    if (gameObject.transform.position.y != 1)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
                    }
                }
            }
        }

        doAnimation();
    }

    void doAnimation()
    {
        if (!scenehandlescript.gamePaused)
        {
            gameObject.transform.Rotate(0, 1, 0);
        }
    }
}