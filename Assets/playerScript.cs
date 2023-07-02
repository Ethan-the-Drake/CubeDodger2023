using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour
{
	
	GameObject lightObj;
	Vector3 lightRot;
	settingsScript settings;
	public Renderer rend;
	
	public bool initiateGameOver;
	public float playerEmissionR = 0;
	public float playerEmissionG = 0;
	public float playerEmissionB = 0;

	public bool initBrighten;
	public bool initDarken;
	public bool isNight = false;

	public bool devCheat_GodMode = false;

	//3.0
	public float iFramesTimer = 1.0F;
	public float blinkTimer = 0.1f;
	public float blinkLength = 0.1f;

	void Start ()
	{
		settings = GameObject.Find ("settingsHolder").GetComponent<settingsScript> ();

		if (settings.enableDevCheats) {
			devCheat_GodMode = true;
		}

		lightObj = GameObject.Find ("masterLight");
		rend = GetComponent<Renderer> ();
		rend.material.shader = Shader.Find ("Standard");
		rend.material.EnableKeyword ("_EMISSION");

		if (Application.loadedLevelName == "menu")
		{
			gameObject.GetComponent<Rigidbody>().detectCollisions = false;
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.GetComponent<powerUpScript>() == null && collision.gameObject.GetComponent<projectileScript>() == null)
        {
            initiateGameOver = true;
        }
	}
	
	void playerMaterialBrighten()
	{
		//Debug.Log ("Brightening");
		if (playerEmissionR >= .5 && playerEmissionG >= .5 && playerEmissionB >= .5)
		{
			initBrighten = false;
		}
		else
		{
			playerEmissionR = playerEmissionR + Time.deltaTime*(lightObj.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
			playerEmissionG = playerEmissionG + Time.deltaTime*(lightObj.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
			playerEmissionB = playerEmissionB + Time.deltaTime*(lightObj.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
			rend.material.SetColor("_EmissionColor", new Color (playerEmissionR, playerEmissionG, playerEmissionB));
		}
	}
	void playerMaterialDarken()
	{
		//Debug.Log ("Darkening");
		if (playerEmissionR <= 0.0 && playerEmissionG <= 0.0 && playerEmissionB <= 0.0)
		{
			initDarken = false;
		}
		else
		{
			playerEmissionR = playerEmissionR - Time.deltaTime*(lightObj.GetComponent<autoIntensity>().dayRotateSpeed.x)/8;
			playerEmissionG = playerEmissionG - Time.deltaTime*(lightObj.GetComponent<autoIntensity>().dayRotateSpeed.x)/8;
			playerEmissionB = playerEmissionB - Time.deltaTime*(lightObj.GetComponent<autoIntensity>().dayRotateSpeed.x)/8;
			rend.material.SetColor("_EmissionColor", new Color (playerEmissionR, playerEmissionG, playerEmissionB));
		}
	}
	
	void Update ()
	{
		//iFrames countdown (3.0)
		if (devCheat_GodMode || (iFramesTimer > 0 && gameObject.GetComponent<MeshCollider>().enabled))
        {
			gameObject.GetComponent<MeshCollider>().enabled = false;
        } else if (iFramesTimer <= 0 && gameObject.GetComponent<MeshCollider>().enabled == false)
        {
			gameObject.GetComponent<MeshCollider>().enabled = true;
		}
		if ((Application.loadedLevelName == "game" && GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>().EnablePlayerControls) || Application.loadedLevelName == "menu") iFramesTimer = iFramesTimer - Time.deltaTime;
		if (devCheat_GodMode && Application.loadedLevelName == "game")
		{
			GameObject.Find ("SceneHandler").GetComponent<SceneHandlerScript>().HaveCheatsBeenEnabled = true;
		}
		MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
		//Player i-frames blink
		if (iFramesTimer > 0 && blinkTimer > 0)
        {
			if ((Application.loadedLevelName == "game" && GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>().EnablePlayerControls) || Application.loadedLevelName == "menu") blinkTimer -= Time.deltaTime;
        } else if (iFramesTimer > 0 && blinkTimer <= 0)
        {
			mr.enabled = !mr.enabled;
			blinkTimer = blinkLength;
        } else if (!mr.enabled && iFramesTimer <= 0)
        {
			mr.enabled = true;
        }

		if ((this.transform.position.x >= 5000) || (this.transform.position.x <= -5000))
		{
			initiateGameOver = true;
		}

		lightRot = lightObj.transform.localEulerAngles;
		if (Application.loadedLevelName == "game")
		{
			if (GameObject.Find ("SceneHandler").GetComponent<SceneHandlerScript>().gameOverDoOnce == false)
			{
				if (isNight == false && (lightRot.x < 12 && lightRot.y == 180))
				{
					initBrighten = true;
					isNight = true;
				}
				if (lightRot.x > 8 && lightRot.x < 100 && lightRot.y == 0 && isNight == true)
				{
					initDarken = true;
					isNight = false;
				}
			}
			if (initBrighten == true)
			{
				playerMaterialBrighten();
			}
			else if (initDarken == true)
			{
				playerMaterialDarken();
			}
		}
		else
		{
			if (lightRot.x < 5 && lightRot.y == 180 && isNight == false)
			{
				initBrighten = true;
				isNight = true;
			}
			if (lightRot.x > 355 && lightRot.y <= 10 && isNight == true)
			{
				initDarken = true;
				isNight = false;
			}
			
			if (initBrighten == true)
			{
				playerMaterialBrighten();
			}
			else if (initDarken == true)
			{
				playerMaterialDarken();
			}
		}


	}
}

