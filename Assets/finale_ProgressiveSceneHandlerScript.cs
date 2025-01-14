//Code written by Ethan 'the Drake' Casey
//Copyright 2016
//Contact me at "EthanDrakeCasey@gmail.com" for inquiries.
//=====================================================================

using UnityEngine;
using System.Collections;

public class finale_ProgressiveSceneHandlerScript : MonoBehaviour {

	settingsScript settings;
	leaderboardsHolder leaderboards;
	AchievementsScript achievements;
	public finale_progressive_playerScript playerScript;

	[Range(-10,10)]
	public float analogTurn;
	public bool useAnalogControls = false;
	public bool useButtonControls = false;
	bool pressRightButton;
	bool pressLeftButton;

    	public bool turnOffCubes;

	string gameVersion;
	public int qualityLevel;
	public GUISkin CDGUIGameSkin;
	public GUISkin CDGUIMenuSkin;
	public GUISkin CDGUILargeTextSkin;
	public Texture logoTex;
	public Texture mobile_pauseButtonTex;

    //powerup Textures
   	public Texture powerup_JumpTex;
    	public Texture powerup_BulletTex;
   	public Texture powerup_SlomoTex;
    	public Texture powerup_BubbleTex;


    	public GameObject settingsHolder;
	string ErrorSettingsHolderMissing = "No Settings Holder Found. The game must first load to the main menu to properly initialize. If you're getting this error and you aren't directly launching into the game level, then this is a bug. Loading menu...";

	int cubeMatPicker; //random value that gets assigned every frame that determines the color of the next cube spawned.
	GameObject newCube; //Game Object of the newest spawned cube. Updated every frame.
	GameObject newFarCube; //Game Object of newest spawned "far" (beyond XLimiters) cube. Updated every frame where player is far enough on the X axis.
	GameObject RedCubeREF; //Cube that new red cubes are copied from.
	GameObject GreenCubeREF;//Cube that new green cubes are copied from.
	GameObject BlueCubeREF;//Cube that new blue cubes are copied from.
	GameObject BlackCubeREF;//Cube that new black cubes are copied from.
	GameObject WhiteCubeREF;//Cube that new white cubes are copied from.
	GameObject RedBallREF;
	GameObject GreenBallREF;
	GameObject BlueBallREF;
    //powerup references
	GameObject powerup_JumpRef;
	GameObject powerup_ShootRef;
	GameObject powerup_SlowmoRef;
	GameObject powerup_BubbleRef;
	GameObject projectileRef;
	GameObject newProjectile;
	GameObject bubbleRef;
	GameObject newBubble;
	bool isPowerupSpawning;
	float secondsFloating;
	int jumpInt;
	int slowmoInt;
	float secondsSlowed;

    public int currentPowerup; //0 = none, 1 = jump, 2 = slomo, 3 = shoot, 4 = bubble

    //----
	public bool EnablePlayerControls = false;//false = No player control, true = player controls enabled
	public float cubeSpawnDistance = 200;
	public float cubeSpawnHeight = 200;

	public GameObject player; //player object
	GameObject gameCamera; //camera object
	UnityStandardAssets.ImageEffects.MotionBlur gameCameraBlur;
	GameObject floor; //floor object
	GameObject objLight; //light
	GameObject lightRotObj;
	GameObject initialCubeSpawner;
	public Vector3 playerPos; //tracks position of player
	public Vector3 playerRot; //tracks rotation of player (for animation)
	public Vector3 cameraPos; //tracks position of camera
	public Vector3 floorPos;  //trakcs position of floor
	Rigidbody rb; //player rigid body
	public Vector3 playerVelocity; //tracks player velocity
	float currentZVelocity; //current Z Velocity
	float currentXVelocity; //current X Velocity

	Component PlayerScript;
	public float gameOverTimer;
	public bool gameOverDoOnce;
	Color lightColor;

	float introFadeInTimer;
	public Color GUIColor = new Color (0, 0, 0);
	Color IntroColor1 = new Color (0, 0, 0);
	Color IntroColor2 = new Color (0, 0, 0);
	Color IntroColor3 = new Color (0, 0, 0);
	bool introShowLogo = false;
	bool introShowDifficulty = false;
	bool introShowModifiers = false;
	//public float modifierScoreMultiplier; MOVED TO SETTINGS SCRIPT
	string modifierString = "None";
	//modifiers-------------------------------------
	//----------------------------------------

	float initialDifficulty;
	float initialSpeedMult;
	bool initialAirdrop;
	int initialCubeType;
	int initialCameraType;
	bool initialDrunkDriver;
	bool initialPowerups;

	//---------Airdrop------------------------
	bool airdropAddRB;
	//----------------------------------------
	//----------------------------------------------
	GameObject currentSong;
	int musicPicker;
	GameObject gameOverCurrentSong;
	int gameOverMusicPicker;
	bool gameOver10sDoOnce = false;
	bool initGameOverFadeOut;

	bool initFadeIn = true;

	GameObject rightXLimiter;
	GameObject rightXLimiter_particle;
	GameObject leftXLimiter;
	GameObject leftXLimiter_particle;
	GameObject farCubeCut;
	GameObject nearCubeCut;
	int timesCubesCut;

	//Difficulty Vars//-------------------
	public float score = 0; //Score
	public float cubeCutScore = 0;
	public float scoreUpdateZCoord;
	public int difficulty = 1; //0 = Easy, 1 = Normal, 2 = Hard, 3 = Extreme
	string difficultyString;
	public int difficultyZVelocity = 25; // player's velocity based on difficulty
	public float difficultyCubeSpawnRate; //number of cubes to spawn in a minute.
	public float finalCubeSpawnTime; //how often a new cube spawns. Calculated by taking difficultyCubeSpawnRate over 60.
	//------------------------------------

	//Pause Menu Vars//
	public bool gamePaused;
	Vector3 pausePlayerVelocity;
	bool exitToMenu;
	bool exitToDesktop;
	float menuExitTimer;
	//--------------//

	//Developer Tool Vars//
	
	public bool HaveCheatsBeenEnabled = false;

	public bool showNumberOfActiveCubes;
	public int numberOfActiveCubes;
	//

	//1.1.0 vars//
	bool showSongSkip;
	float songSkipTimer;

	//progressive vars
	public int progressiveStep;
	public int currentWorld;
	GameObject starsREF;

	bool transition1;
	bool transition2;
	public bool transition3;
	public bool transition4;
	float transition4Timer;

	GameObject step1;
	GameObject step2;
	GameObject step4;
	GameObject step4_clouds;
	GameObject step5;
	GameObject step5_smoke;
	GameObject step6;
	GameObject step6_giantCube;
	GameObject step9;
	GameObject step10_1;
	GameObject step10_2;
	GameObject step11;
	GameObject step13;
	GameObject step15;
	GameObject step17;
	GameObject step19;

	GameObject world3_room;
	GameObject world3_lights;

	GameObject world4_blackHole;
	GameObject world4_songToFade;
	GameObject world4_particleSystems;
	public bool world4_moveBackParticles;

	GameObject dummyPlayer;
	GameObject intro_lookAtMeObj;
	GameObject finale_clouds;

	public bool doFinaleDropDown;

	ParticleSystem.VelocityOverLifetimeModule step4_volm;

	bool referenceCubesHaveRB;
	float pauseReturnTimescale;
	float indTime;
	bool mobile_pauseButtonPressed;
	bool mobile_powerupButtonPressed;	
	public int cubeXMin = -150;
	public int cubeXMax = 150;
	public float cubeSpawnRateMultiplier = 1f;

	void Awake (){
		settingsHolder = GameObject.Find ("settingsHolder");
		try {
			settings = settingsHolder.GetComponent<settingsScript> ();
			leaderboards = settingsHolder.GetComponent<leaderboardsHolder> ();
			achievements = settingsHolder.GetComponent<AchievementsScript> ();
		} catch {
			Debug.Log (ErrorSettingsHolderMissing);
			Application.LoadLevel("loading_menu");
		}

	}

	// Use this for initialization
	void Start () 
	{
		if (Cursor.visible == true)
		{
			Cursor.visible = false;
		}
		gameVersion = settings.gameVersion;
		settings.doMenuIntro = false;
		// we don't use modifiers in this level
		difficulty = 3;
		difficultyString = "Progressive";

		modifierString = "Progressive";

		//Set up for Progressive Mode
		//Get initial values so you can reset the player's modifiers when they leave progressive.
		initialDifficulty = settings.difficulty;
		initialSpeedMult = settings.modifierSpeedMultiplier;
		initialAirdrop = settings.modifierAirdrop;
		initialCameraType = settings.modifierCameraType;
		initialCubeType = settings.modifierCubeType;
		initialDrunkDriver = settings.modifierDrunkDriver;
		initialPowerups = settings.modifierPowerups;
		//
		settings.difficulty = 3;
		settings.modifierSpeedMultiplier = 1f;
		settings.modifierAirdrop = false;
		settings.modifierCameraType = 0;
		settings.modifierCubeType = 0;
		settings.modifierDrunkDriver = false;
		settings.modifierPowerups = false;
		settings.modifierProgressive = true;
		settings.modifierRandomizer = false;


		//Init difficulty settings//
		
		if (difficulty == 0)
		{
			difficultyCubeSpawnRate = 2000;
			difficultyZVelocity = 15;
		}
		else if (difficulty == 1)
		{
			difficultyCubeSpawnRate = 6000;
			difficultyZVelocity = 25;
		}
		else if (difficulty == 2)
		{
			difficultyCubeSpawnRate = 12000;
			difficultyZVelocity = 35;
		}
		else if (difficulty == 3)
		{
			difficultyCubeSpawnRate = 64000;
			difficultyZVelocity = 50;
		}
		
		finalCubeSpawnTime = 60/difficultyCubeSpawnRate;
		
		//---------------------------

		qualityLevel = settings.optionsPresetQuality;
		//Application.targetFrameRate = 60;

		RedCubeREF = GameObject.Find ("referenceRedCube");
		GreenCubeREF = GameObject.Find ("referenceGreenCube");
		BlueCubeREF = GameObject.Find ("referenceBlueCube");
		BlackCubeREF = GameObject.Find ("referenceBlackCube");
		WhiteCubeREF = GameObject.Find ("referenceWhiteCube");

		RedBallREF = GameObject.Find ("referenceRedBall");
		GreenBallREF = GameObject.Find ("referenceGreenBall");
		BlueBallREF = GameObject.Find ("referenceBlueBall");

        	powerup_JumpRef = GameObject.Find("referenceJumpPU");
       		powerup_BubbleRef = GameObject.Find("referenceBubblePU");
        	powerup_SlowmoRef = GameObject.Find("referenceSlomoPU");
        	powerup_ShootRef = GameObject.Find("referenceShootPU");

       		projectileRef = GameObject.Find("reference_projectile");
		bubbleRef = GameObject.Find ("reference_bubble");

		starsREF = GameObject.Find ("stars");

		dummyPlayer = GameObject.Find ("dummyPlayer");
		intro_lookAtMeObj = GameObject.Find ("finaleIntro_cameraLookAtMeObj");

		finale_clouds = GameObject.Find ("finale_clouds");

		step1 = GameObject.Find ("Step 1");
		step4 = GameObject.Find ("Step 4");
		step4.SetActive (false);
		step4_clouds = GameObject.Find ("step4_clouds");
		step5_smoke = GameObject.Find ("step5_smoke");
		step5_smoke.SetActive (false);
		step6 = GameObject.Find ("Step 6");
		step6_giantCube = GameObject.Find ("step6_giantCube");
		step6.SetActive (false);
		/*
		step5 = GameObject.Find ("Step 5");
		step5.SetActive (false);
		step7 = GameObject.Find ("Step 7");
		step7.SetActive (false);
		step9 = GameObject.Find ("Step 9");
		step9.SetActive (false);
		step10_1 = GameObject.Find ("Step 10-1");
		step10_1.SetActive (false);
		step10_2 = GameObject.Find ("Step 10-2");
		step10_2.SetActive (false);
		step11 = GameObject.Find ("Step 11");
		step11.SetActive (false);
		step13 = GameObject.Find ("Step 13");
		step13.SetActive (false);
		step15 = GameObject.Find ("Step 15");
		step15.SetActive (false);
		step17 = GameObject.Find ("Step 17");
		step17.SetActive (false);
		step19 = GameObject.Find ("Step 19");
		step19.SetActive (false);*/

		initialCubeSpawner = GameObject.Find ("initialCubeSpawn");

		farCubeCut = GameObject.Find ("cutCubePattern_far");
		nearCubeCut = GameObject.Find ("cutCubePattern_near");
		rightXLimiter = GameObject.Find ("XLimiter_right");
		leftXLimiter = GameObject.Find ("XLimiter_left");
		rightXLimiter_particle = GameObject.Find ("xLimiterParticle_Right");
		leftXLimiter_particle = GameObject.Find ("xLimiterParticle_Left");

		Debug.Log ("attempting player assignment");
		player = GameObject.Find ("playerREF");
		Debug.Log ("player assigned");
		Debug.Log ("player " + player);
		Debug.Log("playerREF " + GameObject.Find("playerREF"));
		playerScript = player.GetComponent<finale_progressive_playerScript> ();
		gameCamera = GameObject.Find ("cameraREF");
		gameCameraBlur = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur> ();
		floor = GameObject.Find ("floorPlane");
		objLight = GameObject.Find ("masterLight");
		lightRotObj = GameObject.Find ("lightRotationObject");
		player.transform.position = new Vector3 (0, 0, -18);
		gameCamera.transform.position = new Vector3 (0, 25, -23);
		rb = player.GetComponent<Rigidbody> ();
		//lightColor = objLight.GetComponent<Light>().color;

		if (settings.gameCamera == null)
		{
			settings.gameCamera = gameCamera;
		}

		if (settings.optionsPresetQuality <= 2 || settings.optionsDepthOfField == false)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
		}

		settings.checkScoreMultiplier ();
		pickMusic ();
		if (Application.isMobilePlatform || settings.simulateMobileOptimization) {
			doMobileOptimization ();
		}
		InvokeRepeating ("Update1s", 0, 1);
		InvokeRepeating ("Update5s", 0, 5);
		//3.0
		originalPlayerTurnResponsiveness = playerTurnResponsiveness; //for slowmo
	}

	void doMobileOptimization (){
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunShaftIntensity = 0;
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = false;

		cubeXMin = -100;
		cubeXMax = 100;
		cubeSpawnRateMultiplier = 1.5f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
		
	void checkForAchievements(){
	}

	void Update1s(){
		if (settings.modifierAirdrop && !referenceCubesHaveRB) {
			GameObject.Find ("referenceRedCube").AddComponent<Rigidbody> ();
			GameObject.Find ("referenceGreenCube").AddComponent<Rigidbody> ();
			GameObject.Find ("referenceBlueCube").AddComponent<Rigidbody> ();

			GameObject.Find ("referenceRedCube").GetComponent<Rigidbody> ().freezeRotation = true;
			GameObject.Find ("referenceGreenCube").GetComponent<Rigidbody> ().freezeRotation = true;
			GameObject.Find ("referenceBlueCube").GetComponent<Rigidbody> ().freezeRotation = true;

			GameObject.Find ("referenceRedCube").GetComponent<Rigidbody> ().useGravity = false;
			GameObject.Find ("referenceGreenCube").GetComponent<Rigidbody> ().useGravity = false;
			GameObject.Find ("referenceBlueCube").GetComponent<Rigidbody> ().useGravity = false;
			referenceCubesHaveRB = true;
		}

		if (!settings.modifierAirdrop && referenceCubesHaveRB) {
			Destroy (GameObject.Find ("referenceRedCube").GetComponent<Rigidbody> ());
			Destroy (GameObject.Find ("referenceGreenCube").GetComponent<Rigidbody> ());
			Destroy (GameObject.Find ("referenceBlueCube").GetComponent<Rigidbody> ());
			referenceCubesHaveRB = false;
		}
	}

	void Update5s(){
		checkForAchievements ();

		if (!playerScript.initiateGameOver && !gamePaused) {
			updateXLimiters();

			// If the player is below the speed the difficulty demands, raise him to that speed.
			//Debug.Log(playerVelocity.z != (difficultyZVelocity*settings.modifierSpeedMultiplier));
			if (playerVelocity.z != (difficultyZVelocity*settings.modifierSpeedMultiplier) && progressiveStep > 1)
			{
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, difficultyZVelocity*settings.modifierSpeedMultiplier);
				playerVelocity = rb.velocity;
			}
		}


		if ((playerPos.x > 4700 || playerPos.x < -4700) && (playerPos.z + 200 < 4800 && playerPos.z + 200 > -4800)) {
			//spawnFarCube();
			if (!IsInvoking ("spawnFarCube")) {
				InvokeRepeating ("spawnFarCube", 0, finalCubeSpawnTime * cubeSpawnRateMultiplier);
			}
		} else {
			if (IsInvoking ("spawnFarCube")) {
				CancelInvoke ("spawnFarCube");
			}
		}

		if ((playerPos.z + 200 > 4700) || (playerPos.z + 200 < -4700))
		{
			if (rightXLimiter_particle.activeSelf && rightXLimiter_particle.GetComponent<ParticleSystem>().enableEmission == true)
			{
				rightXLimiter_particle.GetComponent<ParticleSystem>().enableEmission = false;
			}
			if (leftXLimiter_particle.activeSelf && leftXLimiter_particle.GetComponent<ParticleSystem>().enableEmission == true)
			{
				leftXLimiter_particle.GetComponent<ParticleSystem>().enableEmission = false;
			}
		}
		else
		{
			if (rightXLimiter_particle.activeSelf && rightXLimiter_particle.GetComponent<ParticleSystem>().enableEmission == false)
			{
				rightXLimiter_particle.GetComponent<ParticleSystem>().enableEmission = true;
			}
			if (leftXLimiter_particle.activeSelf && leftXLimiter_particle.GetComponent<ParticleSystem>().enableEmission == false)
			{
				leftXLimiter_particle.GetComponent<ParticleSystem>().enableEmission = true;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (gamePaused)
		{
			if (Cursor.visible == false && (exitToMenu == false && exitToDesktop == false))
			{
				Cursor.visible = true;
			}

			CancelInvoke("spawnNewCube");
			if (currentSong != null)
			{
				//Debug.Log (Time.realtimeSinceStartup - indTime);
				if (currentSong.GetComponent<AudioSource>().volume > 0)
				{
					currentSong.GetComponent<AudioSource>().volume = currentSong.GetComponent<AudioSource>().volume - (Time.realtimeSinceStartup - indTime);
				}
				if (currentSong.GetComponent<AudioSource>().pitch > 0)
				{
					currentSong.GetComponent<AudioSource>().pitch = currentSong.GetComponent<AudioSource>().pitch - (Time.realtimeSinceStartup - indTime)*2;
				}
				indTime = Time.realtimeSinceStartup;
			}

			if (exitToMenu)
			{
				menuExit();
			}

			if (exitToDesktop)
			{
				desktopExit();
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().enabled = false;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = false;
				Time.timeScale = pauseReturnTimescale;
				pauseReturnTimescale = 0;
				gamePaused = false;
				mobile_pauseButtonPressed = false;
				//player.GetComponent<Rigidbody>().velocity = pausePlayerVelocity;
				//objLight.GetComponent<autoIntensity>().dayRotateSpeed.x = 1;
				//objLight.GetComponent<autoIntensity>().nightRotateSpeed.x = 1;
			}

		}
		else
		{
			if (Cursor.visible == true)
			{
				Cursor.visible = false;
			}

			if (playerPos.z >= 5000)
			{
				cubeCutScore = score;
				scoreUpdateZCoord = -4999;
				player.transform.position = new Vector3(playerPos.x, playerPos.y, -5000);
				timesCubesCut = timesCubesCut + 1;

			}
			/*if (playerPos.y < -0.5)
			{
				player.transform.position = new Vector3(playerPos.x, 0, playerPos.z);
			}*/
			
			playerPos = player.transform.position;
			playerRot = player.transform.eulerAngles;
			cameraPos = new Vector3(playerPos.x, playerPos.y + 2, playerPos.z - 5);
			if (progressiveStep != 3) {
				floorPos = new Vector3 (playerPos.x, floor.transform.position.y, playerPos.z + 25);
			}
			if (progressiveStep > 1 && progressiveStep < 6) {
				gameCamera.transform.position = cameraPos;	
			}
			//lightRotObj.transform.position = cameraPos;
			floor.transform.position = floorPos;
			playerVelocity = rb.velocity;

			if (progressiveStep < 4) {
				finale_clouds.transform.position = new Vector3 (playerPos.x, 50, playerPos.z-125);
			}

			if ((Input.GetKeyDown (KeyCode.Space) || mobile_powerupButtonPressed) && !gameOverDoOnce) {
				if (settings.modifierPowerups) {
					mobile_powerupButtonPressed = false;
					usePowerUp ();
				}
			}

			if (EnablePlayerControls == false && playerPos.z > 0 && progressiveStep > 1)
			{
				EnablePlayerControls = true;
			}
			
			if (gameOverDoOnce == false) 
			{
				//if (HaveCheatsBeenEnabled == false)
				//{
					if (playerPos.z/10 > scoreUpdateZCoord)
					{
						score = score + (1);
						scoreUpdateZCoord = playerPos.z/10 + 1;
					}
				//}
				//else
				//{
				//	score = 0;
				//}

				if (currentSong != null && slowmoInt == 0 && !transition3)
				{
					if (currentSong.GetComponent<AudioSource>().volume < 1)
					{
						currentSong.GetComponent<AudioSource>().volume = currentSong.GetComponent<AudioSource>().volume + Time.deltaTime;
					}
					else
					{
						currentSong.GetComponent<AudioSource>().volume = 1;
					}
					if (currentSong.GetComponent<AudioSource>().pitch < 1)
					{
						currentSong.GetComponent<AudioSource>().pitch = currentSong.GetComponent<AudioSource>().pitch + (2*Time.deltaTime);
					}
					else
					{
						currentSong.GetComponent<AudioSource>().pitch = 1;
					}
				}

			}
			
			if (initFadeIn)
			{
				gameFadeIn();
			}
			
			if (playerScript.initiateGameOver == true)
			{
				if (!initGameOverFadeOut)
				{
					gameOver();
					gameOverMusic();
				}
				else
				{
					gameOverFadeOut();
				}
			}
			else
			{
				if ((currentSong == null || currentSong.GetComponent<AudioSource>().isPlaying == false) && settings.optionsGameADropErrorEnabled)
				{
					pickMusic();
				}
				
				if 
				(
						(playerPos.z + cubeSpawnDistance < 4750 && playerPos.z + cubeSpawnDistance > -4750) &&
						(progressiveStep != 3 || playerPos.z + cubeSpawnDistance < -3000) &&
						(progressiveStep != 4 || playerPos.z + cubeSpawnDistance > -2700) &&
						(progressiveStep != 5 || playerPos.z + cubeSpawnDistance < -150) &&
						(progressiveStep != 6)
				)
				{
					if (IsInvoking("spawnNewCube") == false)
					{
						InvokeRepeating("spawnNewCube",0, finalCubeSpawnTime * cubeSpawnRateMultiplier);
					}
				}
				else
				{
					CancelInvoke("spawnNewCube");
				}
			
				updateCubeCutters();
				
				getPlayerInput();
			}

			progressiveStuff ();
		}

	}

	void progressiveStuff()
	{
		if (progressiveStep == 0) {
			//FADE IN
			if (!achievements.unlockedErrorSong && !GameObject.Find ("ADrop2").GetComponent<AudioSource> ().isPlaying) {
				pickMusic (0, false);
			}

			if (achievements.isContinuousProgressiveRun && score != settings.progressiveContinuousScore) {
				score = settings.progressiveContinuousScore;
			}

			gameCamera.transform.LookAt (dummyPlayer.transform.position);

			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity >= 0) {
				progressiveStep = 1;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = 0;
			}
		} else if (progressiveStep == 1) {
			//Camera comes down w/ dummyPlayer

			if (dummyPlayer.transform.position.y > gameCamera.transform.position.y) {
				if (dummyPlayer.GetComponent<Rigidbody> ().useGravity == false) {
					dummyPlayer.GetComponent<Rigidbody> ().useGravity = true;
				}
				gameCamera.transform.LookAt (dummyPlayer.transform.position);
				gameCamera.transform.position = new Vector3 (gameCamera.transform.position.x, gameCamera.transform.position.y - 2 * Time.deltaTime, gameCamera.transform.position.z);
			} else if (intro_lookAtMeObj.transform.position == Vector3.zero) {
				intro_lookAtMeObj.transform.position = dummyPlayer.transform.position;
				gameCamera.transform.LookAt (intro_lookAtMeObj.transform.position);

			} else if (gameCamera.transform.position.y >= 18) {
				gameCamera.transform.position = new Vector3 (gameCamera.transform.position.x, gameCamera.transform.position.y - 1 * Time.deltaTime, gameCamera.transform.position.z);
				intro_lookAtMeObj.transform.position = new Vector3 (intro_lookAtMeObj.transform.position.x, gameCamera.transform.position.y, intro_lookAtMeObj.transform.position.z);
				gameCamera.transform.LookAt (intro_lookAtMeObj.transform.position);
			} else if (gameCamera.transform.position.y >= player.transform.position.y + 2) {
				gameCamera.transform.position = new Vector3 (gameCamera.transform.position.x, gameCamera.transform.position.y - 10 * Time.deltaTime, gameCamera.transform.position.z);
				intro_lookAtMeObj.transform.position = new Vector3 (intro_lookAtMeObj.transform.position.x, gameCamera.transform.position.y, intro_lookAtMeObj.transform.position.z);
				gameCamera.transform.LookAt (intro_lookAtMeObj.transform.position);
			} else if (intro_lookAtMeObj.activeSelf) {
				intro_lookAtMeObj.SetActive (false);
				GameObject.Find ("finale_dustParticle").GetComponent<ParticleSystem> ().Play();
				//gameCamera.transform.eulerAngles = Vector3.zero;
				//gameCamera.transform.position = new Vector3(playerPos.x, playerPos.y + 4, playerPos.z);

				rb.velocity = new Vector3 (0, 0, difficultyZVelocity);

				progressiveStep = 2;
				progressiveDifficultyChange (3);
			}

			if (dummyPlayer.transform.position.y < 2 && dummyPlayer.activeSelf) {
				dummyPlayer.SetActive (false);
			}



		} else if (progressiveStep == 2) {
			//Player controls are enabled
			if (playerPos.z >= 500) {
				if (step1.activeSelf) {
					step1.SetActive (false);
				}
			}

			if (timesCubesCut >= 1) {
				progressiveStep = 3;

				settings.modifierAirdrop = true;
			}
		} else if (progressiveStep == 3) {

			if (playerPos.z >= -3500) {
				floorPos = new Vector3 (playerPos.x, floorPos.y, -3800);
				//finale_clouds.transform.position = new Vector3 (finale_clouds.transform.position.x, finale_clouds.transform.position.y, -3775);
				if (!step4.activeSelf) {
					step4.SetActive (true);
					step4.transform.position = new Vector3 (playerPos.x, step4.transform.position.y, step4.transform.position.z);
					step4_clouds.transform.position = new Vector3 (playerPos.x, -15, -2975);
					//step4_volm = finale_clouds.GetComponent<ParticleSystem> ().velocityOverLifetime;
					//step4_volm.z = step4_volm.z.constant + difficultyZVelocity;
				}

				if (playerPos.z - 175 >= -3000) {
					step4_clouds.transform.position = new Vector3 (playerPos.x, -10, playerPos.z - 175);
				}

			} else {
				floorPos = new Vector3 (playerPos.x, floorPos.y, playerPos.z);
				//finale_clouds.transform.position = new Vector3 (playerPos.x, 50, playerPos.z-125);
			}

			if (playerPos.z >= -2800) {
				progressiveStep = 4;

				Destroy (finale_clouds);
				floor.GetComponent<Renderer> ().enabled = false;

				settings.modifierAirdrop = false;
				settings.modifierCubeType = 2;

			}
			
		} else if (progressiveStep == 4) {
			step4_clouds.transform.position = new Vector3 (playerPos.x, -10, playerPos.z - 175);

			if (playerPos.z >= -2500) {
				if (step4.activeSelf) {
					step4.SetActive (false);
				}
			}

			if (timesCubesCut >= 2) {
				progressiveStep = 5;

				settings.modifierAirdrop = true;
				doFinaleDropDown = true;
			}
		} else if (progressiveStep == 5) {
			step4_clouds.transform.position = new Vector3 (playerPos.x, step4_clouds.transform.position.y, playerPos.z - 175);

			if (doFinaleDropDown && playerPos.z >= -4800) {
				if (floor.GetComponent<BoxCollider> ().enabled) {
					floor.GetComponent<BoxCollider> ().enabled = false;
				}
				finaleDropDown ();
			}

			if (playerPos.z >= -2900) {
				if (!step5_smoke.activeSelf) {
					step5_smoke.SetActive (true);
				}
			}

			if (playerPos.z >= -500) {
				if (!step6.activeSelf) {
					step6.SetActive (true);
					step6.transform.position = new Vector3 (playerPos.x, step6.transform.position.y, step6.transform.position.z);
					//step6_giantCube.AddComponent<finale_progressive_cubeDeleteScript> ();
				}
				if (playerPos.z <= -300) {
					step6.transform.position = new Vector3 (playerPos.x, step6.transform.position.y, step6.transform.position.z);
				}
			}

			if (playerPos.z >= 300) {
				progressiveStep = 6;
			}
		} else if (progressiveStep == 6) {
			if (achievements.isContinuousProgressiveRun && score != 0) {
				leaderboards.recordScore (score);
				score = 0;
			}

			if (!achievements.achProgressive5) {
				achievements.achProgressive5 = true;
				achievements.queueAch (22);
				achievements.saveAchievements ();

				achievements.unlockedErrorSong = true;
				settings.optionsGameADropErrorEnabled = true;
				settings.optionsMenuADropErrorEnabled = false;
				settings.saveSettings ();
			}
			if (!achievements.achProgressiveContinuous && achievements.isContinuousProgressiveRun) {
				achievements.achProgressiveContinuous = true;
				achievements.queueAch (23);
				achievements.saveAchievements ();
			}

			if (player.GetComponent<MeshCollider> ().enabled) {
				player.GetComponent<MeshCollider> ().enabled = false;
			}

			if (objLight.GetComponent<autoIntensity> ().dayAtmosphereThickness < 1) {
				objLight.GetComponent<autoIntensity> ().dayAtmosphereThickness = objLight.GetComponent<autoIntensity> ().dayAtmosphereThickness + 1 * Time.deltaTime;
				objLight.GetComponent<autoIntensity> ().nightAtmosphereThickness = objLight.GetComponent<autoIntensity> ().nightAtmosphereThickness + 1 * Time.deltaTime;
			}

			if (step5_smoke.GetComponent<ParticleSystem> ().maxParticles > 0) {
				step5_smoke.GetComponent<ParticleSystem> ().maxParticles = step5_smoke.GetComponent<ParticleSystem> ().maxParticles - 1000;
			}

			if (gameCamera.transform.position.y < 25) {
				gameCamera.transform.position = new Vector3 (gameCamera.transform.position.x, gameCamera.transform.position.y + 2 * Time.deltaTime, gameCamera.transform.position.z);
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity - 0.25f * Time.deltaTime;
			} else if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity > -2) {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity - 0.25f * Time.deltaTime;
			} else {
				progressiveReset ();
				Time.timeScale = 1;
				Application.LoadLevel("loading_menu");
			}


		}
	}

	void finaleDropDown(){
		if (step4_clouds.transform.position.y < 40) {
			step4_clouds.transform.position = new Vector3 (step4_clouds.transform.position.x, step4_clouds.transform.position.y + 5 * Time.deltaTime, step4_clouds.transform.position.z);
		} else {
			step4_clouds.transform.position = new Vector3 (step4_clouds.transform.position.x, 40, step4_clouds.transform.position.z);
			doFinaleDropDown = false;
		}
	}

	void progressiveDifficultyChange(int newDifficulty)
	{
		settings.difficulty = newDifficulty;
		difficulty = newDifficulty;
		if (newDifficulty == 0)
		{
			difficultyCubeSpawnRate = 2000;
			difficultyZVelocity = 15;
		}
		else if (newDifficulty == 1)
		{
			difficultyCubeSpawnRate = 6000;
			difficultyZVelocity = 25;
		}
		else if (newDifficulty == 2)
		{
			difficultyCubeSpawnRate = 12000;
			difficultyZVelocity = 35;
		}
		else if (newDifficulty == 3)
		{
			difficultyCubeSpawnRate = 64000;
			difficultyZVelocity = 50;
		}

		finalCubeSpawnTime = 60/difficultyCubeSpawnRate;

	}

	void progressiveReset()
	{
		settings.difficulty = initialDifficulty;
		settings.modifierSpeedMultiplier = initialSpeedMult;
		settings.modifierAirdrop = initialAirdrop;
		settings.modifierCubeType = initialCubeType;
		settings.modifierCameraType = initialCameraType;
		settings.modifierDrunkDriver = initialDrunkDriver;
		settings.modifierPowerups = initialPowerups;
	}

	float slowmoAmt = 0.5f;
	float slowmoWindDownTime = 1.0f;
	float slowmoWindUpTime = 4.0f;
	float slowmoLength = 6.0f;
	float originalPlayerTurnResponsiveness;
	void powerupSlowmo()
	{
		playerTurnResponsiveness = originalPlayerTurnResponsiveness / Time.timeScale;
		if (slowmoInt == 1)
		{
			if (Time.timeScale > slowmoAmt)
			{
				Time.timeScale = Time.timeScale - (Time.deltaTime/slowmoWindDownTime);
				if (currentSong != null)
				{
					currentSong.GetComponent<AudioSource>().pitch = Time.timeScale;
				}
			}
			else
			{
				slowmoInt = 2;
			}
		}
		else if (slowmoInt == 2)
		{
			if (secondsSlowed >= slowmoLength)
			{
				slowmoInt = 3;
				secondsSlowed = 0;
			}
			else
			{
				secondsSlowed = secondsSlowed + Time.deltaTime;
			}
		}
		else if (slowmoInt == 3)
		{
			if (Time.timeScale < 1)
			{
				Time.timeScale = Time.timeScale + (Time.deltaTime/slowmoWindUpTime);
				if (currentSong != null)
				{
					currentSong.GetComponent<AudioSource>().pitch = Time.timeScale;
				}
			}
			else
			{
				Time.timeScale = 1;
				if (currentSong != null)
				{
					currentSong.GetComponent<AudioSource>().pitch = 1;
				}
				slowmoInt = 0;
			}
		}
	}

	void powerupJump()
    {
        if (jumpInt == 1)
        {
            player.transform.position = new Vector3(playerPos.x, playerPos.y + 4 * Time.deltaTime, playerPos.z);
            if (playerPos.y > 1)
            {
                jumpInt = 2;
            }
        }
        else if (jumpInt == 2)
        {
            player.transform.position = new Vector3(playerPos.x, playerPos.y + 2 * Time.deltaTime, playerPos.z);
            if (playerPos.y >= 2)
            {
                jumpInt = 3;
            }
        }
        else if (jumpInt == 3)
        {
            secondsFloating += Time.deltaTime;
            Debug.Log(secondsFloating);
            if (secondsFloating >= 0.25f)
            {
                jumpInt = 4;
                secondsFloating = 0;
            }
        }
        else if (jumpInt == 4)
        {
            player.transform.position = new Vector3(playerPos.x, playerPos.y - 2 * Time.deltaTime, playerPos.z);
            if (playerPos.y <= 1)
            {
                jumpInt = 5;
            }
        }
        else if (jumpInt == 5)
        {
            player.transform.position = new Vector3(playerPos.x, playerPos.y - 4 * Time.deltaTime, playerPos.z);
            if (playerPos.y <= 0)
            {
                player.transform.position = new Vector3(playerPos.x, 0, playerPos.z);
                jumpInt = 0;
            }
        }
    }

    void usePowerUp()
	{
		if (currentPowerup != 0) {
			Debug.Log ("using powerup");
			if (currentPowerup == 1) {
				//Jump
				//playerVelocity = new Vector3(rb.velocity.x, 6, rb.velocity.z);
				//rb.useGravity = true;
				// jumpWatchForYReset = true;
				if (jumpInt == 0) {
					jumpInt = 1;
					GameObject.Find ("jumpSFX").GetComponent<AudioSource> ().Play ();
					currentPowerup = 0;
				}
               
			} else if (currentPowerup == 2) {
				//Slo Mo
				if (slowmoInt == 0) {
					slowmoInt = 1;
					currentPowerup = 0;
				}
			} else if (currentPowerup == 3) {
				//Shoot
				newProjectile = Instantiate (projectileRef);
				newProjectile.transform.position = new Vector3 (playerPos.x, playerPos.y, playerPos.z + 4);
				newProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 100);
				newProjectile.AddComponent<finale_projectileScript> ();
				GameObject.Find ("shootSFX").GetComponent<AudioSource> ().Play ();
				currentPowerup = 0;
			} else if (currentPowerup == 4) {
				//Bubble

				if (player.GetComponent<MeshCollider> ().enabled && newBubble == null) {
					newBubble = Instantiate (bubbleRef);
					newBubble.transform.position = playerPos;
					newBubble.AddComponent<bubbleScript> ();
					currentPowerup = 0;
				}
			}
		}
	}


	void spawnNewCube()
	{
 
		if (settings.modifierAirdrop) {
			if (difficulty == 0) {
				cubeSpawnDistance = 75;
				cubeSpawnHeight = 35;
				if (settings.modifierSpeedMultiplier == 0.5f) {
					cubeSpawnDistance = cubeSpawnDistance - 20;
					//print (0.50);
				} else if (settings.modifierSpeedMultiplier == 0.75f) {
					cubeSpawnDistance = cubeSpawnDistance - 5;
					//print (0.75);
				} else if (settings.modifierSpeedMultiplier == 1.25) {
					cubeSpawnDistance = cubeSpawnDistance + 5;
					//print (1.25);
				} else if (settings.modifierSpeedMultiplier == 1.5f) {
					cubeSpawnDistance = cubeSpawnDistance + 20;
					//print (1.50);
				} else if (settings.modifierSpeedMultiplier == 1.75f) {
					cubeSpawnDistance = cubeSpawnDistance + 35;
					//print (1.75);
				} else if (settings.modifierSpeedMultiplier == 2) {
					cubeSpawnDistance = cubeSpawnDistance + 50;
					//print (2.00);
				}
			} else if (difficulty == 1) {
				cubeSpawnDistance = 125;
				cubeSpawnHeight = 50;
				if (settings.modifierSpeedMultiplier == 0.5f) {
					cubeSpawnDistance = cubeSpawnDistance - 50;
					//print (0.50);
				} else if (settings.modifierSpeedMultiplier == 0.75f) {
					cubeSpawnDistance = cubeSpawnDistance - 25;
					//print (0.75);
				} else if (settings.modifierSpeedMultiplier == 1.25) {
					cubeSpawnDistance = cubeSpawnDistance + 20;
					//print (1.25);
				} else if (settings.modifierSpeedMultiplier == 1.5f) {
					cubeSpawnDistance = cubeSpawnDistance + 40;
					//print (1.50);
				} else if (settings.modifierSpeedMultiplier == 1.75f) {
					cubeSpawnDistance = cubeSpawnDistance + 60;
					//print (1.75);
				} else if (settings.modifierSpeedMultiplier == 2) {
					cubeSpawnDistance = cubeSpawnDistance + 75;
					//print (2.00);
				}
			} else if (difficulty == 2) {
				cubeSpawnDistance = 135;
				cubeSpawnHeight = 45;
				if (settings.modifierSpeedMultiplier == 0.5f) {
					cubeSpawnDistance = cubeSpawnDistance - 50;
					//print (0.50);
				} else if (settings.modifierSpeedMultiplier == 0.75f) {
					cubeSpawnDistance = cubeSpawnDistance - 35;
					//print (0.75);
				} else if (settings.modifierSpeedMultiplier == 1.25) {
					cubeSpawnDistance = cubeSpawnDistance + 35;
					//print (1.25);
				} else if (settings.modifierSpeedMultiplier == 1.5f) {
					cubeSpawnDistance = cubeSpawnDistance + 45;
					//print (1.50);
				} else if (settings.modifierSpeedMultiplier == 1.75f) {
					cubeSpawnDistance = cubeSpawnDistance + 70;
					//print (1.75);
				} else if (settings.modifierSpeedMultiplier == 2) {
					cubeSpawnDistance = cubeSpawnDistance + 95;
					//print (2.00);
				}
			} else if (difficulty == 3) {
				cubeSpawnDistance = 180;
				cubeSpawnHeight = 50;
				if (settings.modifierSpeedMultiplier == 0.5f) {
					cubeSpawnDistance = cubeSpawnDistance - 75;
					//print (0.50);
				} else if (settings.modifierSpeedMultiplier == 0.75f) {
					cubeSpawnDistance = cubeSpawnDistance - 45;
					//print (0.75);
				} else if (settings.modifierSpeedMultiplier == 1.25) {
					cubeSpawnDistance = cubeSpawnDistance + 50;
					//print (1.25);
				} else if (settings.modifierSpeedMultiplier == 1.5f) {
					cubeSpawnDistance = cubeSpawnDistance + 100;
					//print (1.50);
				} else if (settings.modifierSpeedMultiplier == 1.75f) {
					cubeSpawnDistance = cubeSpawnDistance + 150;
					//print (1.75);
				} else if (settings.modifierSpeedMultiplier == 2) {
					cubeSpawnDistance = cubeSpawnDistance + 210;
					//print (2.00);
				}
			}

		} else {
			if (difficulty == 0) {
				cubeSpawnDistance = 100;
			} else if (difficulty == 1) {
				cubeSpawnDistance = 200;
			} else if (difficulty == 2) {
				cubeSpawnDistance = 200;
			} else if (difficulty == 3) {
				cubeSpawnDistance = 200;
			}
			cubeSpawnDistance = cubeSpawnDistance * settings.modifierSpeedMultiplier;
			if (cubeSpawnDistance > 200) {
				cubeSpawnDistance = 200;
			}
		}

		if (settings.modifierPowerups) {
			if (initialCubeSpawner == null && playerPos.z <= 0 && timesCubesCut == 0) {
				return;
			}

			cubeMatPicker = Random.Range (1, 201); // have a 0.5% chance to spawn a power up
			if (cubeMatPicker == 1) {
				isPowerupSpawning = true;
				cubeMatPicker = Random.Range (1, 5); //once the chance succeeds, randomly pick a powerup.
				if (cubeMatPicker == 1) {
					newCube = Instantiate (powerup_JumpRef);
				} else if (cubeMatPicker == 2) {
					newCube = Instantiate (powerup_BubbleRef);
				} else if (cubeMatPicker == 3) {
					newCube = Instantiate (powerup_SlowmoRef);
				} else if (cubeMatPicker == 4) {
					newCube = Instantiate (powerup_ShootRef);
				}
				newCube.AddComponent<progressive_PowerUpScript> ();
			} else {
				isPowerupSpawning = false;

				//generate and place new cubes

				if (settings.modifierCubeType == 0) {
					cubeMatPicker = Random.Range (1, 4);
				} else if (settings.modifierCubeType == 1) {
					cubeMatPicker = Random.Range (4, 7);
				} else if (settings.modifierCubeType == 2) {
					cubeMatPicker = Random.Range (1, 7);
				}

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
			}
		} else {

			if (initialCubeSpawner == null && playerPos.z <= 0 && timesCubesCut == 0) {
				return;
			}
			//generate and place new cubes

			if (settings.modifierCubeType == 0) {
				cubeMatPicker = Random.Range (1, 4);
			} else if (settings.modifierCubeType == 1) {
				cubeMatPicker = Random.Range (4, 7);
			} else if (settings.modifierCubeType == 2) {
				cubeMatPicker = Random.Range (1, 7);
			}

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
		}

		if (!settings.modifierPowerups || !isPowerupSpawning) {
			newCube.GetComponent<finale_progressive_cubeDeleteScript> ().isReferenceCube = false;
		}
        
		if (timesCubesCut > 0 || (playerPos.z + cubeSpawnDistance > cubeSpawnDistance && playerPos.z > 0 && playerPos.z + cubeSpawnDistance < 4750 && playerPos.z + cubeSpawnDistance > -4750)) {
			if (settings.modifierAirdrop) {
				newCube.transform.position = new Vector3 (playerPos.x + ((Random.Range (cubeXMin, cubeXMax))), cubeSpawnHeight, (playerPos.z + cubeSpawnDistance));
			} else if (settings.modifierPowerups && isPowerupSpawning) {
				newCube.transform.position = new Vector3 (playerPos.x + ((Random.Range (cubeXMin, cubeXMax))), 1, (playerPos.z + cubeSpawnDistance));
			} else {
				newCube.transform.position = new Vector3 (playerPos.x + ((Random.Range (cubeXMin, cubeXMax))), 1 / 2, (playerPos.z + cubeSpawnDistance));

			}
		} else if (initialCubeSpawner != null) {
			if (settings.modifierPowerups && isPowerupSpawning) {
				newCube.transform.position = new Vector3 (Random.Range (cubeXMin, cubeXMax), 1, initialCubeSpawner.transform.position.z);
			} else {
				newCube.transform.position = new Vector3 (Random.Range (cubeXMin, cubeXMax), 1 / 2, initialCubeSpawner.transform.position.z);
			}
		}

	}

	void spawnFarCube()
	{
		newFarCube = Instantiate (BlackCubeREF);
		newFarCube.AddComponent<progressive_FarCubeDeleteScript> ();
		if (playerPos.x > 4700)
		{
			newFarCube.transform.position = new Vector3 (Random.Range(5001, 5250), 1/2, playerPos.z + 200);
		}
		else if (playerPos.x < -4700)
		{
			newFarCube.transform.position = new Vector3 (Random.Range(-5001, -5250), 1/2, playerPos.z + 200);
		}
	}

	void updateCubeCutters()
	{
		if (progressiveStep != 10 && progressiveStep != 11 && progressiveStep != 20) {
			if (playerPos.z >= 4200)
			{
				if (farCubeCut.activeSelf == false)
				{
					farCubeCut.SetActive(true);
					if (playerPos.x > 4980)
					{
						farCubeCut.transform.position = new Vector3 (4980, farCubeCut.transform.position.y, farCubeCut.transform.position.z);
					}
					else if (playerPos.x < -4980)
					{
						farCubeCut.transform.position = new Vector3 (-4980, farCubeCut.transform.position.y, farCubeCut.transform.position.z);
					}
					else
					{
						farCubeCut.transform.position = new Vector3 (playerPos.x, farCubeCut.transform.position.y, farCubeCut.transform.position.z);
					}
				}		
			}
			else
			{
				if (farCubeCut.activeSelf == true)
				{
					farCubeCut.SetActive(false);
				}
			}

			if (playerPos.z <= -4700)
			{
				if (nearCubeCut.activeSelf == false)
				{
					nearCubeCut.SetActive(true);
					nearCubeCut.transform.position = new Vector3 (farCubeCut.transform.position.x, nearCubeCut.transform.position.y, nearCubeCut.transform.position.z);
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

	}

	void updateXLimiters()
	{
		if (playerPos.x > 4500)
		{
			if (rightXLimiter.activeSelf == false && world3_room.activeSelf != true)
			{
				rightXLimiter.SetActive(true);
			}
		}
		else
		{
			if (rightXLimiter.activeSelf == true)
			{
				rightXLimiter.SetActive(false);
			}
		}
		if (playerPos.x < -4500)
		{
			if (leftXLimiter.activeSelf == false && world3_room.activeSelf != true)
			{
				leftXLimiter.SetActive(true);
			}
		}
		else
		{
			if (leftXLimiter.activeSelf == true)
			{
				leftXLimiter.SetActive(false);
			}
		}
	}

	void gameFadeIn()
	{
		if (introFadeInTimer >= 2)
		{
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay>().intensity < 0)
			{
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay>().intensity = (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay>().intensity + (Time.deltaTime*settings.modifierSpeedMultiplier));
			}
			else
			{
				initFadeIn = false;
			}
		}
		introFadeInTimer = introFadeInTimer + Time.deltaTime;
	}

	void gameOver()
	{
		if (gameOverDoOnce == false)
		{
			if (achievements.isContinuousProgressiveRun) {
				leaderboards.recordScore(score);
				achievements.isContinuousProgressiveRun = false;
			}

			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().enabled = true;
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = true;
			playerScript.initBrighten = true;
			playerScript.initDarken = false;
			//initGameOverMusic = true;
			CancelInvoke("spawnNewCube");
			rb.useGravity = true;
			gameOverDoOnce = true;
		}
		if (gameOverTimer < 5)
		{
			if (gameOverTimer > 3)
			{
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset - (Time.deltaTime)/3;
				//gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x + (Time.deltaTime);
				//gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y + (Time.deltaTime);
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().iterations = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().iterations + 1;
			}
			else if (gameOverTimer > 1)
			{
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset - (Time.deltaTime)/3;
				//gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x + (Time.deltaTime)/2;
				//gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y + (Time.deltaTime)/2;
			}
			else if (gameOverTimer > 0)
			{
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset - (Time.deltaTime)/6;
			}

		}
		else
		{

			if (gameOverTimer >= 10 && gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset < 0)
			{
				if (gameOver10sDoOnce != true)
				{
					gameCamera.transform.eulerAngles = new Vector3(300,270,0);
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().iterations = 1;
					gameOver10sDoOnce = true;
				}
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset + (Time.deltaTime)/6;
			}
			if (Input.anyKey)
			{
				//Debug.Log ("click");
				playerScript.initBrighten = false;
				playerScript.initDarken = true;
				initGameOverFadeOut = true;
			}
		}
		gameOverTimer = gameOverTimer + Time.deltaTime;

		if (playerPos.y > (0))
		{
			player.transform.position = new Vector3(playerPos.x, playerPos.y - Time.deltaTime, playerPos.z);
		}
		if (playerRot.x < (90))
		{
			player.transform.eulerAngles = new Vector3(playerRot.x + Time.deltaTime, playerRot.y,playerRot.z);
		}
		if (Input.GetKey (KeyCode.A))
		{
			player.transform.Rotate(Vector3.forward*Time.deltaTime*100);
		}
		if (Input.GetKey (KeyCode.D))
		{
			player.transform.Rotate(Vector3.back*Time.deltaTime*100);
		}
	}

	void gameOverFadeOut()
	{
		if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset <= -1 && gameOverCurrentSong.GetComponent<AudioSource>().volume <= 0)
		{
			progressiveReset ();
			Time.timeScale = 1;
			if (settings.progressiveCurrentCheckpoint == 4) {
				Application.LoadLevel ("game_progressive_finale");
			} else {
				Application.LoadLevel ("game_progressive");
			}

		}
		gameOverCurrentSong.GetComponent<AudioSource> ().volume = gameOverCurrentSong.GetComponent<AudioSource> ().volume - Time.deltaTime/2;
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale> ().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale> ().rampOffset - Time.deltaTime/2;

	}

	void menuExit()
	{
		if (Cursor.visible == true)
		{
			Cursor.visible = false;
		}
		if (menuExitTimer < 3)
		{
			gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x - (24 * Time.deltaTime), gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset - (Time.deltaTime)/3;
			menuExitTimer = menuExitTimer + Time.deltaTime;
		}
		else
		{
			progressiveReset ();
			Time.timeScale = 1;
			Application.LoadLevel("loading_menu");
		}
	}

	void desktopExit()
	{
		if (Cursor.visible == true)
		{
			Cursor.visible = false;
		}
		if (menuExitTimer < 3)
		{
			gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x - (24 * Time.deltaTime), gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset - (Time.deltaTime)/3;
			menuExitTimer = menuExitTimer + Time.deltaTime;
		}
		else
		{
			Cursor.visible = true;
			Application.Quit();
		}
	}

	//3.0 new vars for control tuning
	float playerTurnResponsiveness = 40.0F;
	float playerMaxTurnVelocity = 15.0F;
	void getPlayerInput()
	{
		if (EnablePlayerControls)
		{
			if (!useAnalogControls && Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
			{
				if (Input.GetKey(KeyCode.A) && (!Input.GetKey(KeyCode.D)))
				{
					if (playerVelocity.x > -playerMaxTurnVelocity)
					{
						rb.velocity = new Vector3(playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					}
					else if (playerVelocity.x < -playerMaxTurnVelocity)
					{
						rb.velocity = new Vector3(-playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					}
				}
				if (Input.GetKey(KeyCode.D) && (!Input.GetKey(KeyCode.A)))
				{
					if (playerVelocity.x < playerMaxTurnVelocity)
					{
						rb.velocity = new Vector3(playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					}
					else if (playerVelocity.x > playerMaxTurnVelocity)
					{
						rb.velocity = new Vector3(playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					}
				}

				if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
				{
					if (playerVelocity.x < 0)
					{
						rb.velocity = new Vector3(playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					}
					if (playerVelocity.x > 0)
					{
						rb.velocity = new Vector3(playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					}
					if (playerVelocity.x > -1 && playerVelocity.x < 1)
					{
						rb.velocity = new Vector3(0, playerVelocity.y, playerVelocity.z);
					}

				}
				player.transform.eulerAngles = new Vector3(0, 0, -rb.velocity.x * 2);
			}
			else if (Application.platform == RuntimePlatform.Android || settings.simulateMobileOptimization)
			{
				if (settings.mobileControlScheme == 0 || useAnalogControls)
				{
					if (Application.platform == RuntimePlatform.Android)
					{
						analogTurn = Input.acceleration.x * playerTurnResponsiveness;
					}

					if (analogTurn >= -playerMaxTurnVelocity && analogTurn <= playerMaxTurnVelocity)
					{
						rb.velocity = new Vector3(analogTurn, playerVelocity.y, playerVelocity.z);
					}
					else if (analogTurn < -playerMaxTurnVelocity)
					{
						rb.velocity = new Vector3(-playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					}
					else if (analogTurn > playerMaxTurnVelocity)
					{
						rb.velocity = new Vector3(playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					}
					player.transform.eulerAngles = new Vector3(0, 0, -rb.velocity.x * 2);
				}
				else if (settings.mobileControlScheme == 1 || useButtonControls)
				{
					if (pressLeftButton && (!pressRightButton))
					{
						if (playerVelocity.x > -playerMaxTurnVelocity)
						{
							rb.velocity = new Vector3(playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						}
						else if (playerVelocity.x < -playerMaxTurnVelocity)
						{
							rb.velocity = new Vector3(-playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
						}
					}
					if (pressRightButton && (!pressLeftButton))
					{
						if (playerVelocity.x < playerMaxTurnVelocity)
						{
							rb.velocity = new Vector3(playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						}
						else if (playerVelocity.x > playerMaxTurnVelocity)
						{
							rb.velocity = new Vector3(playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
						}
					}

					if ((!pressLeftButton && !pressRightButton) || (pressLeftButton && pressRightButton))
					{
						if (playerVelocity.x < 0)
						{
							rb.velocity = new Vector3(playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						}
						if (playerVelocity.x > 0)
						{
							rb.velocity = new Vector3(playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						}
						if (playerVelocity.x > -1 && playerVelocity.x < 1)
						{
							rb.velocity = new Vector3(0, playerVelocity.y, playerVelocity.z);
						}

					}
					player.transform.eulerAngles = new Vector3(0, 0, -rb.velocity.x * 2);
				}
			}


			if ((Input.GetKeyDown (KeyCode.Escape) || mobile_pauseButtonPressed) && EnablePlayerControls && exitToMenu == false && exitToDesktop == false) {
				gamePaused = true;
				pauseReturnTimescale = Time.timeScale;
				Time.timeScale = 0;
				indTime = Time.realtimeSinceStartup;
			}
		}
	}

	void pickMusic(int musicPicker = 0, bool checkEnabled = true)	// picks a random music track to play
	{

		if (progressiveStep > 1) {
			return;
		}

		Debug.Log (musicPicker + ", " + checkEnabled);
		
		if (currentSong != null)
		{
			currentSong.GetComponent<AudioSource>().Stop();
			Debug.Log ("stopping " + currentSong.name);
		}
		
		if (checkEnabled)
		{
			if (!settings.optionsGameADropErrorEnabled) {
				return;
			}
		}
		
		if (musicPicker == 0)
		{
			musicPicker = 1;
		}

		if (musicPicker == 1)
		{
			currentSong = GameObject.Find ("ADrop2");
			currentSong.GetComponent<AudioSource>().Play();
		}
		
	}

	void gameOverMusic()
	{
		if (currentSong != null)
		{
			if (currentSong.GetComponent<AudioSource>().volume > 0)
			{
				currentSong.GetComponent<AudioSource>().volume = currentSong.GetComponent<AudioSource>().volume - Time.deltaTime/2;
			}
			if (currentSong.GetComponent<AudioSource>().pitch > 0)
			{
				currentSong.GetComponent<AudioSource>().pitch = currentSong.GetComponent<AudioSource>().pitch - Time.deltaTime;
			}
		}


		if (gameOverCurrentSong == null || !gameOverCurrentSong.GetComponent<AudioSource>().isPlaying)
		{
			gameOverMusicPicker = Random.Range(1, 3);
			//Debug.Log ("go: " + gameOverMusicPicker);
			if (gameOverMusicPicker == 1)
			{
				gameOverCurrentSong = GameObject.Find ("gameOverSergey1");
				gameOverCurrentSong.GetComponent<AudioSource>().Play();
			}
			else if (gameOverMusicPicker == 2)
			{
				gameOverCurrentSong = GameObject.Find ("gameOverSergey2");
				gameOverCurrentSong.GetComponent<AudioSource>().Play();
			}
		}
	}

	void OnGUI ()
	{
		if (!settings.screenshotMode)
		{
			GUI.skin = CDGUIGameSkin;

			GUI.color = Color.white;


			if (gamePaused && exitToMenu == false && exitToDesktop == false)
			{

				if (leaderboards.showLeaderboardsError)
				{
					if (Cursor.visible == false)
					{
						Cursor.visible = true;
					}

					GUI.skin = CDGUIMenuSkin;
					GUI.Box(new Rect(Screen.width/2 - 75, 50, 150, 40),"");
					GUI.color = Color.black;
					GUI.skin = CDGUILargeTextSkin;
					GUI.Label(new Rect(Screen.width/2 - 60, 45, 120, 40),"Error");
					GUI.skin = CDGUIMenuSkin;
					GUI.color = Color.white;
					GUI.Box (new Rect (Screen.width / 2 - 100, 80, 200, 225), "");
					
					if (settings.showSettingsResetScreen)
					{
						GUI.Label (new Rect (Screen.width / 2 - (150/2), 120, 150, 100), "Your saved settings save file was corrupted or damaged.\n\nUnfortunately, this means that a new file had to be generated, and all your saved settings have been reset to default.\n\nA log has been created in the game directory.");
					}
					else if (leaderboards.showLeaderboardsError)
					{
						GUI.Label (new Rect (Screen.width / 2 - (150/2), 120, 150, 100), "Your leaderboards save file was corrupted or damaged.\n\nUnfortunately, this means that a new file had to be generated, and all your leaderboard scores have been reset.\n\nA log has been created in the game directory.");
					}
					
					if (GUI.Button (new Rect (Screen.width / 2 - (125/2), 275, 125, 40), "Continue"))
					{
						settings.showSettingsResetScreen = false;
					}
				}

				//GUI.Box (new Rect(-5,-5, Screen.width+10, Screen.height+10), "");
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().enabled = true;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = true;
				GUI.Box (new Rect(Screen.width/2 - (250/2), Screen.height/2 - (375/2), 250, 375), "");
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label (new Rect(Screen.width/2 - 55, Screen.height/2 - 175, 110, 25), "PAUSED");
				GUI.skin = CDGUIGameSkin;
				if (GUI.Button (new Rect(Screen.width/2 - (150/2), Screen.height/2 - 140, 150, 60), "Resume"))
				{
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().enabled = false;
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = false;
					Time.timeScale = pauseReturnTimescale;
					pauseReturnTimescale = 0;
					gamePaused = false;
					mobile_pauseButtonPressed = false;
					//player.GetComponent<Rigidbody>().velocity = pausePlayerVelocity;
					//objLight.GetComponent<autoIntensity>().dayRotateSpeed.x = 1;
					//objLight.GetComponent<autoIntensity>().nightRotateSpeed.x = 1;
				}
				if (GUI.Button (new Rect(Screen.width/2 - (150/2), Screen.height/2 - 60, 150, 60), "Restart"))
				{
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().enabled = false;
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = false;
					Time.timeScale = pauseReturnTimescale;
					pauseReturnTimescale = 0;
					playerScript.initiateGameOver = true;
					gamePaused = false;
					mobile_pauseButtonPressed = false;
					//player.GetComponent<Rigidbody>().velocity = pausePlayerVelocity;
					//objLight.GetComponent<autoIntensity>().dayRotateSpeed.x = 1;
					//objLight.GetComponent<autoIntensity>().nightRotateSpeed.x = 1;
				}
				if (GUI.Button (new Rect(Screen.width/2 - (150/2), Screen.height/2 + 20, 150, 60), "Exit to\nMain Menu"))
				{
					exitToMenu = true;
					Time.timeScale = pauseReturnTimescale;
					pauseReturnTimescale = 0;
					rb.velocity = Vector3.zero;
				}
				if (!Application.isMobilePlatform) {
					if (GUI.Button (new Rect(Screen.width/2 - (150/2), Screen.height/2 + 100, 150, 60), "Exit to\nDesktop"))
					{
						exitToDesktop = true;
						Time.timeScale = pauseReturnTimescale;
						pauseReturnTimescale = 0;
						rb.velocity = Vector3.zero;
					}
				} else {
					if (GUI.Button (new Rect(Screen.width/2 - (150/2), Screen.height/2 + 100, 150, 60), "Exit\nGame."))
					{
						exitToDesktop = true;
						Time.timeScale = pauseReturnTimescale;
						pauseReturnTimescale = 0;
						rb.velocity = Vector3.zero;
					}
				}

			}
			else if (exitToMenu == false && exitToDesktop == false)
			{
				if (gameOverDoOnce == false && score >= 0 && (playerPos.z > 0 || timesCubesCut > 0))
				{
					GUI.skin = CDGUILargeTextSkin;
					//GUI.Label (new Rect (25, 15, 200, 40), score, CDGUILargeTextSkin.customStyles.LargeTextFromLeft);
					if (achievements.isContinuousProgressiveRun) {
						GUI.Label (new Rect (25, 15, 1000, 40), "Score: " + Mathf.Floor (score), CDGUILargeTextSkin.FindStyle("LargeTextFromLeft"));
					}

					if (Application.platform == RuntimePlatform.Android) {

						if (settings.mobileControlScheme == 1) {
							if (GUI.RepeatButton (new Rect (0, 0, Screen.width / 4, Screen.height / 2), "<", CDGUIMenuSkin.FindStyle("controlButtons"))) {
								pressLeftButton = true;
							} else if (Event.current.type == EventType.Repaint) {
								pressLeftButton = false;
							}

							if (GUI.RepeatButton (new Rect (Screen.width * 3 / 4, 0, Screen.width / 4, Screen.height / 2), ">", CDGUIMenuSkin.FindStyle("controlButtons"))) {
								pressRightButton = true;
							} else if (Event.current.type == EventType.Repaint) {
								pressRightButton = false;
							}
						}

						GUI.color = GUIColor;
						if (GUI.Button (new Rect (0, Screen.height - 120, 100, 120), "", CDGUIGameSkin.FindStyle("invisibleButton"))){
							mobile_pauseButtonPressed = true;
						}
						GUI.DrawTexture (new Rect (20,Screen.height - 60, 32, 32), mobile_pauseButtonTex);
					}

			                if (currentPowerup != 0)
			                {
						GUI.color = GUIColor;
						if (Application.isMobilePlatform) {
							GUI.Label (new Rect (Screen.width - 200, Screen.height - 50, 175, 30), "Tap to use");
						} else {
							GUI.Label (new Rect (Screen.width - 200, Screen.height - 50, 175, 30), "'Space' to use");
						}
						GUI.color = Color.white;
						if (currentPowerup == 1) {
							GUI.DrawTexture (new Rect (Screen.width - 180, Screen.height - 200, 150, 150), powerup_JumpTex);
						} else if (currentPowerup == 2) {
							GUI.DrawTexture (new Rect (Screen.width - 180, Screen.height - 200, 150, 150), powerup_SlomoTex);
						} else if (currentPowerup == 3) {
							GUI.DrawTexture (new Rect (Screen.width - 180, Screen.height - 200, 150, 150), powerup_BulletTex);
						} else if (currentPowerup == 4) {
							GUI.DrawTexture (new Rect (Screen.width - 180, Screen.height - 200, 150, 150), powerup_BubbleTex);
						}

						if (Application.platform == RuntimePlatform.Android || settings.simulateMobileOptimization) {
							if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 200, 200, 200), "", CDGUIGameSkin.FindStyle ("invisibleButton"))) {
								mobile_powerupButtonPressed = true;
							}
						}
		                    	}
			                GUI.skin = CDGUIGameSkin;
			                GUI.color = Color.white;
					if (HaveCheatsBeenEnabled)
					{
						GUI.Label (new Rect (25, 50, 500, 20), "Cheats have been enabled, scoring turned off.");
					}
				}
				
				if (playerScript.initBrighten == true)
				{
					if (gameOverTimer > 0)
					{
						//Debug.Log ("GameOverBrighten");
						if (GUIColor.r < 1 && GUIColor.g < 1 && GUIColor.b < 1)
						{
							GUIColor.r = GUIColor.r + Time.deltaTime/4;
							GUIColor.g = GUIColor.g + Time.deltaTime/4;
							GUIColor.b = GUIColor.b + Time.deltaTime/4;
						}
					}
					else
					{
						if (GUIColor.r < 1 && GUIColor.g < 1 && GUIColor.b < 1)
						{
							GUIColor.r = GUIColor.r + Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
							GUIColor.g = GUIColor.g + Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
							GUIColor.b = GUIColor.b + Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
						}
					}
					
				}
				if (playerScript.initDarken == true)
				{
					if (gameOverTimer > 0)
					{
						//Debug.Log ("GameOverDarken");
						if (GUIColor.r > 0 && GUIColor.g > 0 && GUIColor.b > 0)
						{
							GUIColor.r = GUIColor.r - Time.deltaTime/4;
							GUIColor.g = GUIColor.g - Time.deltaTime/4;
							GUIColor.b = GUIColor.b - Time.deltaTime/4;
						}
					}
					else
					{
						if (GUIColor.r > 0 && GUIColor.g > 0 && GUIColor.b > 0)
						{
							GUIColor.r = GUIColor.r - Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
							GUIColor.g = GUIColor.g - Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
							GUIColor.b = GUIColor.b - Time.deltaTime*(objLight.GetComponent<autoIntensity>().nightRotateSpeed.x)/8;
						}
					}
					
				}
				GUI.color = GUIColor;
				
				if (gameOverDoOnce == false)
				{
					GUI.Label (new Rect (5, Screen.height - 20, 100, 20), gameVersion);
					GUI.Label (new Rect (80, Screen.height - 20, 250, 20), "Created by Ethan 'the Drake' Casey");

					//v1.1.0 additions --- UI for skipping song.
					if (showSongSkip && songSkipTimer < 3)
					{
						GUI.Label (new Rect (Screen.width/2 - (125/2), Screen.height - 30, 125, 20), "Skipping song...");
						songSkipTimer = songSkipTimer + Time.deltaTime;
					}
					else if (songSkipTimer >= 3)
					{
						showSongSkip = false;
						songSkipTimer = 0;
					}


				}
				else
				{
					if (achievements.isContinuousProgressiveRun && gameOverTimer > 3 && exitToMenu == false && exitToDesktop == false)
					{
						GUI.skin = CDGUILargeTextSkin;
						GUI.color = GUIColor;


						GUI.Label (new Rect( Screen.width/2 - 500, 20, 1000, 75), "Progressive", CDGUIMenuSkin.FindStyle ("leaderboards1"));

						GUI.Label (new Rect( Screen.width/2 - 500, 100, 1000, 40), "Score: " + Mathf.Floor (score));

						//GUI.Box (new Rect((Screen.width - 575)/2, 110, 575, 260), "", CDGUIMenuSkin.FindStyle("BoxBlackBG"));
						
						GUI.Label (new Rect((Screen.width - 575)/2, 115 + 50, 575, 50), leaderboards.leaderboard1.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards1"));
						GUI.Label (new Rect((Screen.width - 575)/2, 165 + 50, 575, 35), leaderboards.leaderboard2.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards2"));
						GUI.Label (new Rect((Screen.width - 575)/2, 200 + 50, 575, 25), leaderboards.leaderboard3.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards3"));
						GUI.Label (new Rect((Screen.width - 575)/2, 225 + 50, 575, 20), leaderboards.leaderboard4.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
						GUI.Label (new Rect((Screen.width - 575)/2, 245 + 50, 575, 20), leaderboards.leaderboard5.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
						GUI.Label (new Rect((Screen.width - 575)/2, 265 + 50, 575, 20), leaderboards.leaderboard6.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
						GUI.Label (new Rect((Screen.width - 575)/2, 285 + 50, 575, 20), leaderboards.leaderboard7.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
						GUI.Label (new Rect((Screen.width - 575)/2, 305 + 50, 575, 20), leaderboards.leaderboard8.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
						GUI.Label (new Rect((Screen.width - 575)/2, 325 + 50, 575, 20), leaderboards.leaderboard9.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
						GUI.Label (new Rect((Screen.width - 575)/2, 345 + 50, 575, 20), leaderboards.leaderboard10.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));

						GUI.skin = CDGUIGameSkin;
					}
					if (gameOverTimer > 5)
					{
						if (exitToMenu == false && exitToDesktop == false && initGameOverFadeOut == false)
						{
							GUI.skin = CDGUILargeTextSkin;
							GUI.color = GUIColor;
							if (!Application.isMobilePlatform) {
								GUI.Label (new Rect(Screen.width/2 - (350/2), 425, 350, 40), "Press any key to continue");
							} else {
								GUI.Label (new Rect(Screen.width/2 - (350/2), 425, 350, 40), "Tap to continue");
							}
							GUI.skin = CDGUIGameSkin;
						}
					}
				}

				GUI.skin = CDGUILargeTextSkin;
				GUI.color = IntroColor1;

				if (introShowLogo)
				{
					GUI.DrawTexture(new Rect(Screen.width / 2 - 200, 0, 400, 200), logoTex);
					//GUI.Label (new Rect(Screen.width / 2 - 100, 100, 200, 60),"Cube Dodger");

					if (IntroColor1.r < 1 || IntroColor1.g < 1 || IntroColor1.b < 1)
					{
						IntroColor1.r = IntroColor1.r + Time.deltaTime;
						IntroColor1.g = IntroColor1.g + Time.deltaTime;
						IntroColor1.b = IntroColor1.b + Time.deltaTime;
					}
				}
				else if (IntroColor1.r > 0 || IntroColor1.g > 0 || IntroColor1.b > 0)
				{
					GUI.DrawTexture(new Rect(Screen.width / 2 - 200, 0, 400, 200), logoTex);
					//GUI.Label (new Rect(Screen.width / 2 - 100, 100, 200, 60),"Cube Dodger");

					IntroColor1.r = IntroColor1.r - Time.deltaTime;
					IntroColor1.g = IntroColor1.g - Time.deltaTime;
					IntroColor1.b = IntroColor1.b - Time.deltaTime;

				}

				GUI.color = IntroColor2;

				if (introShowModifiers)
				{
					GUI.Label (new Rect(0, 200, Screen.width, 200),"Progressive", CDGUIGameSkin.FindStyle("progressive_IntroText"));

					if (IntroColor2.r < 1 || IntroColor2.g < 1 || IntroColor2.b < 1)
					{
						IntroColor2.r = IntroColor2.r + Time.deltaTime;
						IntroColor2.g = IntroColor2.g + Time.deltaTime;
						IntroColor2.b = IntroColor2.b + Time.deltaTime;
					}
				}
				else if (IntroColor2.r > 0 || IntroColor2.g > 0 || IntroColor2.b > 0)
				{
					GUI.Label (new Rect(0, 200, Screen.width, 200),"Progressive", CDGUIGameSkin.FindStyle("progressive_IntroText"));
					
					IntroColor2.r = IntroColor2.r - Time.deltaTime;
					IntroColor2.g = IntroColor2.g - Time.deltaTime;
					IntroColor2.b = IntroColor2.b - Time.deltaTime;
					
				}

				GUI.color = IntroColor3;

				if (introShowDifficulty)
				{
					//GUI.Label (new Rect(Screen.width / 2 - 100, 375, 200, 60),"Difficulty: " + difficultyString);
					
					if (IntroColor3.r < 1 || IntroColor3.g < 1 || IntroColor3.b < 1)
					{
						IntroColor3.r = IntroColor3.r + Time.deltaTime;
						IntroColor3.g = IntroColor3.g + Time.deltaTime;
						IntroColor3.b = IntroColor3.b + Time.deltaTime;
					}
				}
				else if (IntroColor3.r > 0 || IntroColor3.g > 0 || IntroColor3.b > 0)
				{
					//GUI.Label (new Rect(Screen.width / 2 - 100, 375, 200, 60),"Difficulty: " + difficultyString);
					
					IntroColor3.r = IntroColor3.r - Time.deltaTime;
					IntroColor3.g = IntroColor3.g - Time.deltaTime;
					IntroColor3.b = IntroColor3.b - Time.deltaTime;
					
				}

				GUI.skin = CDGUIGameSkin;
				GUI.color = Color.white;
			}
		}
	}
}