using UnityEngine;
using System.Collections;

public class bubbleScript : MonoBehaviour {

	//Script for Bubble object

	GameObject player;
	MeshCollider playerCollider;
	SphereCollider bubbleCollider;
	bool isBubbleAwake;
	bool isBubbleKilled;

	float scale;

	playerScript playerScript;
	progressive_playerScript prog_playerScript;
	finale_progressive_playerScript finale_progressive_playerScript;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("playerREF");
		playerCollider = player.GetComponent<MeshCollider> ();
		bubbleCollider = gameObject.GetComponent<SphereCollider> ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.position = player.transform.position;

		if (isBubbleAwake)
		{
			if (isBubbleKilled)
			{
				bubbleCollider.enabled = false;
				//playerCollider.enabled = true; 3.0: we make player invulnerable via iframes now.
				kill ();
			}
			else
			{
				if (playerCollider.enabled)
				{
					playerCollider.enabled = false;
				}

				if (!bubbleCollider.enabled)
				{
					bubbleCollider.enabled = true;
				}
			}
		}
		else
		{
			wakeUp ();
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name != "floorPlane")
		{
			isBubbleKilled = true;
			Destroy (collision.gameObject);
			if (Application.loadedLevelName == "game")
            {
				playerScript = player.GetComponent<playerScript>();
				playerScript.iFramesTimer = 2.0F;
            } else if (Application.loadedLevelName == "game_progressive")
            {
				prog_playerScript = player.GetComponent<progressive_playerScript>();
				prog_playerScript.iFramesTimer = 2.0F;
            } else if (Application.loadedLevelName == "game_progressive_finale")
            {
				finale_progressive_playerScript = player.GetComponent<finale_progressive_playerScript>();
				finale_progressive_playerScript.iFramesTimer = 2.0F;
            }
		}
	}

	void wakeUp()
	{
		if (scale < 2)
		{
			scale += Time.deltaTime * 2;
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);
		}
		else
		{
			scale = 2;
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);
			isBubbleAwake = true;

		}
	}

	void kill()
	{
		if (scale > 0.25f)
		{
			scale -= Time.deltaTime * 2;
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);
		}
		else
		{
			Destroy (gameObject);
		}
	}
}
