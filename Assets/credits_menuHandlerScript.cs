//Code written by Ethan 'the Drake' Casey
//Copyright 2016
//Contact me at "EthanDrakeCasey@gmail.com" for inquiries.
//=====================================================================

using UnityEngine;
using System.Collections;

public class credits_menuHandlerScript : MonoBehaviour {

	settingsScript settings;
	leaderboardsHolder leaderboards;
	AchievementsScript achievements;
	playerScript playerScript;

	string gameVersion;	

	public GUISkin CDGUIMenuSkin;
	public GUISkin CDGUILargeTextSkin;
	float boxHeight;
	public float buttonPositionChangeThreshold;
	public float backButtonPositionChangeThreshold;
	public Texture titleCardTexture;

	Resolution[] resolutions;
	int totalResolutionListHeight;
	bool getResolutionDropdownHeightOnce;
	int limboWidthResolution;
	int limboHeightResolution;
	bool limboFullscreen;
	int dropDownCounter;
	public GameObject settingsHolder;


	int cubeMatPicker; //random value that gets assigned every frame that determines the color of the next cube spawned.
	GameObject newCube; //Game Object of the newest spawned cube. Updated every frame.
	GameObject RedCubeREF; //Cube that new red cubes are copied from.
	GameObject GreenCubeREF;//Cube that new green cubes are copied from.
	GameObject BlueCubeREF;//Cube that new blue cubes are copied from.
	GameObject RedBallREF;
	GameObject GreenBallREF;
	GameObject BlueBallREF;

	GameObject farCubeCut;
	GameObject nearCubeCut;
	
	GameObject player; //player object
	GameObject gameCamera; //camera object
	UnityStandardAssets.ImageEffects.ScreenOverlay gameCameraScreenOverlay;
	UnityStandardAssets.ImageEffects.DepthOfField gameCameraDoF;
	UnityStandardAssets.ImageEffects.MotionBlur gameCameraBlur;
	GameObject sunShafts;
	GameObject floor; //floor object
	GameObject objLight; //light
	public Vector3 playerPos; //tracks position of player
	public Vector3 playerRot; //tracks rotation of player (for animation)
	public Vector3 cameraPos; //tracks position of camera
	public Vector3 floorPos;  //trakcs position of floor
	Rigidbody rb; //player rigid body
	public Vector3 playerVelocity; //tracks player velocity
	float currentZVelocity; //current Z Velocity
	float currentXVelocity; //current X Velocity

	public Color GUIColor = new Color (0,0,0);
	Vector2 creditsScrollPosition;
	Vector2 resolutionDropdownScrollPosition;
	Vector2 audioTracksScrollPosition;
	Vector2 achievementsScrollPosition;

	Vector3 creditsCameraVector;
	GameObject blackHole;
	GameObject stars;
	public bool creditsDoOnce;

	bool showLeaderboards;
	bool showLeaderboardsResetAreYouSure;
	public bool showCredits;
	bool showOptions;
	bool showGraphicsOptions;
	bool showAudioOptions;
	public bool showModifiers;
	bool initLoadGame;
	bool initExitGame;
	bool showResolutionDropdown;
	bool showPresetQualityDropdown;
	public bool showStartup = true;
	public bool panCameraDown;
	public Texture playButtonTexture;
	public Texture stopButtonTexture;
	public Texture lockedButtonTexture;
	public Texture achievementUnlocked;
	public Texture achievementLocked;
	int audioTrackScrollingBaseHeightModifier;
	bool isPreviewingSong;

	//1.2.0 UI changes
	public bool fatMenuTransUp;
	public bool fatMenuTransDown;
	public bool pickGameTransDown;
	public bool pickGameTransUp;

	public bool doCreditsTransition;
	public int creditsStep; // 0 = Not showing credits, 1 = Fade to black from menu, 2 = Fade in to credits, 3 = showing credits, 4 = Fade to black from credits, 5 = Fade from black to menu
	public int credTexStep;
	public float credTexTimer;
	public Color credTexColor;


	public bool showExtras;
	public bool showGameModeSelection;
	public bool showAchievements;
	int achListGUIHeight;

	public bool isTurnedAround;
	bool credits_menuLoadMainMenu;
	bool credits_menuLoadFinale;
	//-------------------------

	public bool showFirstTimeMessage;

	GameObject currentSong;
	float leaderboardsDifficulty = -1;
	bool leaderboardsRandomizer;
	//1.2.0
	bool leaderboardsProgressive;

   	 //powerup references
    	GameObject powerup_JumpREF;
   	 GameObject powerup_ShootREF;
    	GameObject powerup_SlowmoREF;
    	GameObject powerup_BubbleREF;
    	bool isPowerUpSpawning;
	public int cubeXMin = -250;
	public int cubeXMax = -1;
	public float cubeSpawnRate = 60 / 6000;
	//
	//int musicPicker;

	//Component PlayerScript;

	int difficultyZVelocity = 25; // player's velocity based on difficulty

	
	// Use this for initialization
	// Spawn player and any initilization
	void Start ()
	{
		resolutions = Screen.resolutions;
		//print (resolutions);

		settingsHolder = GameObject.Find ("settingsHolder");
		settings = settingsHolder.GetComponent<settingsScript> ();
		gameVersion = settings.gameVersion;
		if (settings.doMenuIntro == false) {
			panCameraDown = true;
			showStartup = false;
		}

		leaderboards = settingsHolder.GetComponent<leaderboardsHolder> ();
		achievements = settingsHolder.GetComponent<AchievementsScript> ();

		Cursor.visible = false;
		//settings.optionsPresetQuality = QualitySettings.GetQualityLevel ();

		RedCubeREF = GameObject.Find ("referenceRedCube");
		GreenCubeREF = GameObject.Find ("referenceGreenCube");
		BlueCubeREF = GameObject.Find ("referenceBlueCube");

		RedBallREF = GameObject.Find ("referenceRedBall");
		GreenBallREF = GameObject.Find ("referenceGreenBall");
		BlueBallREF = GameObject.Find ("referenceBlueBall");

		//powerup refs 1.2.0
		powerup_JumpREF = GameObject.Find ("referenceJumpPU");
		powerup_ShootREF = GameObject.Find ("referenceShootPU");
		powerup_SlowmoREF = GameObject.Find ("referenceSlomoPU");
		powerup_BubbleREF = GameObject.Find ("referenceBubblePU");
		//

		blackHole = GameObject.Find ("blackHole");
		stars = GameObject.Find ("stars");

		farCubeCut = GameObject.Find ("cutCubePattern_far");
		nearCubeCut = GameObject.Find ("cutCubePattern_near");

		player = GameObject.Find ("playerREF");
		playerScript = player.GetComponent<playerScript> ();
		gameCamera = GameObject.Find ("cameraREF");
		//sunShafts = GameObject.Find ("sunShafts");
		floor = GameObject.Find ("floorPlane");
		objLight = GameObject.Find ("masterLight");
		player.transform.position = new Vector3 (0, 0, -50);
		gameCamera.transform.position = new Vector3 (0, 4, -30);
		rb = player.GetComponent<Rigidbody> ();

		if (settings.gameCamera == null) {
			settings.gameCamera = gameCamera;
		}

		gameCameraScreenOverlay = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ();
		gameCameraDoF = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
		gameCameraBlur = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur> ();
		//gameCameraVortex = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex> ();

		showGraphicsOptions = true;
		showAudioOptions = false;

		//pickMusic ();
		//InvokeRepeating ("Update1s", 0, 1);
		if (settings.optionsPresetQuality <= 2 || settings.optionsDepthOfField == false)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
		}

		if (Application.isMobilePlatform || settings.simulateMobileOptimization) {
			doMobileOptimization ();
		}
	}

	void doMobileOptimization(){
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunShaftIntensity = 0;
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = false;

		cubeXMin = -62;
		cubeXMax = -1;
		cubeSpawnRate = 0.04f;
		Screen.sleepTimeout = SleepTimeout.SystemSetting;
	}

	/*void Update1s(){
		
	}*/

	// Update is called once per frame
	void Update () 
	{
		if (playerPos.z >= 5000)
		{
			player.transform.position = new Vector3(0, playerPos.y, -5000);
		}

		playerPos = player.transform.position;
		playerRot = player.transform.eulerAngles;
		if (!pickGameTransDown && !pickGameTransUp && !showCredits)
		{
			cameraPos = new Vector3(playerPos.x + 5, playerPos.y+2, playerPos.z);
		}
		if (!showCredits) {
			gameCamera.transform.position = cameraPos;
		}


		floorPos = new Vector3 (gameCamera.transform.position.x, floor.transform.position.y, gameCamera.transform.position.z);
		objLight.transform.position = gameCamera.transform.position;
		floor.transform.position = floorPos;
		playerVelocity = rb.velocity;

		blackHole.transform.position = new Vector3 (gameCamera.transform.position.x, 0, player.transform.position.z + 1500); 
		stars.transform.position = new Vector3 (gameCamera.transform.position.x, 0, gameCamera.transform.position.z);

		if (gameCameraScreenOverlay.intensity < 0 && (playerPos.z > 0 || doCreditsTransition))
		{
			gameCameraScreenOverlay.intensity = gameCameraScreenOverlay.intensity + Time.deltaTime/6;
		}
		else if (gameCameraScreenOverlay.intensity > 0)
		{
			gameCameraScreenOverlay.intensity = 0;
		}

		if (playerPos.z + 250 < 4800 && !showCredits)
		{
			if (!IsInvoking ("spawnNewCube")) {
				InvokeRepeating("spawnNewCube", 0, cubeSpawnRate);
			}
		} else {
			if (IsInvoking ("spawnNewCube")) {
				CancelInvoke ();
			}
		}

		updateCubeCutters ();


		// If the player is below the speed the difficulty demands, raise him to that speed.
		if (playerVelocity.z != (50) && !showCredits)
		{
			rb.velocity = new Vector3(playerVelocity.x, playerVelocity.y, (50));
		}


		//Menu Beautifulness ============================================================================================
		if (showStartup == true && Input.anyKeyDown && gameCameraScreenOverlay.intensity >= 0)
		{
			panCameraDown = true;
			showStartup = false;

			leaderboards.pullLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);
            
		}

		if (panCameraDown == true)
		{
			initMenuCameraTransition();
		}

		if (initLoadGame == true)
		{
			loadGameTransition();
		}

		if (initExitGame == true)
		{
			exitGameTransition();
		}

		if (currentSong == null || currentSong.GetComponent<AudioSource>().isPlaying == false && settings.optionsMenuArsFarewellTheInnocentEnabled && (creditsStep < 2 || creditsStep > 4))
		{
			pickMusic();
		}

		if (doCreditsTransition) {
			creditsTransition ();
		}

		if (showExtras || showGameModeSelection || showCredits)
		{
			gameCameraDoF.focalLength = 0;

		}
		else
		{
			if (gameCameraDoF.focalLength < 10)
			{
				gameCameraDoF.focalLength = gameCameraDoF.focalLength + 25 * Time.deltaTime;
			}
			if (gameCameraDoF.focalSize < .35f)
			{
				gameCameraDoF.focalSize = gameCameraDoF.focalSize + 1 * Time.deltaTime;
			}

		}
	}

	void spawnNewCube()
	{
		cubeMatPicker = Random.Range (1, 4);


		if (cubeMatPicker == 1) {
			newCube = Instantiate (RedCubeREF);
		} else if (cubeMatPicker == 2) {
			newCube = Instantiate (GreenCubeREF);
		} else if (cubeMatPicker == 3) {
			newCube = Instantiate (BlueCubeREF);
		} else if (cubeMatPicker == 4) {
			newCube = Instantiate (RedBallREF);
		} else if (cubeMatPicker == 5) {
			newCube = Instantiate (GreenBallREF);
		} else if (cubeMatPicker == 6) {
			newCube = Instantiate (BlueBallREF);
		}

		newCube.GetComponent<cubeMenuDeleteScript> ().isReferenceCube = false;

		if (playerPos.z + (500) < 4750 && playerPos.z + (250) > -5000) {
			newCube.transform.position = new Vector3 (playerPos.x + ((Random.Range (cubeXMin, cubeXMax))), 0, (playerPos.z + (500)));
		}	
	}

	void updateCubeCutters()
	{
		if (playerPos.z >= 4500)
		{
			if (farCubeCut.activeSelf == false)
			{
				farCubeCut.SetActive(true);
			}		
		}
		else
		{
			if (farCubeCut.activeSelf == true)
			{
				farCubeCut.SetActive(false);
			}
		}

		if (playerPos.z <= -4500)
		{
			if (nearCubeCut.activeSelf == false)
			{
				nearCubeCut.SetActive(true);
			}		
		}
		else
		{
			if (nearCubeCut.activeSelf == true)
			{
				nearCubeCut.SetActive(false);
			}
		}
	}



	void pickMusic(int musicPicker = 0, bool checkEnabled = true)	// picks a random music track to play
	{

		Debug.Log (musicPicker + ", " + checkEnabled);

		if (currentSong != null)
		{
			currentSong.GetComponent<AudioSource>().Stop();
		}	
		if (checkEnabled) {
			if (settings.optionsMenuArsFarewellTheInnocentEnabled) {
				currentSong = GameObject.Find ("Ars1");
				currentSong.GetComponent<AudioSource> ().Play ();
				if (currentSong.GetComponent<AudioSource> ().volume == 0) {
					currentSong.GetComponent<AudioSource> ().volume = 1;
				}
			} else {
				Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
			}
		} else {
			currentSong = GameObject.Find ("Ars1");
			currentSong.GetComponent<AudioSource> ().Play ();
			if (currentSong.GetComponent<AudioSource> ().volume == 0) {
				currentSong.GetComponent<AudioSource> ().volume = 1;
			}
		}

	}


	void initMenuCameraTransition() // Function for camera initial camera transition
	{
		if (gameCamera.transform.eulerAngles.x > 5)
		{
			gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x + Time.deltaTime*15, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			if (gameCameraDoF.focalLength > 10)
			{
				gameCameraDoF.focalLength = gameCameraDoF.focalLength - Time.deltaTime*250;
			}
			else
			{
				gameCameraDoF.focalLength = 10;
			}

        }	
		else
		{
			panCameraDown = false;
		}
		
	}
    //1.2.0 UI changes

	void creditsTransition() {

		if (creditsStep == 1) {
			//Fade to black from menu
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity > -2) {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity - 0.75f * Time.deltaTime;
			} else {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = -2;
			}

			if (currentSong != null && currentSong.GetComponent<AudioSource> ().volume > 0) {
				currentSong.GetComponent<AudioSource> ().volume = currentSong.GetComponent<AudioSource> ().volume - 0.25f * Time.deltaTime;
			} else if (currentSong != null) {
				currentSong.GetComponent<AudioSource> ().volume = 0;
			}

			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity == -2 && (currentSong == null || currentSong.GetComponent<AudioSource> ().volume == 0)) {
				creditsStep = 2;

				gameCamera.transform.position = new Vector3 (0, 2, player.transform.position.z + 800);
				gameCamera.transform.eulerAngles = new Vector3 (-10, 0, 0);

				stars.GetComponent<ParticleSystem> ().startColor = Color.red;
				stars.GetComponent<ParticleSystem> ().maxParticles = 15000;
				stars.GetComponent<ParticleSystem> ().Clear (true);
				objLight.GetComponent<autoIntensity> ().dayRotateSpeed.x = 0;
				objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x = 0;
				objLight.GetComponent<autoIntensity> ().dayAtmosphereThickness = 8;
				objLight.GetComponent<autoIntensity> ().nightAtmosphereThickness = 0.8f;
				objLight.GetComponent<Light> ().color = Color.red;
				objLight.transform.localEulerAngles = new Vector3 (150, 0, 0);

				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = true;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunColor = Color.red;

				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().enabled = true;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur> ().enabled = false;

				if (currentSong != null) {
					currentSong.GetComponent<AudioSource> ().Stop ();
					currentSong.GetComponent<AudioSource> ().volume = 1;
				}
				GameObject.Find ("creditsMusic").GetComponent<AudioSource> ().volume = 1;
				GameObject.Find ("creditsMusic").GetComponent<AudioSource> ().Play ();
				currentSong = GameObject.Find ("creditsMusic");
			}

		} else if (creditsStep == 2) {
			//Fade from black to credits
			if (gameCamera.transform.eulerAngles.x != -10) {
				gameCamera.transform.eulerAngles = new Vector3 (-10, 0, 0);
			}
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity == 0 && GameObject.Find ("creditsMusic").GetComponent<AudioSource> ().isPlaying == true) {
				creditsStep = 3;
			}
		} else if (creditsStep == 3) {
			//showing credits
		} else if (creditsStep == 4) {
			//Fade to black from credits
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity > -2) {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity - 0.75f * Time.deltaTime;
			} else {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = -2;
			}

			if (currentSong != null && currentSong.GetComponent<AudioSource> ().volume > 0) {
				currentSong.GetComponent<AudioSource> ().volume = currentSong.GetComponent<AudioSource> ().volume - 0.25f * Time.deltaTime;
			} else if (currentSong != null) {
				currentSong.GetComponent<AudioSource> ().volume = 0;
				currentSong.GetComponent<AudioSource> ().Stop ();
			}
			
			if ((currentSong == null || currentSong.GetComponent<AudioSource> ().volume == 0) && gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity == -2){
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = -2;

				gameCamera.transform.position = new Vector3 (playerPos.x + 2, 2, playerPos.z);

				gameCamera.transform.eulerAngles = new Vector3 (0, -90, 0);

				stars.GetComponent<ParticleSystem> ().startColor = Color.white;
				stars.GetComponent<ParticleSystem> ().maxParticles = 15000;
				stars.GetComponent<ParticleSystem> ().Clear (true);
				objLight.GetComponent<autoIntensity> ().dayRotateSpeed.x = 0;
				objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x = 0;
				objLight.GetComponent<autoIntensity> ().dayAtmosphereThickness = 0f;
				objLight.GetComponent<autoIntensity> ().nightAtmosphereThickness = 0f;
				objLight.GetComponent<Light> ().color = new Color(0.8f,0.8f,0.8f);
				objLight.transform.localEulerAngles = new Vector3 (20, 125, 0);

				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = true;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunColor = new Color(0.8f, 0.8f, 0.8f);

				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().enabled = false;

				currentSong.GetComponent<AudioSource> ().Stop ();

				showCredits = false;
				creditsDoOnce = false;
				creditsStep = 5;
			}
		} else if (creditsStep == 5) {
			//Fade from black to menu

			if (!GameObject.Find ("Ars1").GetComponent<AudioSource> ().isPlaying) {
				pickMusic ();
			}

			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity == 0) {
				//currentSong = GameObject.Find ("creditsMusic");
				doCreditsTransition = false;
				creditsStep = 0;

			}
		}


	}
	//--------------

	void loadGameTransition()
	{
		panCameraDown = false;
		if (Cursor.visible == true)
		{
			Cursor.visible = false;
		}
		if (gameCamera.transform.eulerAngles.x > 300 || gameCameraScreenOverlay.intensity > -2 || (currentSong != null && currentSong.GetComponent<AudioSource> ().volume > 0))
		{
			gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x - Time.deltaTime * 15, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			gameCameraScreenOverlay.intensity = gameCameraScreenOverlay.intensity - Time.deltaTime;
			if (currentSong != null)
			{
				currentSong.GetComponent<AudioSource> ().volume = currentSong.GetComponent<AudioSource> ().volume - Time.deltaTime / 4;
			}
		}
		else if (credits_menuLoadFinale)
		{
			Application.LoadLevel ("game_progressive_finale");
		}
		else
		{
			Application.LoadLevel ("loading_menu");
		}
	}

	void exitGameTransition()
	{
		panCameraDown = false;
		if (Cursor.visible == true)
		{
			Cursor.visible = false;
		}
		if (gameCamera.transform.eulerAngles.x > 300 || gameCameraScreenOverlay.intensity > -2 || currentSong.GetComponent<AudioSource>().volume > 0)
		{
			gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x - Time.deltaTime*15, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			gameCameraScreenOverlay.intensity = gameCameraScreenOverlay.intensity - Time.deltaTime;
			currentSong.GetComponent<AudioSource>().volume = currentSong.GetComponent<AudioSource>().volume - Time.deltaTime/4;
		}
		else
		{
			Cursor.visible = true;
			Application.Quit();
		}
	}

	void OnGUI () // GUI Stuff
	{
		if (!settings.screenshotMode)
		{
			GUI.skin = CDGUIMenuSkin;

			GUI.color = Color.white;

			if ((settings.showSettingsResetScreen || leaderboards.showLeaderboardsError || showLeaderboardsResetAreYouSure) && !showStartup && !showFirstTimeMessage)
			{
				if (Cursor.visible == false)
				{
					Cursor.visible = true;
				}

				GUI.Box(new Rect(Screen.width/2 - 75, 50, 150, 40),"");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;


				if (showLeaderboardsResetAreYouSure)
				{

					GUI.Label(new Rect(Screen.width/2 - 60, 45, 120, 40), "Confirm");

					GUI.skin = CDGUIMenuSkin;
					GUI.color = Color.white;
					if (GUI.Button (new Rect (Screen.width / 2 - (200), 300, 120, 40), "Yes"))
					{
						showLeaderboardsResetAreYouSure = false;
						leaderboards.resetLeaderboards();
						leaderboards.pullLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);
					}
					if (GUI.Button (new Rect (Screen.width / 2 + (75), 300, 125, 40), "No"))
					{
						showLeaderboardsResetAreYouSure = false;
					}
				}
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				GUI.Box (new Rect (Screen.width / 2 - 100, 80, 200, 250), "");

				if (settings.showSettingsResetScreen)
				{
					GUI.Label (new Rect (Screen.width / 2 - (150/2), 120, 150, 125), "Your saved settings save file was corrupted, damaged, or you've updated to a new version of Cube Dodger.\n\nUnfortunately, this means that a new file had to be generated, and all your saved settings have been reset to default.\n\nA log has been created in the game directory.");
				}
				else if (leaderboards.showLeaderboardsError)
				{
					GUI.Label (new Rect (Screen.width / 2 - (150/2), 120, 150, 125), "Your leaderboards save file was corrupted, damaged, or you've updated to a new version of Cube Dodger.\n\nUnfortunately, this means that a new file had to be generated, and all your leaderboard scores have been reset.\n\nA log has been created in the game directory.");
				}
				else if (showLeaderboardsResetAreYouSure)
				{
					GUI.Label (new Rect (Screen.width / 2 - (150/2), 120, 150, 125), "Are you sure you want to reset the leaderboards?\n\nThis action CANNOT be reversed! All of your saved scores will PERMANENTLY be deleted!");
				}

				if ((settings.showSettingsResetScreen || leaderboards.showLeaderboardsError))
				{
					
					GUI.color = Color.black;
					GUI.skin = CDGUILargeTextSkin;
					GUI.Label(new Rect(Screen.width/2 - 60, 45, 120, 40),"Error");
					GUI.skin = CDGUIMenuSkin;
					GUI.color = Color.white;
					
					if (GUI.Button (new Rect (Screen.width / 2 - (125/2), 300, 125, 40), "Continue"))
					{
						if (settings.showSettingsResetScreen)
						{
							settings.showSettingsResetScreen = false;
						}
						else if (leaderboards.showLeaderboardsError)
						{
							leaderboards.showLeaderboardsError = false;
						}
						
					}
				}
			}

			if (showFirstTimeMessage && !showStartup)
			{
				if (Cursor.visible == false)
				{
					Cursor.visible = true;
				}
				
				GUI.Box(new Rect(Screen.width/2 - 75, 50, 150, 40),"");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label(new Rect(Screen.width/2 - 70, 45, 140, 40),"Welcome!");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				GUI.Box (new Rect (Screen.width / 2 - 150, 80, 300, 350), "");
				
				GUI.Label (new Rect (Screen.width / 2 - (280/2), 90, 280, 300), "Welcome to Cube Dodger!\n\nThank you for taking an interest in Cube Dodger. I hope you enjoy the game as much as I enjoyed making it.\n\nFeel free to contact me at\n\nEthanDrakeCasey@gmail.com\n\nor on Twitter\n\n@EthanTheDrake\n\nand be sure to visit\n\nreddit.com/r/CubeDodger\n\nfor news and updates about the game!\n\nAlso, if you find any bugs while playing, be sure to report them from the link on the main menu.\n\nTHANK YOU!");
				if (GUI.Button (new Rect (Screen.width / 2 - (125/2), 400, 125, 40), "Continue"))
				{
					showFirstTimeMessage = false;
				}
			}

			//GUIColor = new Color(playerScript.playerEmissionR, playerScript.playerEmissionG, playerScript.playerEmissionB);
			//print (playerScript.playerEmissionR, playerScript.playerEmissionG, playerScript.playerEmissionB);

			if (playerScript.initBrighten == true)
			{
				if (GUIColor.r < 1 && GUIColor.g < 1 && GUIColor.b < 1)
				{
					GUIColor.r = GUIColor.r + Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
					GUIColor.g = GUIColor.g + Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
					GUIColor.b = GUIColor.b + Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
				}
			}
			if (playerScript.initDarken == true)
			{
				if (GUIColor.r > 0 && GUIColor.g > 0 && GUIColor.b > 0)
				{
					GUIColor.r = GUIColor.r - Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
					GUIColor.g = GUIColor.g - Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
					GUIColor.b = GUIColor.b - Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
				}
			}

			/*if (440 > Screen.width / 2 + Screen.width/16)
			{
				buttonPositionChangeThreshold = 1;
			}
			else
			{
				buttonPositionChangeThreshold = 0;
			}*/

			if (400 > Screen.height - 80)
			{
				backButtonPositionChangeThreshold = 1;
			}
			else
			{
				backButtonPositionChangeThreshold = 0;
			}
			/*if (Screen.height*4/6 < 400) 
			{
				boxHeight = Screen.height*4/6;
			}
			else
			{
				boxHeight = 400;
			}*/

			if (showFirstTimeMessage == false && !leaderboards.showLeaderboardsError && settings.showSettingsResetScreen == false && !showLeaderboards && showCredits == false && showOptions == false && showModifiers == false && showStartup == false && initLoadGame == false && initExitGame == false && showExtras == false && showGameModeSelection == false && showAchievements == false) 
			{
				if (Cursor.visible == false)
				{
					Cursor.visible = true;
				}

				if (GUI.Button (new Rect (20, 40, 200, 60), new GUIContent("PLAY FINALE", "Conquer the hardest\nchallenge yet..."))) 
				{
					credits_menuLoadFinale = true;
					initLoadGame = true;
				}
				
				if (backButtonPositionChangeThreshold == 0)
				{
					if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 205, 60), new GUIContent("Exit to Main Menu", "Return to the Main Menu."))) 
					{
						credits_menuLoadMainMenu = true;
						initLoadGame = true;
					}
				}
				else
				{
					if (GUI.Button (new Rect (Screen.width - 225, 380, 205, 60), new GUIContent("Exit to Main Menu", "Return to the Main Menu."))) 
					{
						credits_menuLoadMainMenu = true;
						initLoadGame = true;
					}
				}
			}
			else if (showCredits == true)
			{
				if (!creditsDoOnce) {
					Cursor.visible = true;
					doCreditsTransition = true;
					creditsStep = 1;
					credTexStep = 1;
					credTexTimer = 0;
					credTexColor = new Color(1,1,1,0);
					if (!achievements.unlockedLittleThings) {
						achievements.unlockedLittleThings = true;
						settings.optionsMenuGravityLittleThingsEnabled = true;
						settings.optionsGameGravityLittleThingsEnabled = true;
						achievements.saveAchievements ();
					}
					creditsDoOnce = true;
				}

				rb.velocity = Vector3.zero;

				if (creditsStep == 3) {
					if (backButtonPositionChangeThreshold == 0)
					{
						if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Continue")) 
						{
							showExtras = false;
							creditsStep = 4;
						}
					}
					else
					{
						if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Continue")) 
						{
							showExtras = false;
							creditsStep = 4;
						}
					}


					//gameCamera.transform.LookAt (blackHole.transform.position);

					//GUI.Box(new Rect(Screen.width/2 - 75, 20, 150, 40),"");
					GUI.color = Color.white;
					GUI.skin = CDGUILargeTextSkin;
					if (credTexStep == 1 || credTexStep == 54) {
						GUI.color = credTexColor;
					}
					GUI.Label(new Rect(Screen.width/2 - 60, 15, 120, 40),"CREDITS");
					if (credTexStep >= 4 && credTexStep <= 48) {
						if (credTexStep == 4 || credTexStep == 48) {
							GUI.color = credTexColor;
						}
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 160, 400, 60), "Music by", CDGUILargeTextSkin.FindStyle("credTextMedium"));
					}
					if (credTexStep >= 10 && credTexStep <= 18) {
						if (credTexStep == 10 || credTexStep == 18) {
							GUI.color = credTexColor;
						}
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Broke For Free");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "brokeforfree.com", CDGUILargeTextSkin.FindStyle("credTextSmall"));
					}
					GUI.color = credTexColor;



					if (credTexStep >= 1 && credTexStep <= 3) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 30, 400, 60), "Created and Lovingly Developed by", CDGUILargeTextSkin.FindStyle ("credTextMedium"));
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Ethan 'the Drake' Casey");
					} else if (credTexStep >= 4 && credTexStep <= 6) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "A Drop A Day");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "soundcloud.com/adrop_aday", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Bad Influences");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "ERROR: Velocity Not Defined");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 60, 400, 60), "Fight Or Die");
					} else if (credTexStep >= 7 && credTexStep <= 9) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Ars Sonor");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "freemusicarchive.org/music/Ars_Sonor", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Farewell The Innocent");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Samaritan");
					} else if (credTexStep >= 10 && credTexStep <= 12) {

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "At The Count");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Blown Out");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 60, 400, 60), "Building The Sun");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 90, 400, 60), "Calm The Fuck Down");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 120, 400, 60), "Caught In The Beat");
					} else if (credTexStep >= 13 && credTexStep <= 15) {

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Fuck It");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Hella");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 60, 400, 60), "High School Snaps");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 90, 400, 60), "Like Swimming");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 120, 400, 60), "Living In Reverse");
					} else if (credTexStep >= 16 && credTexStep <= 18) {

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Luminous");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Mell's Parade");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 60, 400, 60), "Quit Bitching");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 90, 400, 60), "Something Elated");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 120, 400, 60), "The Great");
					} else if (credTexStep >= 19 && credTexStep <= 21) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Chris Zabriskie");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "chriszabriskie.com", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Another Version Of You");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Divider");
						GUI.Label (new Rect (Screen.width / 2 - 300, Screen.height / 2 + 60, 600, 60), "Everybody's Got Problems That Aren't Mine");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 90, 400, 60), "The 49th Street Galleria");
						GUI.Label (new Rect (Screen.width / 2 - 400, Screen.height / 2 + 120, 800, 60), "The Life and Death of a Certain K. Zabriskie, Patriarch");
						GUI.Label (new Rect (Screen.width / 2 - 300, Screen.height / 2 + 150, 600, 60), "There's A Special Place for Some People");
					} else if (credTexStep >= 22 && credTexStep <= 24) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "D SMILEZ");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "freemusicarchive.org/music/D_SMILEZ", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 400, Screen.height / 2, 800, 60), "Inspiration (I Won't Give Up On My Dreams)");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Let Your Body Move");
						GUI.Label (new Rect (Screen.width / 2 - 300, Screen.height / 2 + 60, 600, 60), "Lost In The Music");
					} else if (credTexStep >= 25 && credTexStep <= 27) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Decktonic");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "freemusicarchive.org/music/Decktonic", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Act IV");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "B@$$ J▲|\\/|");
						GUI.Label (new Rect (Screen.width / 2 - 300, Screen.height / 2 + 60, 600, 60), "Night Drive");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 90, 400, 60), "Stars");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 120, 400, 60), "Watch Your Dubstep Version 2");
					} else if (credTexStep >= 28 && credTexStep <= 30) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Gravity Sound");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "soundcloud.com/thatmuzakguy", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Little Things (Credits Song)");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Microscope");
						GUI.Label (new Rect (Screen.width / 2 - 300, Screen.height / 2 + 60, 600, 60), "Old Habits");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 90, 400, 60), "Radioactive Boy");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 120, 400, 60), "Train Tracks");
					} else if (credTexStep >= 31 && credTexStep <= 33) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Kai Engel");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "www.kai-engel.com", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Low Horizon");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Nothing");
						GUI.Label (new Rect (Screen.width / 2 - 300, Screen.height / 2 + 60, 600, 60), "Something");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 90, 400, 60), "Wake Up!");
					} else if (credTexStep >= 34 && credTexStep <= 36) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Parijat Mishra");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "www.soundcloud.com/parijat-mishra", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "4th Night");
					} else if (credTexStep >= 37 && credTexStep <= 39) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Pierlo");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "freemusicarchive.org/music/Pierlo", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Barbarian");
					} else if (credTexStep >= 40 && credTexStep <= 42) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Revolution Void");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "freemusicarchive.org/music/Revolution_Void", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "How Exciting");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Someone Else's Memories");
					} else if (credTexStep >= 43 && credTexStep <= 45) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Sergey Cheremisinov");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "www.s-cheremisinov.com", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Labyrinth");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 30, 400, 60), "Now You Are Here");
					} else if (credTexStep >= 46 && credTexStep <= 48) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 130, 400, 60), "Tours");
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 60), "", CDGUILargeTextSkin.FindStyle ("credTextSmall"));

						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "Enthusiast");
					} else if (credTexStep >= 49 && credTexStep <= 51) {
						GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 60), "THANK YOU FOR PLAYING!");
					} else if (credTexStep >= 52 && credTexStep <= 54) {
						GUI.DrawTexture (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200), titleCardTexture);
					}
					if (credTexStep == 1 || credTexStep == 4 || credTexStep == 7 || credTexStep == 10 || credTexStep == 13 || credTexStep == 16 || credTexStep == 19 || credTexStep == 22 || credTexStep == 25 || credTexStep == 28 || credTexStep == 31 || credTexStep == 34 || credTexStep == 37 || credTexStep == 40 || credTexStep == 43 || credTexStep == 46 || credTexStep == 49 || credTexStep == 52) {
						//Main Credit
						if (credTexColor.a < 1) {
							credTexColor = new Color (1, 1, 1, credTexColor.a + 0.25f * Time.deltaTime); 
						} else {
							credTexStep++;
						}
					} else if (credTexStep == 5 || credTexStep == 8 || credTexStep == 11 || credTexStep == 14 || credTexStep == 17 || credTexStep == 20 || credTexStep == 23 || credTexStep == 26 || credTexStep == 29 || credTexStep == 32 || credTexStep == 35 || credTexStep == 38 || credTexStep == 41 || credTexStep == 44 || credTexStep == 47) {
						if (credTexTimer < 15) {
							credTexTimer = credTexTimer + 1 * Time.deltaTime;
						} else {
							credTexStep++;
							credTexTimer = 0;
						}
					} else if (credTexStep == 3 || credTexStep == 6 || credTexStep == 9 || credTexStep == 12 || credTexStep == 15 || credTexStep == 18 || credTexStep == 21 || credTexStep == 24 || credTexStep == 27 || credTexStep == 30 || credTexStep == 33 || credTexStep == 36 || credTexStep == 39 || credTexStep == 42 || credTexStep == 45 || credTexStep == 48 || credTexStep == 51 || credTexStep == 54) {
						if (credTexColor.a > 0) {
							credTexColor = new Color (1, 1, 1, credTexColor.a - 0.25f * Time.deltaTime); 
						} else {
							credTexStep++;
						}
					} else if (credTexStep == 50) {
						if (credTexTimer < 10) {
							credTexTimer = credTexTimer + 1 * Time.deltaTime;
						} else {
							credTexStep++;
							credTexTimer = 0;
						}
					} else if (credTexStep == 2 || credTexStep == 53) {
						if (credTexTimer < 30) {
							credTexTimer = credTexTimer + 1 * Time.deltaTime;
						} else {
							credTexStep++;
							credTexTimer = 0;
						}
					} else {
						credTexStep = 0;
						creditsStep = 4;
						showExtras = false;
						if (achievements.achFinishCredits == false) {
							achievements.achFinishCredits = true;
							achievements.queueAch (27);
							achievements.saveAchievements ();
						}
					}


					//GUI.Box (new Rect (Screen.width / 2 - 160, 50, 320, 325), "");
					//creditsScrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 160, 50, 320, 325), creditsScrollPosition, new Rect(0,0,250,550));
					//GUI.color = Color.black;
					//GUI.Label (new Rect (0, 60, 300, 420), "Created, and lovingly developed by: \n Ethan 'the Drake' Casey \n Contact me: \n Email: EthanDrakeCasey@gmail.com \n Twitter: @EthanTheDrake \n \n Music by: \n \n Broke For Free \n www.brokeforfree.com \n \n Kai Engel \n www.kai-engel.com \n \n Chris Zabriskie \n www.chriszabriskie.com \n \n Sergey Cheremisinov \n sergeycheremisinov.bandcamp.com \n \n D SMILEZ \n www.soundcloud.com/d-smilez \n \n Revolution Void \n revolutionvoid.bandcamp.com \n \n Tours \n www.freemusicarchive.org/music/Tours \n \n Pierlo \n www.upitup.com/artists/18 \n \n Decktonic \n www.thisisdecktonic.com \n \n Possimiste \n www.possimiste.com \n \n Please support these music artists! \n \n Some 2D Graphics created by Michal Beno, Creative Stall, To Uyen, from Noun Project \n \n Thank you for playing!");
					//GUI.EndScrollView();
				}

			}

			if (GUI.tooltip != null && GUI.tooltip != "")
			{
				GUI.color = Color.white;
				if (Input.mousePosition.x > Screen.width - Screen.width/2)
				{
					GUI.Label(new Rect (Input.mousePosition.x - 210, Screen.height - Input.mousePosition.y - 10, 200, 60), GUI.tooltip, CDGUIMenuSkin.FindStyle("tooltipStyle"));
				}
				else
				{
					GUI.Label(new Rect (Input.mousePosition.x + 10, Screen.height - Input.mousePosition.y - 10, 200, 60), GUI.tooltip, CDGUIMenuSkin.FindStyle("tooltipStyle"));
				}
			}
		}
	}
}
