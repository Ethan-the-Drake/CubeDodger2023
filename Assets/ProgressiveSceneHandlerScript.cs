//Code written by Ethan 'the Drake' Casey
//Copyright 2016
//Contact me at "EthanDrakeCasey@gmail.com" for inquiries.
//=====================================================================

using UnityEngine;
using System.Collections;

public class ProgressiveSceneHandlerScript : MonoBehaviour {

	settingsScript settings;
	leaderboardsHolder leaderboards;
	AchievementsScript achievements;
	public progressive_playerScript playerScript;

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

	GameObject step2;
	GameObject step4;
	GameObject step5;
	GameObject step7;
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

	bool referenceCubesHaveRB;
	float pauseReturnTimescale;
	float indTime;
	bool mobile_pauseButtonPressed;
	bool mobile_powerupButtonPressed;	
	public int cubeXMin = -150;
	public int cubeXMax = 150;
	public float cubeSpawnRateMultiplier = 1f;

	int easyCubeSpawnRate = 3000;
	int easyZVelocity = 20;
	int normalCubeSpawnRate = 6000;
	int normalZVelocity = 30;
	int hardCubeSpawnRate = 12000;
	int hardZVelocity = 40;
	int extremeCubeSpawnRate = 32000;
	int extremeZVelocity = 50;

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
		difficulty = 0;
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
		settings.difficulty = 0;
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
			difficultyCubeSpawnRate = easyCubeSpawnRate;
			difficultyZVelocity = easyZVelocity;
		}
		else if (difficulty == 1)
		{
			difficultyCubeSpawnRate = normalCubeSpawnRate;
			difficultyZVelocity = normalZVelocity;
		}
		else if (difficulty == 2)
		{
			difficultyCubeSpawnRate = hardCubeSpawnRate;
			difficultyZVelocity = hardZVelocity;
		}
		else if (difficulty == 3)
		{
			difficultyCubeSpawnRate = extremeCubeSpawnRate;
			difficultyZVelocity = extremeZVelocity;
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

		step2 = GameObject.Find ("Step 2");
		step2.SetActive (false);
		step4 = GameObject.Find ("Step 4");
		step4.SetActive (false);
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
		step19.SetActive (false);

		world3_room = GameObject.Find ("world3_room");
		world3_lights = GameObject.Find ("circusLights");
		world3_room.SetActive (false);
		world3_lights.SetActive (false);
		world4_blackHole = GameObject.Find ("world4_blackHole");
		world4_blackHole.SetActive (false);
		world4_particleSystems = GameObject.Find ("world4_particleSystems");
		world4_particleSystems.SetActive (false);

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
		playerScript = player.GetComponent<progressive_playerScript> ();
		gameCamera = GameObject.Find ("cameraREF");
		gameCameraBlur = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur> ();
		floor = GameObject.Find ("floorPlane");
		objLight = GameObject.Find ("masterLight");
		lightRotObj = GameObject.Find ("lightRotationObject");
		player.transform.position = new Vector3 (0, 0, -50);
		gameCamera.transform.position = new Vector3 (0, 4, -30);
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

		checkCheckpoints ();

		//3.0
		originalPlayerTurnResponsiveness = playerTurnResponsiveness; //for slowmo
	}

	void doMobileOptimization (){
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunShaftIntensity = 0;
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = false;
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity = 0;

		cubeXMin = -100;
		cubeXMax = 100;
		cubeSpawnRateMultiplier = 1.5f;

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void checkCheckpoints(){
		if (settings.progressiveCurrentCheckpoint == 0) {
			achievements.isContinuousProgressiveRun = true;
		} else if (settings.progressiveCurrentCheckpoint == 1) {
			player.transform.position = new Vector3 (0, playerPos.y, -230);
			GameObject.Find ("Step 1").SetActive (false);
			step5.SetActive (true);
			progressiveStep = 6;
			timesCubesCut = 2;
			settings.modifierSpeedMultiplier = 1.0f;
			progressiveDifficultyChange (1);
			settings.modifierPowerups = true;
			progressive_changeworld (1);
		} else if (settings.progressiveCurrentCheckpoint == 2) {
			step10_2.SetActive (true);
			player.transform.position = new Vector3 (0, playerPos.y, -5000);
			progressiveStep = 11;
			timesCubesCut = 5;
			progressiveDifficultyChange (1);
			settings.modifierPowerups = false;
			settings.modifierSpeedMultiplier = 0.75f;
			settings.modifierCubeType = 1;
			progressive_changeworld (2);
			if (world3_room.activeSelf == true) {
				world3_room.transform.position = new Vector3 (world3_room.transform.position.x, world3_room.transform.position.y, world3_room.transform.position.z - 10000);
			}
			nearCubeCut.SetActive (false);
			farCubeCut.SetActive (false);
		} else if (settings.progressiveCurrentCheckpoint == 3) {
			player.transform.position = new Vector3 (0, playerPos.y, -230);
			GameObject.Find ("Step 1").SetActive (false);
			step15.SetActive (true);
			progressiveStep = 16;
			timesCubesCut = 7;
			progressiveDifficultyChange (2);
			settings.modifierPowerups = true;
			settings.modifierSpeedMultiplier = 0.75f;
			settings.modifierCubeType = 0;
			progressive_changeworld (3);
			if (world4_blackHole.activeSelf == true) {
				world4_blackHole.transform.position = new Vector3 (playerPos.x, world4_blackHole.transform.position.y, world4_blackHole.transform.position.z - 10000);
			}
			playerScript.initBrighten = true;
			playerScript.isNight = true;
		}
	}

	void checkForAchievements(){
		if (!achievements.ach100points && score >= 100) {
			achievements.ach100points = true;
			achievements.queueAch (1);
			achievements.saveAchievements ();
		}
		if (!achievements.ach1000points && score >= 1000) {
			achievements.ach1000points = true;
			achievements.queueAch (2);
			achievements.saveAchievements ();
		}
		if (!achievements.ach5000points && score >= 5000) {
			achievements.ach5000points = true;
			achievements.queueAch (3);
			achievements.saveAchievements ();
		}
		if (!achievements.ach10000points && score >= 10000) {
			achievements.ach10000points = true;
			achievements.queueAch (4);
			achievements.saveAchievements ();
		}
		if (!achievements.ach20000points && score >= 20000) {
			achievements.ach20000points = true;
			achievements.queueAch (5);
			achievements.saveAchievements ();
		}
		if (!achievements.achDie && gameOverDoOnce) {
			achievements.achDie = true;
			achievements.queueAch (11);
			achievements.saveAchievements ();
		}
		if (!achievements.achXLimiters && (player.transform.position.x < -4750 || player.transform.position.x > 4750)) {
			achievements.achXLimiters = true;
			achievements.queueAch (16);
			achievements.saveAchievements ();
		}
	}

	//Called every 1 second
	void Update1s() {
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

	//called every 5 seconds
	void Update5s(){
		checkForAchievements ();

		if (!playerScript.initiateGameOver && !gamePaused) {
			updateXLimiters();

			// If the player is below the speed the difficulty demands, raise him to that speed.
			//Debug.Log(playerVelocity.z != (difficultyZVelocity*settings.modifierSpeedMultiplier));
			if (playerVelocity.z != (difficultyZVelocity*settings.modifierSpeedMultiplier))
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
		if (settings.screenshotMode)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				gamePaused = true;
				exitToMenu = true;
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (gamePaused) 
				{
					gamePaused = false;
				}
				else if (!gamePaused)
				{
					gamePaused = true;
				}
				//exitToDesktop = true;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				player.transform.position = new Vector3 (4995, playerPos.y, playerPos.z);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				player.transform.position = new Vector3 (-4995, playerPos.y, playerPos.z);
			}
			if (Input.GetKey(KeyCode.UpArrow))
			{
				player.transform.position = new Vector3 (0, playerPos.y, playerPos.z);
			}
			if (Input.GetKey(KeyCode.Keypad8))//Rot Up
			{
				gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x + 1, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			}
			if (Input.GetKey(KeyCode.Keypad2))//Rot Down
			{
				gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x - 1, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			}
			if (Input.GetKey(KeyCode.Keypad6))//Rot Right
			{
				gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y + 1, gameCamera.transform.eulerAngles.z);
			}
			if (Input.GetKey(KeyCode.Keypad4))//Rot Left
			{
				gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y - 1, gameCamera.transform.eulerAngles.z);
			}
			if (!playerScript.devCheat_GodMode)
			{
				playerScript.devCheat_GodMode = true;
			}
		}

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
			if (playerPos.z >= 5000 && progressiveStep < 20)
			{
				cubeCutScore = score;
				scoreUpdateZCoord = -4999;
				if (world3_room.activeSelf == true) {
					world3_room.transform.position = new Vector3 (world3_room.transform.position.x, world3_room.transform.position.y, world3_room.transform.position.z - 10000);
				}
				if (world4_blackHole.activeSelf == true) {
					world4_blackHole.transform.position = new Vector3 (playerPos.x, world4_blackHole.transform.position.y, world4_blackHole.transform.position.z - 10000);
				}
				player.transform.position = new Vector3(playerPos.x, playerPos.y, -5000);
				timesCubesCut = timesCubesCut + 1;

			}
			if (playerPos.y < -0.5)
			{
				player.transform.position = new Vector3(playerPos.x, 0, playerPos.z);
			}
			
			playerPos = player.transform.position;
			playerRot = player.transform.eulerAngles;
			cameraPos = new Vector3(playerPos.x, playerPos.y + 2, playerPos.z - 5);
			floorPos = new Vector3 (playerPos.x, floor.transform.position.y, playerPos.z + 25);
			gameCamera.transform.position = cameraPos;
			lightRotObj.transform.position = cameraPos;
			floor.transform.position = floorPos;
			playerVelocity = rb.velocity;

			if ((Input.GetKeyDown (KeyCode.Space) || mobile_powerupButtonPressed) && !gameOverDoOnce) {
				if (settings.modifierPowerups) {
					mobile_powerupButtonPressed = false;
					usePowerUp ();
				}
			}

			if (EnablePlayerControls == false && ((playerPos.z > 0 && settings.progressiveCurrentCheckpoint == 0) || (playerPos.z > 0 && (settings.progressiveCurrentCheckpoint == 1 || settings.progressiveCurrentCheckpoint == 3))  || (timesCubesCut >= 5 && settings.progressiveCurrentCheckpoint == 2)) && progressiveStep < 20)
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
				///else
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
				if (currentSong == null || currentSong.GetComponent<AudioSource>().isPlaying == false && !transition3 && GameObject.Find("endingArsSonor1").GetComponent<AudioSource>().isPlaying == false)
				{
					pickMusic();
				}
				else if (Input.GetKeyUp (KeyCode.F) && GameObject.Find("endingArsSonor1").GetComponent<AudioSource>().isPlaying == false)
				{
					pickMusic();
					showSongSkip = true;
				}
				
				if 
				(
						(playerPos.z + cubeSpawnDistance < 4750 && playerPos.z + cubeSpawnDistance > -4750) &&
						(progressiveStep != 1 || playerPos.z + cubeSpawnDistance < 780) &&
						(progressiveStep != 2 || playerPos.z + cubeSpawnDistance > 1220) &&
						(progressiveStep != 3 || playerPos.z + cubeSpawnDistance < -350) &&
						(progressiveStep != 4 || playerPos.z + cubeSpawnDistance > 350) &&
						(progressiveStep != 5 || playerPos.z + cubeSpawnDistance < -250) &&
						(progressiveStep != 6 || playerPos.z + cubeSpawnDistance > 250) &&
						(progressiveStep != 7 || playerPos.z + cubeSpawnDistance < -350) && 
						(progressiveStep != 8 || playerPos.z + cubeSpawnDistance > 350) &&
						(progressiveStep != 9 || playerPos.z + cubeSpawnDistance < -350) &&
						(progressiveStep != 10 || playerPos.z + cubeSpawnDistance > 350) &&
						(progressiveStep != 11 || playerPos.z + cubeSpawnDistance < -350) &&
						(progressiveStep != 12 || playerPos.z + cubeSpawnDistance > 350) &&
						(progressiveStep != 13 || playerPos.z + cubeSpawnDistance < -450) &&
						(progressiveStep != 14 || playerPos.z + cubeSpawnDistance > 450) &&
						(progressiveStep != 15 || playerPos.z + cubeSpawnDistance < -250) &&
						(progressiveStep != 16 || playerPos.z + cubeSpawnDistance > 250) &&
						(progressiveStep != 17 || playerPos.z + cubeSpawnDistance < -450) &&
						(progressiveStep != 18 || playerPos.z + cubeSpawnDistance > 450) &&
						(progressiveStep != 19 || playerPos.z + cubeSpawnDistance < -450) &&
						(progressiveStep != 20 || playerPos.z + cubeSpawnDistance > 450) &&
						(progressiveStep != 21)

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
			//modifiersCameraType();
			//modifiersDrunkDriver();

            		powerupJump();
            		powerupSlowmo();

			progressiveStuff ();
		}

	}

	void progressiveStuff()
	{
		if (progressiveStep == 0) {
			if (playerPos.z >= 0) {
				progressiveStep = 1;
				progressiveDifficultyChange (0);
			}
		} else if (progressiveStep == 1) {
			if (playerPos.z >= 300) {
				if (step2.activeSelf == false) {
					step2.transform.position = new Vector3 (playerPos.x, step2.transform.position.y, step2.transform.position.z);
					step2.SetActive (true);
				}
			}

			if (playerPos.z >= 1000) {
				progressiveStep = 2;

				progressiveDifficultyChange (1);
				settings.modifierSpeedMultiplier = 0.75f;
			}
		} else if (progressiveStep == 2) {
			if (playerPos.z >= 1200) {
				if (step2.activeSelf == true) {
					step2.SetActive (false);
				}
			}

			if (timesCubesCut >= 1) {
				progressiveStep = 3;

				settings.modifierSpeedMultiplier = 1.0f;
			}
		} else if (progressiveStep == 3) {
			if (playerPos.z >= -750) {
				if (step4.activeSelf == false) {
					step4.transform.position = new Vector3 (playerPos.x, step4.transform.position.y, step4.transform.position.z);
					step4.SetActive (true);
				}
			}

			if (playerPos.z >= 150) {
				progressiveStep = 4;

				settings.modifierSpeedMultiplier = 1.25f;
			}
			
		} else if (progressiveStep == 4) {
			if (playerPos.z >= 400) {
				if (step4.activeSelf == true) {
					step4.SetActive (false);
				}
			}

			if (timesCubesCut >= 2) {
				progressiveStep = 5;

				settings.modifierSpeedMultiplier = 1.5f;
			}
		} else if (progressiveStep == 5) {
			if (playerPos.z >= -750) {
				if (!step5.activeSelf) {
					step5.transform.position = new Vector3 (playerPos.x, step5.transform.position.y, step5.transform.position.z);
					step5.SetActive (true);
				}
			}

			if (playerPos.z >= -150) {
				progressiveStep = 6;

				settings.modifierPowerups = true;
				settings.modifierSpeedMultiplier = 1.0f;
				progressive_changeworld (1);
			}
		} else if (progressiveStep == 6) {
			if (playerPos.z >= 250) {
				if (step5.activeSelf == true) {
					step5.SetActive (false);
				}
			}

			if (timesCubesCut >= 3) {
				progressiveStep = 7;

				settings.modifierSpeedMultiplier = 1.25f;

			}
		} else if (progressiveStep == 7) {
			if (playerPos.z >= -750) {
				if (step7.activeSelf == false) {
					step7.transform.position = new Vector3 (playerPos.x, step7.transform.position.y, step7.transform.position.z);
					step7.SetActive (true);
				}
			}

			if (playerPos.z >= 150) {
				progressiveStep = 8;

				settings.modifierSpeedMultiplier = 1.5f;
			}
		} else if (progressiveStep == 8) {
			if (playerPos.z > 400) {
				if (step7.activeSelf == true) {
					step7.SetActive (false);
				}
			}

			if (timesCubesCut >= 4) {
				progressiveStep = 9;

				settings.modifierSpeedMultiplier = 1.75f;
			}
		} else if (progressiveStep == 9) {
			if (playerPos.z >= -750) {
				if (step9.activeSelf == false) {
					step9.transform.position = new Vector3 (playerPos.x, step9.transform.position.y, step9.transform.position.z);
					step9.SetActive (true);
				}
			}

			if (playerPos.z > 150) {
				progressiveStep = 10;

				settings.modifierSpeedMultiplier = 2.0f;
			}
		} else if (progressiveStep == 10) {
			if (playerPos.z > 400) {
				if (step9.activeSelf == true) {
					step9.SetActive (false);
				}
			}

			if (playerPos.z >= 4250) {
				if (step10_1.activeSelf == false) {
					step10_1.transform.position = new Vector3 (playerPos.x, step10_1.transform.position.y, step10_1.transform.position.z);
					step10_1.SetActive (true);
				}
			}

			if (timesCubesCut >= 5) {
				progressiveStep = 11;

				settings.modifierSpeedMultiplier = 0.75f;
				settings.modifierCubeType = 1;
				settings.modifierPowerups = false;
				progressive_changeworld (2);
				step10_2.transform.position = new Vector3 (step10_1.transform.position.x, step10_2.transform.position.y, step10_2.transform.position.z);
				step10_2.SetActive (true);
				player.transform.position = new Vector3 (playerPos.x, playerPos.y, -5000);
				step10_1.SetActive (false);
			}

		} else if (progressiveStep == 11) {
			if (playerPos.z > -4500) {
				if (step10_2.activeSelf == true) {
					step10_2.SetActive (false);
				}
			}

			if (playerPos.z >= -750) {
				if (step11.activeSelf == false) {
					step11.transform.position = new Vector3 (playerPos.x, step11.transform.position.y, step11.transform.position.z);
					step11.SetActive (true);
				}
			}

			if (playerPos.z > 150) {
				progressiveStep = 12;

				settings.modifierSpeedMultiplier = 1.0f;
				settings.modifierPowerups = true;
			}
		} else if (progressiveStep == 12) {
			if (playerPos.z > 400) {
				if (step11.activeSelf == true) {
					step11.SetActive (false);
				}
			}

			if (timesCubesCut >= 6) {
				progressiveStep = 13;

				settings.modifierSpeedMultiplier = 1.25f;
			}

		} else if (progressiveStep == 13) {
			if (playerPos.z > -750) {
				if (step13.activeSelf == false) {
					step13.transform.position = new Vector3 (playerPos.x, step13.transform.position.y, step13.transform.position.z);
					step13.SetActive (true);
				}
			}

			if (playerPos.z > 200) {
				progressiveStep = 14;

				settings.modifierSpeedMultiplier = 1.0f;
				settings.modifierCubeType = 2;
			}
		} else if (progressiveStep == 14) {
			if (playerPos.z > 500) {
				if (step13.activeSelf == true) {
					step13.SetActive (false);
				}
			}

			if (timesCubesCut >= 7) {
				progressiveStep = 15;

				settings.modifierSpeedMultiplier = 1.25f;
			}
		} else if (progressiveStep == 15) {
			if (playerPos.z >= -750) {
				if (step15.activeSelf == false) {
					step15.transform.position = new Vector3 (playerPos.x, step15.transform.position.y, step15.transform.position.z);
					step15.SetActive (true);
				}
			}

			if (playerPos.z >= -150) {
				progressiveStep = 16;

				progressiveDifficultyChange(2);
				settings.modifierSpeedMultiplier = .75f;
				settings.modifierCubeType = 0;
				progressive_changeworld (3);

				playerScript.initBrighten = true;
				playerScript.isNight = true;
			}
		} else if (progressiveStep == 16) {
			if (playerPos.z >= 250) {
				if (step15.activeSelf) {
					step15.SetActive (false);
				}
			}

			if (playerPos.z >= 2500) {
				if (transition3 != true && GameObject.Find("endingArsSonor1").GetComponent<AudioSource>().isPlaying == false) {
					transition3 = true;
					world4_songToFade = currentSong;
					currentSong = GameObject.Find("endingArsSonor1");
					Debug.Log ("playing ending ars sonor 1");
					currentSong.GetComponent<AudioSource> ().Play ();
				}
			}

			if (timesCubesCut >= 8) {
				progressiveStep = 17;

				settings.modifierSpeedMultiplier = 1.0f;

				world4_moveBackParticles = true;
					
			}
		} else if (progressiveStep == 17) {
			if (playerPos.z >= -750) {
				if (step17.activeSelf == false) {
					step17.transform.position = new Vector3 (playerPos.x, step17.transform.position.y, step17.transform.position.z);
					step17.SetActive (true);
				}
			}

			if (playerPos.z >= 100) {
				progressiveStep = 18;

				progressiveDifficultyChange(1);
				settings.modifierSpeedMultiplier = 1.0f;
				settings.modifierAirdrop = true;
			}
		} else if (progressiveStep == 18) {
			if (playerPos.z >= 500) {
				if (step17.activeSelf) {
					step17.SetActive (false);
				}
			}

			if (timesCubesCut >= 9) {
				progressiveStep = 19;

				settings.modifierCubeType = 2;

				world4_moveBackParticles = true;
			}
		} else if (progressiveStep == 19) {
			if (playerPos.z >= -750) {
				if (step19.activeSelf == false) {
					step19.transform.position = new Vector3 (playerPos.x, step17.transform.position.y, step17.transform.position.z);
					step19.SetActive (true);
				}
			}

			if (playerPos.z >= 100) {
				progressiveStep = 20;

				progressiveDifficultyChange(2);
				settings.modifierSpeedMultiplier = 1.0f;
				settings.modifierCubeType = 0;
			}
		} else if (progressiveStep == 20) {
			if (playerPos.z >= 500) {
				if (step19.activeSelf) {
					step19.SetActive (false);
				}
			}

			if (playerPos.z >= 4750) {
				EnablePlayerControls = false;
				transition4 = true;
			}

			if (playerPos.z >= 5000) {
				progressiveStep = 21;

				difficultyZVelocity = 0;
				difficultyCubeSpawnRate = 0;
				player.GetComponent<MeshCollider> ().enabled = false;

				//settings.writeProgressiveFinale();
			}
		} else if (progressiveStep == 21) {
			doProgressiveEnding ();
			if (!achievements.achProgressive4) {
				achievements.achProgressive4 = true;
				achievements.unlockedFarewellTheInnocent = true;
				achievements.unlockedSamaritan = true;
				settings.optionsMenuArsFarewellTheInnocentEnabled = true;
				settings.optionsGameArsFarewellTheInnocentEnabled = false;
				settings.optionsMenuArsSamaritanEnabled = false;
				settings.optionsGameArsSamaritanEnabled = true;
				achievements.queueAch (21);
				achievements.saveAchievements();
			}
		}
			

		if (transition1) {
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly < 0.75f || gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity < 1.5f) {
				if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly < 0.75f) {
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly + 0.005f;
				}
				if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity < 1.5f) {
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity + 0.05f;
				}
			} else {
				playerScript.initBrighten = true;
				playerScript.isNight = true;
				GameObject.Find ("world2_particleSystems").GetComponent<ParticleSystem> ().Play ();
				transition1 = false;
			}
		} else if (transition2) {
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly > 0 || gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity > 0) {
				if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly != 0) {
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().edgesOnly - 0.005f;
				}
				if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity > 0) {
					gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().bloomIntensity - 0.05f;
				}
			} else {
				playerScript.initDarken = true;
				playerScript.isNight = false;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection> ().enabled = false;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().enabled = false;
				GameObject.Find ("world2_particleSystems").GetComponent<ParticleSystem> ().Stop ();
				transition2 = false;
			}
		} else if (transition3) {
			//this section handles music transitions towards the end of world 4.

			if (world4_songToFade != null && world4_songToFade.GetComponent<AudioSource> ().volume > 0) {
				world4_songToFade.GetComponent<AudioSource> ().volume = world4_songToFade.GetComponent<AudioSource> ().volume - 0.025f * Time.deltaTime;
			} else {
				transition3 = false;
			}

		} else if (transition4) {
			//if (transition4Timer >= 2.5f) {
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity < 0.95) {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity + 0.125f * Time.deltaTime;
			} else {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().intensity = 1;
			}
			//} else {
			//	transition4Timer = transition4Timer + 1 * Time.deltaTime;
			//}

		}
	}

	void progressive_changeworld(int newWorld)
	{
		currentWorld = newWorld;
		if (newWorld == 0) {
			// Standard World
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts>().enabled = true;
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = false;
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().enabled = false;
			starsREF.GetComponent<ParticleSystem> ().startColor = Color.white;
			starsREF.GetComponent<ParticleSystem> ().maxParticles = 15000;
			starsREF.GetComponent<ParticleSystem> ().Clear (true);

		} else if (newWorld == 1) {
			//Cyber/Wireframe world
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts>().enabled = false;
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.EdgeDetection>().enabled = true;
			if (!Application.isMobilePlatform) {
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Bloom> ().enabled = true;
			}
			starsREF.GetComponent<ParticleSystem> ().startColor = Color.black;
			starsREF.GetComponent<ParticleSystem> ().maxParticles = 15000;
			starsREF.GetComponent<ParticleSystem> ().Clear (true);
			transition1 = true;
			if (!achievements.achProgressive1) {
				achievements.achProgressive1 = true;
				achievements.queueAch (18);
				achievements.saveAchievements();
			}

			if (settings.progressiveCheckpoints) {
				settings.progressiveCurrentCheckpoint = 1;
			}

		} else if (newWorld == 2) {
			//Circus World
			starsREF.GetComponent<ParticleSystem> ().startColor = Color.white;
			starsREF.GetComponent<ParticleSystem> ().maxParticles = 0;
			starsREF.GetComponent<ParticleSystem> ().Clear (true);
			objLight.GetComponent<autoIntensity> ().dayRotateSpeed.x = 0;
			objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x = 0;
			objLight.GetComponent<Light>().color = Color.black;

			world3_room.SetActive (true);
			world3_lights.SetActive (true);
			transition2 = true;

			if (!achievements.achProgressive2) {
				achievements.achProgressive2 = true;
				achievements.queueAch (19);
				achievements.saveAchievements();
			}
			if (settings.progressiveCheckpoints) {
				settings.progressiveCurrentCheckpoint = 2;
			}
		} else if (newWorld == 3) {
			//Apocalypse World
			starsREF.GetComponent<ParticleSystem> ().startColor = Color.red;
			starsREF.GetComponent<ParticleSystem> ().maxParticles = 15000;
			starsREF.GetComponent<ParticleSystem> ().Clear (true);
			objLight.GetComponent<autoIntensity> ().dayRotateSpeed.x = 0.1f;
			objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x = 0.1f;
			objLight.GetComponent<autoIntensity> ().dayAtmosphereThickness = 8;
			objLight.GetComponent<autoIntensity> ().nightAtmosphereThickness = 0.8f;
			objLight.GetComponent<Light> ().color = Color.red;
			objLight.transform.localEulerAngles = new Vector3 (150, 0, 0);

			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = true;
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunColor = Color.red;

			player.GetComponent<progressive_playerScript> ().initBrighten = true;

			world3_room.SetActive (false);
			world3_lights.SetActive (false);
			world4_blackHole.SetActive (true);
			world4_particleSystems.SetActive (true);

			if (!achievements.achProgressive3) {
				achievements.achProgressive3 = true;
				achievements.queueAch (20);
				achievements.saveAchievements();
			}
			if (settings.progressiveCheckpoints) {
				settings.progressiveCurrentCheckpoint = 3;
			}
			//transition3 = true;
		} else if (newWorld == 4) {
			//Epilogue World
		} else {
			Debug.Log ("Invalid World Code. Changing to standard.");
			progressive_changeworld (0);
		}
	}

	void doProgressiveEnding ()
	{
		if (achievements.isContinuousProgressiveRun && settings.progressiveContinuousScore != score) {
			settings.progressiveContinuousScore = score;
		}

		if (settings.progressiveCheckpoints) {
			settings.progressiveCurrentCheckpoint = 4;
		}

		if (GameObject.Find ("endingArsSonor1").GetComponent<AudioSource> ().isPlaying == false) {
			Application.LoadLevel ("credits_menu");
			Debug.Log("loading credits menu");
		}
	}

	void progressiveDifficultyChange(int newDifficulty)
	{
		settings.difficulty = newDifficulty;
		difficulty = newDifficulty;
		if (newDifficulty == 0)
		{
			difficultyCubeSpawnRate = easyCubeSpawnRate;
			difficultyZVelocity = easyZVelocity;
		}
		else if (newDifficulty == 1)
		{
			difficultyCubeSpawnRate = normalCubeSpawnRate;
			difficultyZVelocity = normalZVelocity;
		}
		else if (newDifficulty == 2)
		{
			difficultyCubeSpawnRate = hardCubeSpawnRate;
			difficultyZVelocity = hardZVelocity;
		}
		else if (newDifficulty == 3)
		{
			difficultyCubeSpawnRate = extremeCubeSpawnRate;
			difficultyZVelocity = extremeZVelocity;
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
				Time.timeScale = Time.timeScale - (Time.deltaTime / slowmoWindDownTime);
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
				Time.timeScale = Time.timeScale + (Time.deltaTime / slowmoWindUpTime);
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
				newProjectile.AddComponent<projectileScript> ();
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
	//Drunk Driver and Camera Type are not used in Progressive, so there's no need to run the scripts for them every frame here.
	/*void modifiersDrunkDriver()
	{
		if (settings.modifierDrunkDriver)
		{
			if (!gameCameraBlur.enabled)
			{
				gameCameraBlur.enabled = true;
			}
			
			if (settings.modifierDrunkDriverCamAnimSwitch == 0)
			{
				settings.modifierDrunkDriverCamAnimAmount = settings.modifierDrunkDriverCamAnimAmount - 0.0025f;
				gameCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y, cameraPos.z + settings.modifierDrunkDriverCamAnimAmount);
				gameCamera.transform.Rotate(new Vector3(Random.Range(-0.03f, 0.01f),0,0));
				if (settings.modifierDrunkDriverCamAnimAmount <= -0.5f)
				{
					settings.modifierDrunkDriverCamAnimSwitch = 1;
				}
			}
			else if (settings.modifierDrunkDriverCamAnimSwitch == 1)
			{
				settings.modifierDrunkDriverCamAnimAmount = settings.modifierDrunkDriverCamAnimAmount + 0.0025f;
				gameCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y,cameraPos.z + settings.modifierDrunkDriverCamAnimAmount);
				gameCamera.transform.Rotate(new Vector3(Random.Range(-0.01f, 0.03f),0,0));
				if (settings.modifierDrunkDriverCamAnimAmount >= 0.5f)
				{
					settings.modifierDrunkDriverCamAnimSwitch = 0;
				}
			}
		}
		else if (!gameOverDoOnce)
		{
			if (gameCameraBlur.enabled)
			{
				gameCameraBlur.enabled = false;
			}
			
			gameCamera.transform.position = cameraPos;
			if (settings.modifierCameraType == 0 && !settings.screenshotMode)
			{
				gameCamera.transform.eulerAngles = new Vector3(0,0,0);
			}
			else if (!settings.screenshotMode)
			{
				gameCamera.transform.eulerAngles = new Vector3(0,gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
				
			}
		}
	}*/

	/*void modifiersCameraType()
	{
		if (settings.modifierCameraType == 1)
		{
			
			if (Mathf.FloorToInt(gameCamera.transform.eulerAngles.z) != 180 && !settings.modifierDrunkDriver)
			{
				gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y,(gameCamera.transform.eulerAngles.z + 1));
			}
			else if (settings.modifierDrunkDriver)
			{
				if (Mathf.FloorToInt(gameCamera.transform.eulerAngles.z) < 178)
				{
					gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y,(gameCamera.transform.eulerAngles.z + 1));
				}
				else if (Mathf.FloorToInt(gameCamera.transform.eulerAngles.z) > 182)
				{
					gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y,(gameCamera.transform.eulerAngles.z - 1));
				}
			}
		}
		else if (settings.modifierCameraType == 2)
		{
			gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y, (gameCamera.transform.eulerAngles.z + 1));
		}
		else
		{
			/*gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y,(gameCamera.transform.eulerAngles.z + 1));
					if (Mathf.FloorToInt (gameCamera.transform.eulerAngles.z) < 1 || Mathf.FloorToInt(gameCamera.transform.eulerAngles.z) > 359)
					{
						gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y, 0);
					}* /
			gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y, 0);
		}
	}*/


	void spawnNewCube()
	{
 
		if (settings.modifierAirdrop) {
			if (difficulty == 0) {
				cubeSpawnDistance = Mathf.CeilToInt(player.transform.position.z + 75);
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
				cubeSpawnDistance = 150;
			} else if (difficulty == 2) {
				if (Application.isMobilePlatform || settings.simulateMobileOptimization) {
					cubeSpawnDistance = 75;
				} else {
					cubeSpawnDistance = 175;
				}

			} else if (difficulty == 3) {
				cubeSpawnDistance = 200;
			}
			cubeSpawnDistance = cubeSpawnDistance * settings.modifierSpeedMultiplier;
			if (cubeSpawnDistance > 200) {
				cubeSpawnDistance = 200;
			}
		}

		//1.2.0

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
			//newCube.AddComponent<progressive_cubeDeleteScript> ();
			newCube.GetComponent<progressive_cubeDeleteScript>().isReferenceCube = false;
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
					if (currentWorld == 1 && progressiveStep != 10) {
						GameObject.Find ("world2_StaticParticleSystems").GetComponent<ParticleSystem> ().Stop ();
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
					if (currentWorld == 1) {
						GameObject.Find ("world2_StaticParticleSystems").GetComponent<ParticleSystem> ().Play ();
						//GameObject.Find ("world2_StaticParticleSystems").transform.position = new Vector3 (farCubeCut.transform.position.x, -3, 5000);
					}
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
		if (introFadeInTimer >= 10)
		{
			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay>().intensity < 0)
			{
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay>().intensity = (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay>().intensity + (Time.deltaTime*settings.modifierSpeedMultiplier));
			}
			else
			{
				initFadeIn = false;
			}
			if (introShowLogo)
			{
				introShowLogo = false;
			}
			if (introShowModifiers)
			{
				introShowModifiers = false;
			}
			if (introShowDifficulty)
			{
				introShowDifficulty = false;
			}
		}
		else if (introFadeInTimer < 10)
		{
			if (settings.progressiveCurrentCheckpoint == 0) {
				player.transform.position = new Vector3 (0, 0, -50);
			} else if (settings.progressiveCurrentCheckpoint == 1 || settings.progressiveCurrentCheckpoint == 3) {
				player.transform.position = new Vector3 (0, 0, -230);
			} else if (settings.progressiveCurrentCheckpoint == 2) {
				player.transform.position = new Vector3 (0, 0, -5000);
			}
			if (introFadeInTimer>= 1)
			{
				if (introShowLogo == false)
				{
					introShowLogo = true;
				}
			}
			if (introFadeInTimer >= 2.5f)
			{
				if (introShowModifiers == false)
				{
					introShowModifiers = true;
				}
			}
			if (introFadeInTimer >= 5)
			{
				if (introShowDifficulty == false)
				{
					introShowDifficulty = true;
				}
			}
			if ((Input.GetMouseButton(0) || Input.anyKey) && initialCubeSpawner == null)
			{
				introFadeInTimer = 10;
			}

		}
		introFadeInTimer = introFadeInTimer + Time.deltaTime;
	}

	void gameOver()
	{
		if (gameOverDoOnce == false)
		{
			leaderboards.recordScore(score);

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
			Application.LoadLevel("game_progressive");

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

	int cheatCounter = 0;
	//3.0 new vars for control tuning
	float playerTurnResponsiveness = 40.0F;
	float playerMaxTurnVelocity = 15.0F;
	void getPlayerInput()
	{
		//Cheats
		if (Input.GetKeyUp(KeyCode.BackQuote))
        {
			cheatCounter++;
			Debug.Log("Cheat Counter: " + cheatCounter);
			if (cheatCounter >= 3)
            {
				settings.enableDevCheats = true;
				playerScript.devCheat_GodMode = true;
            }
        }
		if (EnablePlayerControls) {
			if (!useAnalogControls && Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
				if (Input.GetKey (KeyCode.A) && (!Input.GetKey (KeyCode.D))) {
					if (playerVelocity.x > -playerMaxTurnVelocity) {
						rb.velocity = new Vector3 (playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					} else if (playerVelocity.x < -playerMaxTurnVelocity) {
						rb.velocity = new Vector3 (-playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					}
				}
				if (Input.GetKey (KeyCode.D) && (!Input.GetKey (KeyCode.A))) {
					if (playerVelocity.x < playerMaxTurnVelocity) {
						rb.velocity = new Vector3 (playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					} else if (playerVelocity.x > playerMaxTurnVelocity) {
						rb.velocity = new Vector3 (playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					}
				}

				if ((!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) || (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D))) {
					if (playerVelocity.x < 0) {
						rb.velocity = new Vector3 (playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					}
					if (playerVelocity.x > 0) {
						rb.velocity = new Vector3 (playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
					}
					if (playerVelocity.x > -1 && playerVelocity.x < 1) {
						rb.velocity = new Vector3 (0, playerVelocity.y, playerVelocity.z);
					}

				}
				player.transform.eulerAngles = new Vector3 (0, 0, -rb.velocity.x * 2);
			} else if (Application.platform == RuntimePlatform.Android || settings.simulateMobileOptimization) {
				if (settings.mobileControlScheme == 0 || useAnalogControls) {
					if (Application.platform == RuntimePlatform.Android) {
						analogTurn = Input.acceleration.x * playerTurnResponsiveness;
					}

					if (analogTurn >= -playerMaxTurnVelocity && analogTurn <= playerMaxTurnVelocity) {
						rb.velocity = new Vector3 (analogTurn, playerVelocity.y, playerVelocity.z);
					} else if (analogTurn < -playerMaxTurnVelocity) {
						rb.velocity = new Vector3 (-playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					} else if (analogTurn > playerMaxTurnVelocity) {
						rb.velocity = new Vector3 (playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
					}
					player.transform.eulerAngles = new Vector3 (0, 0, -rb.velocity.x * 2);
				} else if (settings.mobileControlScheme == 1 || useButtonControls) {
					if (pressLeftButton && (!pressRightButton)) {
						if (playerVelocity.x > -playerMaxTurnVelocity) {
							rb.velocity = new Vector3 (playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						} else if (playerVelocity.x < -playerMaxTurnVelocity) {
							rb.velocity = new Vector3 (-playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
						}
					}
					if (pressRightButton && (!pressLeftButton)) {
						if (playerVelocity.x < playerMaxTurnVelocity) {
							rb.velocity = new Vector3 (playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						} else if (playerVelocity.x > playerMaxTurnVelocity) {
							rb.velocity = new Vector3 (playerMaxTurnVelocity, playerVelocity.y, playerVelocity.z);
						}
					}

					if ((!pressLeftButton && !pressRightButton) || (pressLeftButton && pressRightButton)) {
						if (playerVelocity.x < 0) {
							rb.velocity = new Vector3 (playerVelocity.x + Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						}
						if (playerVelocity.x > 0) {
							rb.velocity = new Vector3 (playerVelocity.x - Time.deltaTime * playerTurnResponsiveness, playerVelocity.y, playerVelocity.z);
						}
						if (playerVelocity.x > -1 && playerVelocity.x < 1) {
							rb.velocity = new Vector3 (0, playerVelocity.y, playerVelocity.z);
						}

					}
					player.transform.eulerAngles = new Vector3 (0, 0, -rb.velocity.x * 2);
				}
			}

			if ((Input.GetKeyDown (KeyCode.Escape) || mobile_pauseButtonPressed) && EnablePlayerControls && exitToMenu == false && exitToDesktop == false) {
				gamePaused = true;
				pauseReturnTimescale = Time.timeScale;
				Time.timeScale = 0;
				indTime = Time.realtimeSinceStartup;
				//pausePlayerVelocity = playerVelocity;
				//player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
				//objLight.GetComponent<autoIntensity>().dayRotateSpeed.x = 0;
				//objLight.GetComponent<autoIntensity>().nightRotateSpeed.x = 0;
			}
		}
	}

	void pickMusic(int musicPicker = 0, bool checkEnabled = true)	// picks a random music track to play
	{
		if (progressiveStep >= 17) {
			Debug.Log ("killing music pick in favor of scripted music.");
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
			
			if (Application.loadedLevelName == "game_progressive")
			{
				if (    !settings.optionsGameBrokeAtTheCountEnabled &&
					!settings.optionsGameBrokeBuildingTheSunEnabled &&
					!settings.optionsGameBrokeBlownOutEnabled &&
					!settings.optionsGameBrokeCalmTheFuckDownEnabled &&
					!settings.optionsGameBrokeCaughtInTheBeatEnabled &&
					!settings.optionsGameBrokeFuckItEnabled &&
					!settings.optionsGameBrokeHellaEnabled &&
					!settings.optionsGameBrokeHighSchoolSnapsEnabled &&
					!settings.optionsGameBrokeLikeSwimmingEnabled &&
					!settings.optionsGameBrokeLivingInReverseEnabled &&
					!settings.optionsGameBrokeLuminousEnabled &&
					!settings.optionsGameBrokeMellsParadeEnabled &&
					!settings.optionsGameBrokeSomethingElatedEnabled &&
					!settings.optionsGameBrokeTheGreatEnabled &&
					!settings.optionsGameChrisAnotherVersionOfYouEnabled &&
					!settings.optionsGameChrisDividerEnabled &&
					!settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled &&
					!settings.optionsGameChrisThe49thStreetGalleriaEnabled &&
					!settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled &&
					!settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled &&
					!settings.optionsGameDSMILEZInspirationEnabled &&
					!settings.optionsGameDSMILEZLetYourBodyMoveEnabled &&
					!settings.optionsGameDSMILEZLostInTheMusicEnabled &&
					!settings.optionsGameDecktonicNightDriveEnabled &&
					!settings.optionsGameDecktonicStarsEnabled &&
					!settings.optionsGameDecktonicWatchYourDubstepEnabled &&
					!settings.optionsGameDecktonicActIVEnabled &&
					!settings.optionsGameDecktonicBassJamEnabled &&
					!settings.optionsGameKaiEngelLowHorizonEnabled &&
					!settings.optionsGameKaiEngelNothingEnabled &&
					!settings.optionsGameKaiEngelSomethingEnabled &&
					!settings.optionsGameKaiEngelWakeUpEnabled &&
					!settings.optionsGamePierloBarbarianEnabled &&
					!settings.optionsGameParijat4thNightEnabled &&
					!settings.optionsGameRevolutionVoidHowExcitingEnabled &&
					!settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled &&
					!settings.optionsGameSergeyLabyrinthEnabled &&
					!settings.optionsGameSergeyNowYouAreHereEnabled &&
					!settings.optionsGameToursEnthusiastEnabled &&
					!settings.optionsGameADropBadInfluencesEnabled &&
					!settings.optionsGameADropErrorEnabled &&
					!settings.optionsGameADropFightOrDieEnabled &&
					!settings.optionsGameArsFarewellTheInnocentEnabled &&
					!settings.optionsGameArsSamaritanEnabled &&
					!settings.optionsGameGravityLittleThingsEnabled &&
					!settings.optionsGameGravityMicroscopeEnabled &&
					!settings.optionsGameGravityOldHabitsEnabled &&
					!settings.optionsGameGravityRadioactiveBoyEnabled &&
					!settings.optionsGameGravityTrainTracksEnabled)
				{
					//Debug.Log ("No songs are enabled in " + Application.loadedLevelName);
					return;
				}
			}
		}
		
		if (musicPicker == 0)
		{
			musicPicker = Random.Range(1, 51);
		}

		if (musicPicker == 1)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeAtTheCountEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeAtTheCountEnabled))
				{
					currentSong = GameObject.Find ("broke1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 2)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeBlownOutEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeBlownOutEnabled))
				{
					currentSong = GameObject.Find ("broke2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 3)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeBuildingTheSunEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeBuildingTheSunEnabled))
				{
					currentSong = GameObject.Find ("broke3");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke3");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 4)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeCalmTheFuckDownEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeCalmTheFuckDownEnabled))
				{
					currentSong = GameObject.Find ("broke4");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke4");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 5)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeCaughtInTheBeatEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeCaughtInTheBeatEnabled))
				{
					currentSong = GameObject.Find ("broke5");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke5");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 6)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeFuckItEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeFuckItEnabled))
				{
					currentSong = GameObject.Find ("broke6");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke6");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 7)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeHellaEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeHellaEnabled))
				{
					currentSong = GameObject.Find ("broke7");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke7");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 8)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeHighSchoolSnapsEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeHighSchoolSnapsEnabled))
				{
					currentSong = GameObject.Find ("broke8");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke8");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 9)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeLikeSwimmingEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeLikeSwimmingEnabled))
				{
					currentSong = GameObject.Find ("broke9");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke9");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 10)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeLivingInReverseEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeLivingInReverseEnabled))
				{
					currentSong = GameObject.Find ("broke10");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);
					//pickMusic(0, true);
				}
			}
			else
			{
				currentSong = GameObject.Find ("broke10");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 11)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeLuminousEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeLuminousEnabled))
				{
					currentSong = GameObject.Find ("broke11");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("broke11");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 12)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeMellsParadeEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeMellsParadeEnabled))
				{
					currentSong = GameObject.Find ("broke12");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("broke12");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 13)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeQuitBitchingEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeQuitBitchingEnabled))
				{
					currentSong = GameObject.Find ("broke13");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("broke13");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 14)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeSomethingElatedEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeSomethingElatedEnabled))
				{
					currentSong = GameObject.Find ("broke14");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("broke14");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 15)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeTheGreatEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameBrokeTheGreatEnabled))
				{
					currentSong = GameObject.Find ("broke15");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("broke15");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 16)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisAnotherVersionOfYouEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameChrisAnotherVersionOfYouEnabled))
				{
					currentSong = GameObject.Find ("chris1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("chris1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 17)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisDividerEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameChrisDividerEnabled))
				{
					currentSong = GameObject.Find ("chris2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("chris2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 18)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled))
				{
					currentSong = GameObject.Find ("chris3");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("chris3");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 19)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisThe49thStreetGalleriaEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameChrisThe49thStreetGalleriaEnabled))
				{
					currentSong = GameObject.Find ("chris4");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("chris4");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 20)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled))
				{
					currentSong = GameObject.Find ("chris5");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("chris5");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 21)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled))
				{
					currentSong = GameObject.Find ("chris6");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("chris6");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 22)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDSMILEZInspirationEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDSMILEZInspirationEnabled))
				{
					currentSong = GameObject.Find ("DSMILEZ1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("DSMILEZ1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 23)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDSMILEZLetYourBodyMoveEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDSMILEZLetYourBodyMoveEnabled))
				{
					currentSong = GameObject.Find ("DSMILEZ2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("DSMILEZ2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 24)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDSMILEZLostInTheMusicEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDSMILEZLostInTheMusicEnabled))
				{
					currentSong = GameObject.Find ("DSMILEZ3");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("DSMILEZ3");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 25)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicActIVEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDecktonicActIVEnabled))
				{
					currentSong = GameObject.Find ("decktonic1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("decktonic1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 26)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelLowHorizonEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameKaiEngelLowHorizonEnabled))
				{
					currentSong = GameObject.Find ("kai1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("kai1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 27)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelNothingEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameKaiEngelNothingEnabled))
				{
					currentSong = GameObject.Find ("kai2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("kai2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 28)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelSomethingEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameKaiEngelSomethingEnabled))
				{
					currentSong = GameObject.Find ("kai3");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("kai3");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 29)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelWakeUpEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameKaiEngelWakeUpEnabled))
				{
					currentSong = GameObject.Find ("kai4");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("kai4");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 30)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuPierloBarbarianEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGamePierloBarbarianEnabled))
				{
					currentSong = GameObject.Find ("pierlo1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("pierlo1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 31)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuParijat4thNightEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameParijat4thNightEnabled))
				{
					currentSong = GameObject.Find ("parijat1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("parijat1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 32)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuRevolutionVoidHowExcitingEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameRevolutionVoidHowExcitingEnabled))
				{
					currentSong = GameObject.Find ("revolution1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("revolution1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 33)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled))
				{
					currentSong = GameObject.Find ("revolution2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("revolution2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 34)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuSergeyLabyrinthEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameSergeyLabyrinthEnabled))
				{
					currentSong = GameObject.Find ("sergey1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("sergey1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 35)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuSergeyNowYouAreHereEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameSergeyNowYouAreHereEnabled))
				{
					currentSong = GameObject.Find ("sergey2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("sergey2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 36)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuToursEnthusiastEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameToursEnthusiastEnabled))
				{
					currentSong = GameObject.Find ("tours1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("tours1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 37)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicBassJamEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDecktonicBassJamEnabled))
				{
					currentSong = GameObject.Find ("decktonic2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("decktonic2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 38)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicNightDriveEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDecktonicNightDriveEnabled))
				{
					currentSong = GameObject.Find ("decktonic3");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("decktonic3");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 39)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicStarsEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDecktonicStarsEnabled))
				{
					currentSong = GameObject.Find ("decktonic4");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("decktonic4");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 40)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicWatchYourDubstepEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameDecktonicWatchYourDubstepEnabled))
				{
					currentSong = GameObject.Find ("decktonic5");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("decktonic5");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 41)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuADropBadInfluencesEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameADropBadInfluencesEnabled))
				{
					currentSong = GameObject.Find ("ADrop1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("ADrop1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 42)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuADropErrorEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameADropErrorEnabled))
				{
					currentSong = GameObject.Find ("ADrop2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("ADrop2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 43)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuADropFightOrDieEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameADropFightOrDieEnabled))
				{
					currentSong = GameObject.Find ("ADrop3");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("ADrop3");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 44)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuArsFarewellTheInnocentEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameArsFarewellTheInnocentEnabled))
				{
					currentSong = GameObject.Find ("Ars1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("Ars1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 45)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuArsSamaritanEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameArsSamaritanEnabled))
				{
					currentSong = GameObject.Find ("Ars2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("Ars2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 46)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityLittleThingsEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameGravityLittleThingsEnabled))
				{
					currentSong = GameObject.Find ("gravity1");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("gravity1");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 47)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityMicroscopeEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameGravityMicroscopeEnabled))
				{
					currentSong = GameObject.Find ("gravity2");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("gravity2");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 48)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityOldHabitsEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameGravityOldHabitsEnabled))
				{
					currentSong = GameObject.Find ("gravity3");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("gravity3");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 49)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityRadioactiveBoyEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameGravityRadioactiveBoyEnabled))
				{
					currentSong = GameObject.Find ("gravity4");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("gravity4");
				currentSong.GetComponent<AudioSource>().Play();
			}
		}
		else if (musicPicker == 50)
		{
			if (checkEnabled)
			{
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityTrainTracksEnabled) || (Application.loadedLevelName == "game_progressive" && settings.optionsGameGravityTrainTracksEnabled))
				{
					currentSong = GameObject.Find ("gravity5");
					currentSong.GetComponent<AudioSource>().Play();
				}
				else
				{
					Debug.Log ("Song " + musicPicker + " is not Enabled in " + Application.loadedLevelName);

				}
			}
			else
			{
				currentSong = GameObject.Find ("gravity5");
				currentSong.GetComponent<AudioSource>().Play();
			}
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
			if (progressiveStep >= 20 && playerPos.z > 4750) {
				return;
			}
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
				if (gameOverDoOnce == false && score >= 0 && (playerPos.z > 0 || timesCubesCut > 0) && EnablePlayerControls)
				{
					GUI.skin = CDGUILargeTextSkin;
					//GUI.Label (new Rect (25, 15, 200, 40), score, CDGUILargeTextSkin.customStyles.LargeTextFromLeft);
					GUI.Label (new Rect (25, 15, 1000, 40), "Score: " + Mathf.Floor (score), CDGUILargeTextSkin.FindStyle("LargeTextFromLeft"));

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
				
				if (gameOverDoOnce == false && EnablePlayerControls)
				{
					GUI.Label (new Rect (5, Screen.height - 20, 100, 20), gameVersion);
					GUI.Label (new Rect (80, Screen.height - 20, 250, 20), "Created by Ethan 'the Drake' Casey");

					//v1.1.0 additions --- UI for skipping song.
					if (showSongSkip && songSkipTimer < 3)
					{
						GUI.Label (new Rect (Screen.width/2 - (125/2), Screen.height - 20, 125, 20), "Skipping song...");
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
					if (gameOverTimer > 3 && exitToMenu == false && exitToDesktop == false)
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
						if (exitToMenu == false && exitToDesktop == false)
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

			if (initialCubeSpawner != null)
			{
				GUI.Label (new Rect (10, Screen.height - 30, 80, 25), "Loading...");
			}
			else if (introFadeInTimer < 10)
			{
				if (!Application.isMobilePlatform) {
					GUI.Label (new Rect (10, Screen.height - 30, 175, 25), "Press any key to continue");
				} else {
					GUI.Label (new Rect (10, Screen.height - 30, 175, 25), "Tap to continue");
				}
			}
		}
	}
}