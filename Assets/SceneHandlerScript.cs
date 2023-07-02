//Code written by Ethan 'the Drake' Casey
//Copyright 2016
//Contact me at "EthanDrakeCasey@gmail.com" for inquiries.
//=====================================================================

using UnityEngine;
using System.Collections;

public class SceneHandlerScript : MonoBehaviour {

	settingsScript settings;
	leaderboardsHolder leaderboards;
	AchievementsScript achievements;
	playerScript playerScript;

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
	string ErrorSettingsHolderMissing = "No Settings Holder Found. Settings will be set to default. If you're getting this error and you aren't directly launching into the game level, then this is a bug.";

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

	GameObject player; //player object
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
	//---------Randomizer---------------------
	bool randomizerDone;
	bool showRandomizerHUD;
	bool autoShowRandomizerHUD;
	int randomizerInt;
	int randomizerDifficulty;
	string randomizerDifficultyString;
	float randomizerSpeedMult;
	bool randomizerAirdrop;
	int randomizerCubeType;
	string randomizerCubeTypeString;
	int randomizerCameraType;
	string randomizerCameraTypeString;
	bool randomizerDrunkDriver;

    bool randomizerPowerUps;

	//bool randomizerVortex;

	int initialDifficulty;
	float initialSpeedMult;
	bool initialAirdrop;
	int initialCubeType;
	int initialCameraType;
	bool initialDrunkDriver;
    bool initialPowerups;
	//----------------------------------------
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

	bool referenceCubesHaveRB;
	public float pauseReturnTimescale;
	float indTime;
	bool mobile_pauseButtonPressed;
	bool mobile_powerupButtonPressed;	
	public int cubeXMin = -150;
	public int cubeXMax = 150;
	public float cubeSpawnRateMultiplier = 1f;

	//2.1
	GameObject randomizer0;
	GameObject randomizer2500;
	GameObject randomizerNegative2500;

	//3.0
	int easyCubeSpawnRate = 3000;
	int easyZVelocity = 20;
	int normalCubeSpawnRate = 6000;
	int normalZVelocity = 30;
	int hardCubeSpawnRate = 10000;
	int hardZVelocity = 40;
	int extremeCubeSpawnRate = 32000;
	int extremeZVelocity = 50;

	// Use this for initialization
	void Start () 
	{
		if (Cursor.visible == true)
		{
			Cursor.visible = false;
		}

		settingsHolder = GameObject.Find ("settingsHolder");
		settings = settingsHolder.GetComponent<settingsScript> ();
		leaderboards = settingsHolder.GetComponent<leaderboardsHolder> ();
		achievements = settingsHolder.GetComponent<AchievementsScript> ();
		if (settingsHolder == null)
		{
			Debug.Log (ErrorSettingsHolderMissing);
			Application.LoadLevel("loading_menu");
		}
		else
		{
			gameVersion = settings.gameVersion;
			settings.doMenuIntro = false;
			difficulty = Mathf.FloorToInt(settings.difficulty);
			difficultyString = settings.difficultyString;

			if (
				settings.modifierRandomizer != false ||
				settings.modifierSpeedMultiplier != 1 ||
				settings.modifierDrunkDriver != false ||
				settings.modifierSeizure != false ||
				settings.modifierCubeType != 0 ||
				settings.modifierCameraType != 0 ||
				settings.modifierAirdrop != false ||
				settings.modifierRandomVelocity != false ||
				settings.modifierFarsighted != false ||
				settings.modifierNearsighted != false ||
                		settings.modifierPowerups != false
				)
			{
				modifierString = "| ";
				if (settings.modifierSpeedMultiplier != 1)
				{
					modifierString = modifierString + "Speed: " + settings.modifierSpeedMultiplier.ToString() + "x | ";
				}
				if (settings.modifierRandomizer)
				{
					modifierString = modifierString + "Randomizer | ";

					initialDifficulty = difficulty;
					initialSpeedMult = settings.modifierSpeedMultiplier;
					initialAirdrop = settings.modifierAirdrop;
					initialCubeType = settings.modifierCubeType;
					initialCameraType = settings.modifierCameraType;
					initialDrunkDriver = settings.modifierDrunkDriver;
                    initialPowerups = settings.modifierPowerups;
				}
				if (settings.modifierAirdrop)
				{
					modifierString = modifierString + "Airdrop | ";
				}
				if (settings.modifierCameraType != 0)
				{
					modifierString = modifierString + "Camera: " + settings.modifierCameraTypeString + " | ";
				}
				if (settings.modifierCubeType != 0)
				{
					modifierString = modifierString + "Cube Type: " + settings.modifierCubeTypeString + " | ";
				}
				if (settings.modifierDrunkDriver)
				{
					modifierString = modifierString + "Drunk Driver | ";
				}
                if (settings.modifierPowerups)
                {
                    modifierString = modifierString + "Power Ups | ";
                }
			}
		}
		//1.1.0, needed to set these for initial values so that if the player hits Q prior to hitting the 500 mark, it doesn't come up blank.
		randomizerDifficultyString = settings.difficultyString;
		randomizerAirdrop = settings.modifierAirdrop;
		randomizerCameraTypeString = settings.modifierCameraTypeString;
		randomizerCubeTypeString = settings.modifierCubeTypeString;
		randomizerDrunkDriver = settings.modifierDrunkDriver;
		randomizerSpeedMult = settings.modifierSpeedMultiplier;
        randomizerPowerUps = settings.modifierPowerups;

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

		powerup_JumpRef = GameObject.Find ("referenceJumpPU");
		powerup_BubbleRef = GameObject.Find ("referenceBubblePU");
		powerup_SlowmoRef = GameObject.Find ("referenceSlomoPU");
		powerup_ShootRef = GameObject.Find ("referenceShootPU");

        	projectileRef = GameObject.Find("reference_projectile");
		bubbleRef = GameObject.Find ("reference_bubble");

		initialCubeSpawner = GameObject.Find ("initialCubeSpawn");

		farCubeCut = GameObject.Find ("cutCubePattern_far");
		nearCubeCut = GameObject.Find ("cutCubePattern_near");
		rightXLimiter = GameObject.Find ("XLimiter_right");
		leftXLimiter = GameObject.Find ("XLimiter_left");
		rightXLimiter_particle = GameObject.Find ("xLimiterParticle_Right");
		leftXLimiter_particle = GameObject.Find ("xLimiterParticle_Left");

		randomizer0 = GameObject.Find ("randomizerCubeCutter0");
		randomizer0.SetActive (false);
		randomizer2500 = GameObject.Find ("randomizerCubeCutter2500");
		randomizer2500.SetActive (false);
		randomizerNegative2500 = GameObject.Find ("randomizerCubeCutter-2500");
		randomizerNegative2500.SetActive (false);

		player = GameObject.Find ("playerREF");
		playerScript = player.GetComponent<playerScript> ();
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
		if (!achievements.ach5000Easy && score >= 5000 && settings.difficulty >= 0 && !settings.modifierRandomizer) {
			achievements.ach5000Easy = true;
			achievements.queueAch (6);
			achievements.saveAchievements ();
		}
		if (!achievements.ach5000Standard && score >= 5000 && settings.difficulty >= 1 && !settings.modifierRandomizer) {
			achievements.ach5000Standard = true;
			achievements.queueAch (7);
			achievements.saveAchievements ();
		}
		if (!achievements.ach5000Hard && score >= 5000 && settings.difficulty >= 2 && !settings.modifierRandomizer) {
			achievements.ach5000Hard = true;
			achievements.queueAch (8);
			achievements.saveAchievements ();
		}
		if (!achievements.ach5000Extreme && score >= 5000 && settings.difficulty >= 3 && !settings.modifierRandomizer) {
			achievements.ach5000Extreme = true;
			achievements.queueAch (9);
			achievements.saveAchievements ();
		}
		if (!achievements.ach1000Airdrop && score >= 1000 && settings.modifierAirdrop && !settings.modifierRandomizer) {
			achievements.ach1000Airdrop = true;
			achievements.queueAch (10);
			achievements.saveAchievements ();
		}
		if (!achievements.achDie && gameOverDoOnce) {
			achievements.achDie = true;
			achievements.queueAch (11);
			achievements.saveAchievements ();
		}
		if (!achievements.ach1000Spheres && score >= 1000 && settings.modifierCubeType == 1 && !settings.modifierRandomizer) {
			achievements.ach1000Spheres = true;
			achievements.queueAch (12);
			achievements.saveAchievements ();
		}
		if (!achievements.ach1000DrunkDriver && score >= 1000 && settings.modifierDrunkDriver && !settings.modifierRandomizer) {
			achievements.ach1000DrunkDriver = true;
			achievements.queueAch (13);
			achievements.saveAchievements ();
		}
		if (!achievements.ach1000Powerups && score >= 1000 && settings.modifierPowerups && !settings.modifierRandomizer) {
			achievements.ach1000Powerups = true;
			achievements.queueAch (14);
			achievements.saveAchievements ();
		}
		if (!achievements.ach1000SpeedMult && score >= 1000 && settings.modifierSpeedMultiplier >= 1.5f && !settings.modifierRandomizer) {
			achievements.ach1000SpeedMult = true;
			achievements.queueAch (15);
			achievements.saveAchievements ();
		}
		if (!achievements.achXLimiters && (player.transform.position.x < -4750 || player.transform.position.x > 4750)) {
			achievements.achXLimiters = true;
			achievements.queueAch (16);
			achievements.saveAchievements ();
		}
		if (!achievements.ach10000Randomizer && score >= 10000 && settings.modifierRandomizer) {
			achievements.ach10000Randomizer = true;
			achievements.queueAch (17);
			achievements.saveAchievements ();
		}
		if (!achievements.achBrokenCubeCutters && (player.transform.position.z > 4780 || player.transform.position.z < -4780) && (player.transform.position.x < farCubeCut.transform.position.x - 5 || player.transform.position.x > farCubeCut.transform.position.x + 5)) {
			achievements.achBrokenCubeCutters = true;
			achievements.queueAch (24);
			achievements.saveAchievements ();
		}
		if (!achievements.ach500ExtremeWithAllModifiers && score >= 500 && settings.difficulty == 3 && settings.modifierSpeedMultiplier >= 2 && settings.modifierAirdrop && settings.modifierCameraType == 2 && settings.modifierCubeType == 1 && settings.modifierDrunkDriver && settings.modifierPowerups) {
			achievements.ach500ExtremeWithAllModifiers = true;
			achievements.queueAch (26);
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

			if (settings.modifierRandomizer)
			{
				showRandomizerHUD = true;
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

			if (settings.modifierRandomizer) {
				checkForNewRandomizerStuff ();
			}

			if (playerPos.z >= 4700 && randomizerDone == false)
			{
				if (settings.modifierRandomizer)
				{
					randomizerConfigure();
				}
			}

			if (playerPos.z >= 5000)
			{
				cubeCutScore = score;
				scoreUpdateZCoord = -4999;
				player.transform.position = new Vector3(playerPos.x, playerPos.y, -5000);
				timesCubesCut = timesCubesCut + 1;
				if (settings.modifierRandomizer)
				{
					randomizerRandomize();
					settings.checkScoreMultiplier();
				}

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

			if (EnablePlayerControls == false && playerPos.z > 0) {
				EnablePlayerControls = true;
			}
			
			if (gameOverDoOnce == false) 
			{
				if (HaveCheatsBeenEnabled == false)
				{
					if (playerPos.z/10 > scoreUpdateZCoord)
					{
						score = score + (1*settings.scoreMultiplier);
						scoreUpdateZCoord = playerPos.z/10 + 1;
					}
				}
				else
				{
					score = 0;
				}

				if (currentSong != null && slowmoInt == 0)
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
				if (currentSong == null || currentSong.GetComponent<AudioSource>().isPlaying == false)
				{
					pickMusic();
				}
				else if (Input.GetKeyUp (KeyCode.F))
				{
					pickMusic();
					showSongSkip = true;
				}
				
				if ((playerPos.z + cubeSpawnDistance < 4750 && playerPos.z + cubeSpawnDistance > -4750) &&
					(!settings.modifierRandomizer || (playerPos.z + cubeSpawnDistance < -250 || playerPos.z + cubeSpawnDistance > 250 || timesCubesCut == 0)) &&
					(!settings.modifierRandomizer || (playerPos.z + cubeSpawnDistance < 2250 || playerPos.z + cubeSpawnDistance > 2750)) &&
					(!settings.modifierRandomizer || (playerPos.z + cubeSpawnDistance < -2750 || playerPos.z + cubeSpawnDistance > -2250))

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
			modifiersCameraType();
			modifiersDrunkDriver();

			if (settings.modifierPowerups) {
				powerupJump ();
				powerupSlowmo ();
			}
		}

	}

	void checkForNewRandomizerStuff(){
		if (((playerPos.z >= -300 && playerPos.z <= -200) || (playerPos.z >= -2800 && playerPos.z <= -2700) || (playerPos.z >= 2200 && playerPos.z <= 2300)) && randomizerDone == false)
		{
			randomizerConfigure();
		}

		if ((playerPos.z >= 0 && randomizer0.activeSelf) || (playerPos.z >= 2500 && randomizer2500.activeSelf) || (playerPos.z >= -2500 && randomizerNegative2500.activeSelf))
		{
			randomizerRandomize();
			settings.checkScoreMultiplier();
		}

		if (Input.GetKey (KeyCode.Q) || autoShowRandomizerHUD) {
			if (!showRandomizerHUD) {
				showRandomizerHUD = true;
			}
		} else if (!Input.GetKey (KeyCode.Q) && !autoShowRandomizerHUD) {
			if (showRandomizerHUD) {
				showRandomizerHUD = false;
			}
		}
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

	void modifiersDrunkDriver()
	{
		if (settings.modifierDrunkDriver) {
			if (!gameCameraBlur.enabled) {
				gameCameraBlur.enabled = true;
			}
			
			if (settings.modifierDrunkDriverCamAnimSwitch == 0) {
				settings.modifierDrunkDriverCamAnimAmount = settings.modifierDrunkDriverCamAnimAmount - 0.0025f;
				gameCamera.transform.position = new Vector3 (cameraPos.x, cameraPos.y, cameraPos.z + settings.modifierDrunkDriverCamAnimAmount);
				gameCamera.transform.Rotate (new Vector3 (Random.Range (-0.03f, 0.01f), 0, 0));
				if (settings.modifierDrunkDriverCamAnimAmount <= -0.5f) {
					settings.modifierDrunkDriverCamAnimSwitch = 1;
				}
			} else if (settings.modifierDrunkDriverCamAnimSwitch == 1) {
				settings.modifierDrunkDriverCamAnimAmount = settings.modifierDrunkDriverCamAnimAmount + 0.0025f;
				gameCamera.transform.position = new Vector3 (cameraPos.x, cameraPos.y, cameraPos.z + settings.modifierDrunkDriverCamAnimAmount);
				gameCamera.transform.Rotate (new Vector3 (Random.Range (-0.01f, 0.03f), 0, 0));
				if (settings.modifierDrunkDriverCamAnimAmount >= 0.5f) {
					settings.modifierDrunkDriverCamAnimSwitch = 0;
				}
			}
		} else if (!gameOverDoOnce) {
			if (gameCameraBlur.enabled) {
				gameCameraBlur.enabled = false;
			}
			
			gameCamera.transform.position = cameraPos;
			if (settings.modifierCameraType == 0 && !settings.screenshotMode) {
				gameCamera.transform.eulerAngles = new Vector3 (0, 0, 0);
			} else if (!settings.screenshotMode) {
				gameCamera.transform.eulerAngles = new Vector3 (0, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
				
			}
		}
	}

	void modifiersCameraType()
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
					}*/
			gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y, 0);
		}
	}


	void spawnNewCube()
	{
		if (settings.modifierAirdrop) {
			if (difficulty == 0) {
				cubeSpawnDistance = Mathf.FloorToInt(player.transform.position.z + 75);
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
				}
				cubeSpawnDistance = 175;
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
				newCube.AddComponent<powerUpScript> ();
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
			//newCube.AddComponent<cubeDeleteScript>();
			newCube.GetComponent<cubeDeleteScript>().isReferenceCube = false;
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

	void randomizerConfigure()
	{
		randomizerDifficulty = Random.Range (0, 4);
		if (randomizerDifficulty == 0) {
			randomizerDifficultyString = "Easy";
		} else if (randomizerDifficulty == 1) {
			randomizerDifficultyString = "Normal";
		} else if (randomizerDifficulty == 2) {
			randomizerDifficultyString = "Hard";
		} else if (randomizerDifficulty == 3) {
			randomizerDifficultyString = "Extreme";
		}
		randomizerSpeedMult = ((Random.Range (2, 8)) / 4f);
		randomizerInt = Random.Range (0, 2);
		if (randomizerInt == 1) {
			randomizerAirdrop = true;
		} else {
			randomizerAirdrop = false;
		}
		randomizerCubeType = Random.Range (0, 3);
		if (randomizerCubeType == 0) {
			randomizerCubeTypeString = "Cubes";
		} else if (randomizerCubeType == 1) {
			randomizerCubeTypeString = "Spheres";
		} else if (randomizerCubeType == 2) {
			randomizerCubeTypeString = "Mixed";
		}
		randomizerInt = Random.Range (0, 3);
		randomizerCameraType = randomizerInt;
		if (randomizerInt == 0) {

			randomizerCameraTypeString = "Static";
		} else if (randomizerInt == 1) {
			randomizerCameraTypeString = "Upside Down";
		} else if (randomizerInt == 2) {
			randomizerCameraTypeString = "Spinning";
		}
		randomizerInt = Random.Range (0, 2);
		if (randomizerInt == 0) {
			randomizerDrunkDriver = false;
		} else if (randomizerInt == 1) {
			randomizerDrunkDriver = true;
		}
		randomizerInt = Random.Range (0, 2);
		if (randomizerInt == 0) {
			randomizerPowerUps = false;
		} else if (randomizerInt == 1) {
			randomizerPowerUps = true;
		}

		autoShowRandomizerHUD = true;
		randomizerDone = true;
	}

	void randomizerRandomize()
	{
		settings.difficulty = randomizerDifficulty;
		difficulty = randomizerDifficulty;
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
		
		settings.modifierSpeedMultiplier = randomizerSpeedMult;
		settings.modifierAirdrop = randomizerAirdrop;
		settings.modifierCubeType = randomizerCubeType;
		settings.modifierCubeTypeString = randomizerCubeTypeString;
		settings.modifierCameraType = randomizerCameraType;
		settings.modifierDrunkDriver = randomizerDrunkDriver;
        	settings.modifierPowerups = randomizerPowerUps;
		
		randomizerDone = false;
	}

	void spawnFarCube()
	{
		newFarCube = Instantiate (BlackCubeREF);
		newFarCube.AddComponent<farCubeDeleteScript> ();
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
				if (settings.modifierRandomizer) {
					autoShowRandomizerHUD = false;
				}
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
				if (settings.modifierRandomizer) {
					autoShowRandomizerHUD = false;
				}
			}
		}

		if (settings.modifierRandomizer) {
			
			if (playerPos.z >= -2200 && timesCubesCut > 0) {
				if (randomizerNegative2500.activeSelf) {
					randomizerNegative2500.SetActive (false);
					autoShowRandomizerHUD = false;
				}
			} else if (playerPos.z >= -3200 && timesCubesCut > 0) {
				if (!randomizerNegative2500.activeSelf) {					
					randomizerNegative2500.transform.position = new Vector3 (playerPos.x, randomizerNegative2500.transform.position.y, randomizerNegative2500.transform.position.z);
					randomizerNegative2500.SetActive (true);
				}
			}

			if (playerPos.z >= 300 && timesCubesCut > 0) {
				if (randomizer0.activeSelf) {
					randomizer0.SetActive (false);
					autoShowRandomizerHUD = false;
				}
			} else if (playerPos.z >= -700 && timesCubesCut > 0) {
				if (!randomizer0.activeSelf) {
					randomizer0.transform.position = new Vector3 (playerPos.x, randomizer0.transform.position.y, randomizer0.transform.position.z);
					randomizer0.SetActive (true);
				}
			}

			if (playerPos.z >= 2800) {
				if (randomizer2500.activeSelf) {
					randomizer2500.SetActive (false);
					autoShowRandomizerHUD = false;
				}
			} else if (playerPos.z >= 1800) {
				if (!randomizer2500.activeSelf) {
					randomizer2500.transform.position = new Vector3 (playerPos.x, randomizer2500.transform.position.y, randomizer2500.transform.position.z);
					randomizer2500.SetActive (true);
				}
			}

		}
	}

	void updateXLimiters()
	{
		if (playerPos.x > 4500)
		{
			if (rightXLimiter.activeSelf == false)
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
			if (leftXLimiter.activeSelf == false)
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
			player.transform.position = new Vector3(0,0,-50);
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
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x + (Time.deltaTime);
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y + (Time.deltaTime);
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().iterations = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().iterations + 1;
			}
			else if (gameOverTimer > 1)
			{
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().rampOffset - (Time.deltaTime)/3;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.x + (Time.deltaTime)/2;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().radius.y + (Time.deltaTime)/2;
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

			randomizerReset();
			Time.timeScale = 1;
			Application.LoadLevel("game");

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
			randomizerReset ();
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
			randomizerReset ();
			Cursor.visible = true;
			Application.Quit();
		}
	}

	void randomizerReset()
	{
		if (settings.modifierRandomizer)
		{
			settings.difficulty = initialDifficulty;
			settings.modifierSpeedMultiplier = initialSpeedMult;
			settings.modifierAirdrop = initialAirdrop;
			settings.modifierCubeType = initialCubeType;
			settings.modifierCameraType = initialCameraType;
			settings.modifierDrunkDriver = initialDrunkDriver;
            		settings.modifierPowerups = initialPowerups;
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

			if ((Input.GetKeyDown(KeyCode.Escape) || mobile_pauseButtonPressed) && EnablePlayerControls && exitToMenu == false && exitToDesktop == false)
			{
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
		Debug.Log (musicPicker + ", " + checkEnabled);
		
		if (currentSong != null)
		{
			currentSong.GetComponent<AudioSource>().Stop();
		}
		
		if (checkEnabled)
		{
			if (Application.loadedLevelName == "game")
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeAtTheCountEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeAtTheCountEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeBlownOutEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeBlownOutEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeBuildingTheSunEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeBuildingTheSunEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeCalmTheFuckDownEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeCalmTheFuckDownEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeCaughtInTheBeatEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeCaughtInTheBeatEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeFuckItEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeFuckItEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeHellaEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeHellaEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeHighSchoolSnapsEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeHighSchoolSnapsEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeLikeSwimmingEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeLikeSwimmingEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeLivingInReverseEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeLivingInReverseEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeLuminousEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeLuminousEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeMellsParadeEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeMellsParadeEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeQuitBitchingEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeQuitBitchingEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeSomethingElatedEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeSomethingElatedEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuBrokeTheGreatEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameBrokeTheGreatEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisAnotherVersionOfYouEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameChrisAnotherVersionOfYouEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisDividerEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameChrisDividerEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisThe49thStreetGalleriaEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameChrisThe49thStreetGalleriaEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDSMILEZInspirationEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDSMILEZInspirationEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDSMILEZLetYourBodyMoveEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDSMILEZLetYourBodyMoveEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDSMILEZLostInTheMusicEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDSMILEZLostInTheMusicEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicActIVEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDecktonicActIVEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelLowHorizonEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameKaiEngelLowHorizonEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelNothingEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameKaiEngelNothingEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelSomethingEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameKaiEngelSomethingEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuKaiEngelWakeUpEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameKaiEngelWakeUpEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuPierloBarbarianEnabled) || (Application.loadedLevelName == "game" && settings.optionsGamePierloBarbarianEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuParijat4thNightEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameParijat4thNightEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuRevolutionVoidHowExcitingEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameRevolutionVoidHowExcitingEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuSergeyLabyrinthEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameSergeyLabyrinthEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuSergeyNowYouAreHereEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameSergeyNowYouAreHereEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuToursEnthusiastEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameToursEnthusiastEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicBassJamEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDecktonicBassJamEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicNightDriveEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDecktonicNightDriveEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicStarsEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDecktonicStarsEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuDecktonicWatchYourDubstepEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameDecktonicWatchYourDubstepEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuADropBadInfluencesEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameADropBadInfluencesEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuADropErrorEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameADropErrorEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuADropFightOrDieEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameADropFightOrDieEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuArsFarewellTheInnocentEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameArsFarewellTheInnocentEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuArsSamaritanEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameArsSamaritanEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityLittleThingsEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameGravityLittleThingsEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityMicroscopeEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameGravityMicroscopeEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityOldHabitsEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameGravityOldHabitsEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityRadioactiveBoyEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameGravityRadioactiveBoyEnabled))
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
				if ((Application.loadedLevelName == "menu" && settings.optionsMenuGravityTrainTracksEnabled) || (Application.loadedLevelName == "game" && settings.optionsGameGravityTrainTracksEnabled))
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


				if (showRandomizerHUD) {
					GUI.Box (new Rect (Screen.width - 225, 15, 200, 200), "Randomizer", CDGUIGameSkin.FindStyle ("randomizerBox"));
					GUI.Label (new Rect (Screen.width - 225, 35, 200, 20), "Difficulty: " + randomizerDifficultyString);
					GUI.Label (new Rect (Screen.width - 225, 45, 200, 20), "Speed: " + randomizerSpeedMult + "x");
					GUI.Label (new Rect (Screen.width - 225, 55, 200, 20), "Airdrop: " + randomizerAirdrop);
					GUI.Label (new Rect (Screen.width - 225, 65, 200, 20), "Camera Type: " + randomizerCameraTypeString);
					GUI.Label (new Rect (Screen.width - 225, 75, 200, 20), "Cube Type: " + randomizerCubeTypeString);
					GUI.Label (new Rect (Screen.width - 225, 85, 200, 20), "Drunk Driver: " + randomizerDrunkDriver);
					GUI.Label (new Rect (Screen.width - 225, 95, 200, 20), "Power Ups: " + randomizerPowerUps);
				}

			}
			else if (exitToMenu == false && exitToDesktop == false)
			{
				if (showRandomizerHUD && gameOverDoOnce == false)
				{
					GUI.Box (new Rect (Screen.width - 225, 15, 200, 200), "Randomizer", CDGUIGameSkin.FindStyle("randomizerBox"));
					GUI.Label (new Rect (Screen.width - 225, 35, 200, 20), "Difficulty: " + randomizerDifficultyString);
					GUI.Label (new Rect (Screen.width - 225, 45, 200, 20), "Speed: " + randomizerSpeedMult + "x");
					GUI.Label (new Rect (Screen.width - 225, 55, 200, 20), "Airdrop: " + randomizerAirdrop);
					GUI.Label (new Rect (Screen.width - 225, 65, 200, 20), "Camera Type: " + randomizerCameraTypeString);
					GUI.Label (new Rect (Screen.width - 225, 75, 200, 20), "Cube Type: " + randomizerCubeTypeString);
					GUI.Label (new Rect (Screen.width - 225, 85, 200, 20), "Drunk Driver: " + randomizerDrunkDriver);
                    			GUI.Label(new Rect(Screen.width - 225, 95, 200, 20), "Power Ups: " + randomizerPowerUps);
                    //GUI.Label (new Rect(Screen.width - 225, 200, 200, 20), "Score Multiplier: " + randomizerScoreMultiplier);
				}

				if (gameOverDoOnce == false && score >= 0 && (playerPos.z > 0 || timesCubesCut > 0))
				{
					GUI.skin = CDGUILargeTextSkin;
					//GUI.Label (new Rect (25, 15, 200, 40), score, CDGUILargeTextSkin.customStyles.LargeTextFromLeft);
					GUI.Label (new Rect (25, 15, 1000, 40), "Score: " + Mathf.Floor (score), CDGUILargeTextSkin.FindStyle("LargeTextFromLeft"));
					
					if (Application.platform == RuntimePlatform.Android || settings.simulateMobileOptimization) {

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


					if (currentPowerup != 0) {
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
					if (HaveCheatsBeenEnabled) {
						GUI.Label (new Rect (25, 50, 500, 20), "Cheats have been enabled, scoring turned off.");
					}
				}
				
				if (playerScript.initBrighten == true) {
					if (gameOverTimer > 0) {
						//Debug.Log ("GameOverBrighten");
						if (GUIColor.r < 1 && GUIColor.g < 1 && GUIColor.b < 1) {
							GUIColor.r = GUIColor.r + Time.deltaTime / 4;
							GUIColor.g = GUIColor.g + Time.deltaTime / 4;
							GUIColor.b = GUIColor.b + Time.deltaTime / 4;
						}
					} else {
						if (GUIColor.r < 1 && GUIColor.g < 1 && GUIColor.b < 1) {
							GUIColor.r = GUIColor.r + Time.deltaTime * (objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x) / 8;
							GUIColor.g = GUIColor.g + Time.deltaTime * (objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x) / 8;
							GUIColor.b = GUIColor.b + Time.deltaTime * (objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x) / 8;
						}
					}
					
				}
				if (playerScript.initDarken == true) {
					if (gameOverTimer > 0) {
						//Debug.Log ("GameOverDarken");
						if (GUIColor.r > 0 && GUIColor.g > 0 && GUIColor.b > 0) {
							GUIColor.r = GUIColor.r - Time.deltaTime / 4;
							GUIColor.g = GUIColor.g - Time.deltaTime / 4;
							GUIColor.b = GUIColor.b - Time.deltaTime / 4;
						}
					} else {
						if (GUIColor.r > 0 && GUIColor.g > 0 && GUIColor.b > 0) {
							GUIColor.r = GUIColor.r - Time.deltaTime * (objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x) / 8;
							GUIColor.g = GUIColor.g - Time.deltaTime * (objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x) / 8;
							GUIColor.b = GUIColor.b - Time.deltaTime * (objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x) / 8;
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

						if (!settings.modifierRandomizer)
						{
							GUI.Label (new Rect( Screen.width/2 - 500, 20, 1000, 75), settings.difficultyString, CDGUIMenuSkin.FindStyle ("leaderboards1"));
						}
						else
						{
							GUI.Label (new Rect( Screen.width/2 - 500, 20, 1000, 75), "Randomizer", CDGUIMenuSkin.FindStyle ("leaderboards1"));
						}
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

					GUI.Label (new Rect(0, 150, Screen.width, 200),"Modifiers:\n" + modifierString);
					
					if (IntroColor2.r < 1 || IntroColor2.g < 1 || IntroColor2.b < 1)
					{
						IntroColor2.r = IntroColor2.r + Time.deltaTime;
						IntroColor2.g = IntroColor2.g + Time.deltaTime;
						IntroColor2.b = IntroColor2.b + Time.deltaTime;
					}
				}
				else if (IntroColor2.r > 0 || IntroColor2.g > 0 || IntroColor2.b > 0)
				{
					GUI.Label (new Rect(0, 150, Screen.width, 200),"Modifiers:\n" + modifierString);
					
					IntroColor2.r = IntroColor2.r - Time.deltaTime;
					IntroColor2.g = IntroColor2.g - Time.deltaTime;
					IntroColor2.b = IntroColor2.b - Time.deltaTime;
					
				}

				GUI.color = IntroColor3;

				if (introShowDifficulty)
				{
					GUI.Label (new Rect(Screen.width / 2 - 100, 375, 200, 60),"Difficulty: " + difficultyString);
					
					if (IntroColor3.r < 1 || IntroColor3.g < 1 || IntroColor3.b < 1)
					{
						IntroColor3.r = IntroColor3.r + Time.deltaTime;
						IntroColor3.g = IntroColor3.g + Time.deltaTime;
						IntroColor3.b = IntroColor3.b + Time.deltaTime;
					}
				}
				else if (IntroColor3.r > 0 || IntroColor3.g > 0 || IntroColor3.b > 0)
				{
					GUI.Label (new Rect(Screen.width / 2 - 100, 375, 200, 60),"Difficulty: " + difficultyString);
					
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
