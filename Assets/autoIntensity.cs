
using UnityEngine;
using System.Collections;

public class autoIntensity : MonoBehaviour {

	public settingsScript settings;
	public ProgressiveSceneHandlerScript prog_sceneHandle;
	public menuHandlerScript menu_sceneHandle;
	public credits_menuHandlerScript credits_menu_sceneHandle;
	public finale_ProgressiveSceneHandlerScript finale_prog_sceneHandle;

	public Gradient nightDayColor;
	
	public float maxIntensity = 3f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;
	
	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;
	
	
	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;
	public float fogScale = 1f;
	
	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;
	
	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;
	
	float skySpeed = 1;
	
	
	Light mainLight;
	Skybox sky;
	Material skyMat;
	
	void Start () 
	{
		try{
			settings = GameObject.Find ("settingsHolder").GetComponent<settingsScript> ();
		}catch{
			Destroy (gameObject);
		}

		try{
			menu_sceneHandle = GameObject.Find("menuHandler").GetComponent<menuHandlerScript>();
		} catch {
			menu_sceneHandle = null;
		}

		try{
			prog_sceneHandle = GameObject.Find("SceneHandler").GetComponent<ProgressiveSceneHandlerScript>();
		}
		catch{
			prog_sceneHandle = null;
		}

		try{
			credits_menu_sceneHandle = GameObject.Find("menuHandler").GetComponent<credits_menuHandlerScript>();
		}
		catch{
			credits_menu_sceneHandle = null;
		}

		try{
			finale_prog_sceneHandle = GameObject.Find("SceneHandler").GetComponent<finale_ProgressiveSceneHandlerScript>();
		}
		catch{
			finale_prog_sceneHandle = null;
		}


		mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;

		//InvokeRepeating ("Update5s", 0, 5);

	}
	
	void Update () 
	{
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		if ((finale_prog_sceneHandle != null) || (credits_menu_sceneHandle != null && (credits_menu_sceneHandle.creditsStep < 2 || credits_menu_sceneHandle.creditsStep > 4))) {
			//Debug.Log ("didid");
			mainLight.intensity = i;

			tRange = 1 - minAmbientPoint;
			dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
			i = ((maxAmbient - minAmbient) * dot) + minAmbient;
			RenderSettings.ambientIntensity = i;

			mainLight.color = new Color (0.8f, 0.8f, 0.8f);
			RenderSettings.ambientLight = mainLight.color;

			RenderSettings.fogColor = nightDayFogColor.Evaluate (dot);
			RenderSettings.fogDensity = fogDensityCurve.Evaluate (dot) * fogScale;

			i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
			skyMat.SetFloat ("_AtmosphereThickness", i);
		}
		else if ((prog_sceneHandle == null || prog_sceneHandle.currentWorld == 0) && ((menu_sceneHandle == null) || (menu_sceneHandle.creditsStep < 2 || menu_sceneHandle.creditsStep > 4)) && (credits_menu_sceneHandle == null)) {
			mainLight.intensity = i;

			tRange = 1 - minAmbientPoint;
			dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
			i = ((maxAmbient - minAmbient) * dot) + minAmbient;
			RenderSettings.ambientIntensity = i;

			mainLight.color = nightDayColor.Evaluate (dot);
			RenderSettings.ambientLight = mainLight.color;

			RenderSettings.fogColor = nightDayFogColor.Evaluate (dot);
			RenderSettings.fogDensity = fogDensityCurve.Evaluate (dot) * fogScale;

			i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
			skyMat.SetFloat ("_AtmosphereThickness", i);

			if (dot > 0)
				transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
			else
				transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);
		} else if (menu_sceneHandle == null && credits_menu_sceneHandle == null && prog_sceneHandle.currentWorld == 1) {

			mainLight.intensity = 1;

			tRange = 1 - minAmbientPoint;
			dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
			i = ((maxAmbient - minAmbient) * dot) + minAmbient;
			RenderSettings.ambientIntensity = i;

			mainLight.color = Color.black;
			RenderSettings.ambientLight = mainLight.color;

			RenderSettings.fogColor = nightDayFogColor.Evaluate (dot);
			RenderSettings.fogDensity = fogDensityCurve.Evaluate (dot) * fogScale;

			i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
			skyMat.SetFloat ("_AtmosphereThickness", i);

			if (dot > 0)
				transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
			else
				transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);
		} else if (menu_sceneHandle == null && credits_menu_sceneHandle == null && prog_sceneHandle.currentWorld == 2) {

			mainLight.intensity = 0;

			RenderSettings.ambientIntensity = 0;

			mainLight.color = Color.black;
			RenderSettings.ambientLight = mainLight.color;

		} else if (((menu_sceneHandle != null) && (menu_sceneHandle.creditsStep >= 2 && menu_sceneHandle.creditsStep <= 4)) || ((credits_menu_sceneHandle != null) && (credits_menu_sceneHandle.creditsStep >= 2 && credits_menu_sceneHandle.creditsStep <= 4)) || prog_sceneHandle.currentWorld == 3) {
			mainLight.intensity = 1;

			tRange = 1 - minAmbientPoint;
			dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
			i = ((maxAmbient - minAmbient) * dot) + minAmbient;
			RenderSettings.ambientIntensity = i;

			mainLight.color = new Color (0.65f, 0, 0, 1);
			RenderSettings.ambientLight = mainLight.color;

			RenderSettings.fogColor = nightDayFogColor.Evaluate (dot);
			RenderSettings.fogDensity = fogDensityCurve.Evaluate (dot) * fogScale;

			i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
			skyMat.SetFloat ("_AtmosphereThickness", i);


			if (transform.eulerAngles.x > -185) {
				transform.eulerAngles = new Vector3 (-185, 0, 0);
			} else if (dot > 0) { 
				transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
			} else {
				transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);
			}
		}
	}
}