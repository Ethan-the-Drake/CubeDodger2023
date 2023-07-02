using UnityEngine;
using System.Collections;

public class powerUpMenuScript : MonoBehaviour
{

   	//menuHandlerScript menuhandlescript;
	//credits_menuHandlerScript credits_menuhandlescript;
	GameObject player;
    	settingsScript settingsScript;

   	public Rigidbody cubeRB;

    	void Start()
    	{
		/*try {
			menuhandlescript = GameObject.Find("menuHandler").GetComponent<menuHandlerScript>();	
		}
		catch {
			menuhandlescript = null;
		}
		try {
			credits_menuhandlescript = GameObject.Find("menuHandler").GetComponent<credits_menuHandlerScript>();	
		}
		catch {
			credits_menuhandlescript = null;
		}*/
		player = GameObject.Find ("playerREF");
	        settingsScript = GameObject.Find("settingsHolder").GetComponent<settingsScript>();

	        if (settingsScript.modifierAirdrop)
	        {
	            gameObject.AddComponent<Rigidbody>();
	            cubeRB = gameObject.GetComponent<Rigidbody>();
	            cubeRB.freezeRotation = true;
	        }

    	}
	    // Update is called once per frame
	    void Update()
	    {
	        //Debug.Log ("this position " + this.transform.position + " playerPos + 10 " + GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>().playerPos.z + 10);
	        if (player.transform.position.z - 300 > this.transform.position.z || gameObject.transform.position.z > 5000 || gameObject.transform.position.z < -5000)
	        {
	            Destroy(gameObject);
	        }

	        if (cubeRB != null)
	        {
	            if (cubeRB.velocity == Vector3.zero && gameObject.transform.position.y <= 1)
	            {
	                Destroy(cubeRB);
	                if (gameObject.transform.position.y != 1)
	                {
	                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
	                }
	            }
	        }

	        doAnimation();
	    }

	    void doAnimation()
	    {
	        gameObject.transform.Rotate(0, 1, 0);
	    }
}