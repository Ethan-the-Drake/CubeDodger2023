//Code written by Ethan 'the Drake' Casey
//Copyright 2016
//Contact me at "EthanDrakeCasey@gmail.com" for inquiries.
//=====================================================================

using UnityEngine;
using System.Collections;

public class menuHandlerScript : MonoBehaviour {

	settingsScript settings;
	leaderboardsHolder leaderboards;
	AchievementsScript achievements;
	playerScript playerScript;


	string gameVersion;	

	string changelogsString = "3.0 Update\nTODO: Add changelog notes";
	//ChangelogsString is not a comprehensive changelog, but just user-friendly highlights that changes with each update.

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
	public Texture controlsTutorialPC;
	public Texture controlsTutorialMobile;
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
	public bool showProgressiveOptions;
	public bool showAchievements;
	int achListGUIHeight;

	public bool isTurnedAround;
	//-------------------------

	public bool showFirstTimeMessage;
	public bool showNewVersion;

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
	bool referenceCubesHaveRB;
	public int mobile_showHelp; // 0 = Nothing, 1 = Difficulty, 2 = Speed Mult., 3 = Airdrop, 4 = Cube Type, 5 = Camera Mode, 6 = Drunk Driver, 7 = Power ups, 8 = musicError, 9 = Music Farewell, 10 = Music Samar, 11 = Music Little Things
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
		if (settings.doMenuIntro == false)
		{
			panCameraDown = true;
			showStartup = false;
		}

		leaderboards = settingsHolder.GetComponent<leaderboardsHolder> ();
		achievements = settingsHolder.GetComponent<AchievementsScript> ();
		if (settings.isNewVerison) {
			showNewVersion = true;
		}

		Cursor.visible = false;
		//settings.optionsPresetQuality = QualitySettings.GetQualityLevel ();

		RedCubeREF = GameObject.Find ("referenceRedCube");
		GreenCubeREF = GameObject.Find ("referenceGreenCube");
		BlueCubeREF = GameObject.Find ("referenceBlueCube");

		RedBallREF = GameObject.Find ("referenceRedBall");
		GreenBallREF = GameObject.Find ("referenceGreenBall");
		BlueBallREF = GameObject.Find ("referenceBlueBall");

        	//powerup refs 1.2.0
        	powerup_JumpREF = GameObject.Find("referenceJumpPU");
        	powerup_ShootREF = GameObject.Find("referenceShootPU");
        	powerup_SlowmoREF = GameObject.Find("referenceSlomoPU");
        	powerup_BubbleREF = GameObject.Find("referenceBubblePU");
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

		if (settings.gameCamera == null)
		{
			settings.gameCamera = gameCamera;
		}

		gameCameraScreenOverlay = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ();
		gameCameraDoF = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ();
		gameCameraBlur = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur> ();
        	//gameCameraVortex = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.Vortex> ();

        	showGraphicsOptions = true;
		showAudioOptions = false;

		pickMusic ();

		/*if (settings.useAndroidLighting) {
			objLight.GetComponent<Light>().ba
		}*/
		if (Application.isMobilePlatform || settings.simulateMobileOptimization) {
			doMobileOptimization ();
		}
		InvokeRepeating ("Update1s", 0, 1);
	}

	void doMobileOptimization (){
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunShaftIntensity = 0;
		gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = false;

		cubeXMin = -62;
		cubeXMax = -1;
		cubeSpawnRate = 0.04f;

		Screen.sleepTimeout = SleepTimeout.SystemSetting;
	}

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

		if (playerPos.z + 250 < 4750 && playerPos.z + 250 > -4750 && !showCredits) {
			//spawnNewCube();
			if (IsInvoking ("spawnNewCube") == false) {
				InvokeRepeating ("spawnNewCube", 0, cubeSpawnRate);
			}
		} else {
			if (IsInvoking ("spawnNewCube") == true) {
				CancelInvoke ();
			}
		}

		updateCubeCutters ();

		// If the player is below the speed the difficulty demands, raise him to that speed.
		if (playerVelocity.z != (difficultyZVelocity * settings.modifierSpeedMultiplier) && !showCredits)
		{
			rb.velocity = new Vector3(playerVelocity.x, playerVelocity.y, (difficultyZVelocity * settings.modifierSpeedMultiplier));
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

		if (panCameraDown == false && showStartup == false && (creditsStep < 2 || creditsStep > 4))
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts>().enabled = false; //disable sunshafts after the initial camera pan. You don't see them again and they cause weird flashing.
		}

		if (initLoadGame == true)
		{
			loadGameTransition();
		}

		if (initExitGame == true)
		{
			exitGameTransition();
		}

		if (currentSong == null || currentSong.GetComponent<AudioSource>().isPlaying == false && (creditsStep < 2 || creditsStep > 4))
		{
			pickMusic();
		}
			
		if (doCreditsTransition) {
			creditsTransition ();
		}

		if (fatMenuTransUp)
		{
			fatMenuTransitionUp();
		}

		if (fatMenuTransDown)
		{
			fatMenuTransitionDown();
		}

		if (pickGameTransDown)
		{
			pickGameTransitionDown();
		}

		if (pickGameTransUp)
		{
			pickGameTransitionUp();
		}

		if (showExtras || showGameModeSelection || showCredits)
		{
			if (gameCameraDoF.focalLength > 0)
			{
				gameCameraDoF.focalLength = gameCameraDoF.focalLength - 25 * Time.deltaTime;
			}
			if (gameCameraDoF.focalSize > 0)
			{
				gameCameraDoF.focalSize = gameCameraDoF.focalSize - 1 * Time.deltaTime;
			}

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
		//---------------
		//==============================================================================================================

		modifiersCameraType();
		modifiersDrunkDriver ();
		
		//modifiersVortexMenu ();
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

	void modifiersDrunkDriver()
	{
		if (!fatMenuTransDown && !fatMenuTransUp && !panCameraDown && !showStartup && !initExitGame && !initLoadGame && !showCredits)
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
			else if (!showCredits && !showExtras && !showGameModeSelection && !isTurnedAround)
			{
				if (gameCameraBlur.enabled)
				{
					gameCameraBlur.enabled = false;
				}
				
				gameCamera.transform.position = cameraPos;
				if (settings.modifierCameraType == 0)
				{
					gameCamera.transform.eulerAngles = new Vector3(0, 270, 0);
				}
				else
				{
					gameCamera.transform.eulerAngles = new Vector3(0,gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
					
				}
			}
		}
		else if (creditsStep < 2 || creditsStep > 4)
		{
			if (settings.modifierDrunkDriver)
			{
				if (!gameCameraBlur.enabled)
				{
					gameCameraBlur.enabled = true;
				}
			}
			else
			{
				if (gameCameraBlur.enabled)
				{
					gameCameraBlur.enabled = false;
				}
			}
		}

	}

	void modifiersCameraType()
	{
		if (!showCredits)
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
			else if (!panCameraDown)
			{
				if (Mathf.FloorToInt(gameCamera.transform.eulerAngles.z) != 0)
				{
					/*gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y,(gameCamera.transform.eulerAngles.z + 1));
					if (Mathf.FloorToInt (gameCamera.transform.eulerAngles.z) < 1 || Mathf.FloorToInt(gameCamera.transform.eulerAngles.z) > 359)
					{
						gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y, 0);
					}*/
					gameCamera.transform.eulerAngles = new Vector3(gameCamera.transform.eulerAngles.x, gameCamera.transform.eulerAngles.y, 0);
				}
			}
		}
	}

	void spawnNewCube()
	{
        //1.2.0
		if (settings.modifierPowerups)
		{
			cubeMatPicker = Random.Range (1, 101); // have a 1% chance to spawn a power up
			if (cubeMatPicker == 1)
			{
				isPowerUpSpawning = true;
				cubeMatPicker = Random.Range (1, 5); //once the chance succeeds, randomly pick a powerup.
				if (cubeMatPicker == 1)
				{
					newCube = Instantiate (powerup_JumpREF);
				}
				else
				if (cubeMatPicker == 2)
				{
					newCube = Instantiate (powerup_ShootREF); 
				}
				else
				if (cubeMatPicker == 3)
				{
					newCube = Instantiate (powerup_SlowmoREF); 
				}
				else
				if (cubeMatPicker == 4)
				{
					newCube = Instantiate (powerup_BubbleREF); 
				}
				newCube.AddComponent<powerUpMenuScript> ();
			}
			else
			{
				isPowerUpSpawning = false;

				if (settings.modifierCubeType == 0)
				{
					cubeMatPicker = Random.Range (1, 4);
				}
				else
				if (settings.modifierCubeType == 1)
				{
					cubeMatPicker = Random.Range (4, 7);
				}
				else
				if (settings.modifierCubeType == 2)
				{
					cubeMatPicker = Random.Range (1, 7);
				}


				if (cubeMatPicker == 1)
				{
					newCube = Instantiate (RedCubeREF);
				}
				else
				if (cubeMatPicker == 2)
				{
					newCube = Instantiate (GreenCubeREF);
				}
				else
				if (cubeMatPicker == 3)
				{
					newCube = Instantiate (BlueCubeREF);
				}
				else
				if (cubeMatPicker == 4)
				{
					newCube = Instantiate (RedBallREF);
				}
				else
				if (cubeMatPicker == 5)
				{
					newCube = Instantiate (GreenBallREF);
				}
				else
				if (cubeMatPicker == 6)
				{
					newCube = Instantiate (BlueBallREF);
				}
			}
		}
		else
		{
			if (settings.modifierCubeType == 0)
			{
				cubeMatPicker = Random.Range (1, 4);
			}
			else
			if (settings.modifierCubeType == 1)
			{
				cubeMatPicker = Random.Range (4, 7);
			}
			else
			if (settings.modifierCubeType == 2)
			{
				cubeMatPicker = Random.Range (1, 7);
			}


			if (cubeMatPicker == 1)
			{
				newCube = Instantiate (RedCubeREF);
			}
			else
			if (cubeMatPicker == 2)
			{
				newCube = Instantiate (GreenCubeREF);
			}
			else
			if (cubeMatPicker == 3)
			{
				newCube = Instantiate (BlueCubeREF);
			}
			else
			if (cubeMatPicker == 4)
			{
				newCube = Instantiate (RedBallREF);
			}
			else
			if (cubeMatPicker == 5)
			{
				newCube = Instantiate (GreenBallREF);
			}
			else
			if (cubeMatPicker == 6)
			{
				newCube = Instantiate (BlueBallREF);
			}
		}

	        if (!settings.modifierPowerups || !isPowerUpSpawning)
	        {
			newCube.GetComponent<cubeMenuDeleteScript> ().isReferenceCube = false;
	        }

	        if (settings.modifierAirdrop && playerPos.z + (250 * settings.modifierSpeedMultiplier) < 4750 && playerPos.z + (250 * settings.modifierSpeedMultiplier) > -4750)
	        {
			newCube.transform.position = new Vector3(playerPos.x + ((Random.Range(cubeXMin, cubeXMax))), 350, (playerPos.z + (250 * settings.modifierSpeedMultiplier)));
	        }
	        else if (settings.modifierPowerups && isPowerUpSpawning)
	        {
			newCube.transform.position = new Vector3(playerPos.x + ((Random.Range(cubeXMin, cubeXMax))), 1, (playerPos.z + 250));
	        }
	        else
	        {
			newCube.transform.position = new Vector3(playerPos.x + ((Random.Range(cubeXMin, cubeXMax))), 1 / 2, (playerPos.z + 250));
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

		if (checkEnabled)
		{
			if (isPreviewingSong == true)
			{
				isPreviewingSong = false;
			}

			if (Application.loadedLevelName == "menu")
			{
				if (    !settings.optionsMenuBrokeAtTheCountEnabled &&
				    	!settings.optionsMenuBrokeBuildingTheSunEnabled &&
				    	!settings.optionsMenuBrokeBlownOutEnabled &&
				   	!settings.optionsMenuBrokeCalmTheFuckDownEnabled &&
				    	!settings.optionsMenuBrokeCaughtInTheBeatEnabled &&
				    	!settings.optionsMenuBrokeFuckItEnabled &&
				    	!settings.optionsMenuBrokeHellaEnabled &&
				    	!settings.optionsMenuBrokeHighSchoolSnapsEnabled &&
				    	!settings.optionsMenuBrokeLikeSwimmingEnabled &&
				    	!settings.optionsMenuBrokeLivingInReverseEnabled &&
				    	!settings.optionsMenuBrokeLuminousEnabled &&
				    	!settings.optionsMenuBrokeMellsParadeEnabled &&
				   	!settings.optionsMenuBrokeSomethingElatedEnabled &&
				    	!settings.optionsMenuBrokeTheGreatEnabled &&
					!settings.optionsMenuChrisAnotherVersionOfYouEnabled &&
					!settings.optionsMenuChrisDividerEnabled &&
					!settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled &&
					!settings.optionsMenuChrisThe49thStreetGalleriaEnabled &&
					!settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled &&
					!settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled &&
					!settings.optionsMenuDSMILEZInspirationEnabled &&
					!settings.optionsMenuDSMILEZLetYourBodyMoveEnabled &&
					!settings.optionsMenuDSMILEZLostInTheMusicEnabled &&
					!settings.optionsMenuDecktonicNightDriveEnabled &&
					!settings.optionsMenuDecktonicStarsEnabled &&
					!settings.optionsMenuDecktonicWatchYourDubstepEnabled &&
					!settings.optionsMenuDecktonicActIVEnabled &&
					!settings.optionsMenuDecktonicBassJamEnabled &&
					!settings.optionsMenuKaiEngelLowHorizonEnabled &&
					!settings.optionsMenuKaiEngelNothingEnabled &&
					!settings.optionsMenuKaiEngelSomethingEnabled &&
					!settings.optionsMenuKaiEngelWakeUpEnabled &&
					!settings.optionsMenuPierloBarbarianEnabled &&
					!settings.optionsMenuParijat4thNightEnabled &&
					!settings.optionsMenuRevolutionVoidHowExcitingEnabled &&
					!settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled &&
					!settings.optionsMenuSergeyLabyrinthEnabled &&
					!settings.optionsMenuSergeyNowYouAreHereEnabled &&
					!settings.optionsMenuToursEnthusiastEnabled &&
					!settings.optionsMenuADropBadInfluencesEnabled &&
					!settings.optionsMenuADropErrorEnabled &&
					!settings.optionsMenuADropFightOrDieEnabled &&
					!settings.optionsMenuArsFarewellTheInnocentEnabled &&
					!settings.optionsMenuArsSamaritanEnabled &&
					!settings.optionsMenuGravityLittleThingsEnabled &&
					!settings.optionsMenuGravityMicroscopeEnabled &&
					!settings.optionsMenuGravityOldHabitsEnabled &&
					!settings.optionsMenuGravityRadioactiveBoyEnabled &&
					!settings.optionsMenuGravityTrainTracksEnabled)
				{
					//Debug.Log ("No songs are enabled in " + Application.loadedLevelName);
					return;
				}
			}
		}
		else
		{
			if (isPreviewingSong == false)
			{
				isPreviewingSong = true;
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


	void initMenuCameraTransition() // Function for camera initial camera transition
	{
		//Debug.Log ("panning");
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

   	void cameraTurnAround()
    	{
    	}

    	void cameraTurnBack()
    	{
    	}

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

				gameCamera.transform.position = new Vector3 (playerPos.x + 2, 2, playerPos.z);

				gameCamera.transform.eulerAngles = new Vector3 (0, -90, 0);

				stars.GetComponent<ParticleSystem> ().startColor = Color.white;
				stars.GetComponent<ParticleSystem> ().maxParticles = 15000;
				stars.GetComponent<ParticleSystem> ().Clear (true);
				objLight.GetComponent<autoIntensity> ().dayRotateSpeed.x = 2;
				objLight.GetComponent<autoIntensity> ().nightRotateSpeed.x = 2;
				objLight.GetComponent<autoIntensity> ().dayAtmosphereThickness = 1;
				objLight.GetComponent<autoIntensity> ().nightAtmosphereThickness = 0.87f;
				objLight.GetComponent<Light> ().color = Color.white;
				objLight.transform.localEulerAngles = new Vector3 (25, 0, 0);

				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().enabled = false;
				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunColor = Color.white;

				gameCamera.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration> ().enabled = false;

				showCredits = false;
				creditsDoOnce = false;
				creditsStep = 5;
			}
		} else if (creditsStep == 5) {
			//Fade from black to menu

			if (gameCamera.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ().intensity == 0) {
				//currentSong = GameObject.Find ("creditsMusic");
				doCreditsTransition = false;
				creditsStep = 0;
			}
		}


	}
	void fatMenuTransitionUp()
	{


		gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x - Time.deltaTime*250, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);

		if (gameCamera.transform.eulerAngles.x < 325)
		{
			fatMenuTransUp = false;
		}
		
	}

	void fatMenuTransitionDown()
	{
		gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x + Time.deltaTime*250, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);

		if (gameCamera.transform.eulerAngles.x < 50)
		{
			fatMenuTransDown = false;
		}
		
	}

	void pickGameTransitionDown()
	{
		if (cameraPos.x > -10)
		{
			cameraPos = new Vector3(cameraPos.x - 50*Time.deltaTime, cameraPos.y, playerPos.z);
		}
		else
		{
			cameraPos = new Vector3(cameraPos.x, cameraPos.y, playerPos.z);
		}
	}

	void pickGameTransitionUp()
	{
		if (cameraPos.x < 4)
		{
			cameraPos = new Vector3(cameraPos.x + 50*Time.deltaTime, cameraPos.y, playerPos.z);
		}
		else
		{
			//cameraPos = new Vector3(5, cameraPos.y, cameraPos.z);
			pickGameTransUp = false;
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
		if (gameCamera.transform.eulerAngles.x > 300 || gameCameraScreenOverlay.intensity > -2 || (currentSong != null && currentSong.GetComponent<AudioSource> ().volume > 0)) {
			gameCamera.transform.eulerAngles = new Vector3 (gameCamera.transform.eulerAngles.x - Time.deltaTime * 15, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
			gameCameraScreenOverlay.intensity = gameCameraScreenOverlay.intensity - Time.deltaTime;
			if (currentSong != null) {
				currentSong.GetComponent<AudioSource> ().volume = currentSong.GetComponent<AudioSource> ().volume - Time.deltaTime / 4;
			}
		} else if (!settings.modifierProgressive) {
			Application.LoadLevel ("loading_game");
		} else if (settings.progressiveCurrentCheckpoint != 4) {
			Application.LoadLevel ("loading_progressive");
		} else {
			Application.LoadLevel ("game_progressive_finale");
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

			if ((settings.showSettingsResetScreen || leaderboards.showLeaderboardsError || showLeaderboardsResetAreYouSure) && !showStartup && !showFirstTimeMessage && !showNewVersion)
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

			if (showFirstTimeMessage && !showStartup) {
				if (Cursor.visible == false) {
					Cursor.visible = true;
				}
				
				GUI.Box (new Rect (Screen.width / 2 - 75, 50, 150, 40), "");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label (new Rect (Screen.width / 2 - 70, 45, 140, 40), "Welcome!");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				GUI.Box (new Rect (Screen.width / 2 - 150, 80, 300, 350), "");
				
				GUI.Label (new Rect (Screen.width / 2 - (280 / 2), 90, 280, 300), "Welcome to Cube Dodger!\n\nThank you for downloading Cube Dodger. I hope you enjoy the game as much as I enjoyed making it.\n\nFeel free to contact me at\n\nEthanDrakeCasey@gmail.com\n\nTHANK YOU!");
				if (GUI.Button (new Rect (Screen.width / 2 - (125 / 2), 400, 125, 40), "Continue")) {
					showFirstTimeMessage = false;
					showNewVersion = false;
					settings.isNewVerison = false;
					settings.saveSettings ();
				}
			} else if (showNewVersion && !showFirstTimeMessage && !showStartup) {
				if (Cursor.visible == false) {
					Cursor.visible = true;
				}

				GUI.Box (new Rect (Screen.width / 2 - 150, 50, 300, 40), "");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label (new Rect (Screen.width / 2 - 145, 45, 290, 40), "Welcome to v. " + gameVersion);
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				GUI.Box (new Rect (Screen.width / 2 - (470 / 2), 80, 470, 380), "");

				GUI.Label (new Rect (Screen.width / 2 - (450 / 2), 90, 450, 330), "Your game has updated to Cube Dodger version "+ gameVersion + "\n\nWhat's New?\n\n", CDGUIMenuSkin.FindStyle("changelogHeader"));
				GUI.Label (new Rect (Screen.width / 2 - (450 / 2), 170, 450, 330), changelogsString, CDGUIMenuSkin.FindStyle("changelogStyle"));

				if (GUI.Button (new Rect (Screen.width / 2 - (125 / 2), 430, 125, 40), "Continue")) {
					showFirstTimeMessage = false;
					showNewVersion = false;
					settings.isNewVerison = false;
					settings.saveSettings ();
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

			if (!showNewVersion && !showFirstTimeMessage && !leaderboards.showLeaderboardsError && settings.showSettingsResetScreen == false && !showLeaderboards && showCredits == false && showOptions == false && showModifiers == false && showStartup == false && initLoadGame == false && initExitGame == false && showExtras == false && showGameModeSelection == false && showAchievements == false) 
			{
				if (Cursor.visible == false)
				{
					Cursor.visible = true;
				}


				//GUI.color = Color.black;

				GUI.DrawTexture (new Rect (Screen.width - 380, 10, 400, 200), titleCardTexture);

				if (!isTurnedAround)
				{
					if (GUI.Button (new Rect (20, 40, 200, 60), new GUIContent("Start Game", "Choose your game mode."))) 
					{
						showGameModeSelection = true;
						if (panCameraDown)
						{
							panCameraDown = false;
							gameCameraDoF.focalLength = 10;
						}
						else
						{
							gameCamera.transform.eulerAngles = new Vector3(0, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
						}
						pickGameTransDown = true;
						//initLoadGame = true;
					}
					if (GUI.Button (new Rect (20, 120, 200, 60), new GUIContent("Modifiers", "Edit active game modifiers.")))
					{
						showModifiers = true;
					}
					if (GUI.Button (new Rect (20, 200, 200, 60), new GUIContent("Options", "Edit graphics and audio\nsettings.")))
					{
						showOptions = true;
						limboFullscreen = Screen.fullScreen;
					}
					if (GUI.Button (new Rect (20, 280, 200, 60), new GUIContent("Extras", "View leaderboards, \nachievements, or credits."))) 
					{
						showExtras = true;
						if (panCameraDown)
						{
							panCameraDown = false;
							gameCameraDoF.focalLength = 10;
						}
						else
						{
							gameCamera.transform.eulerAngles = new Vector3(0, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
						}
						fatMenuTransUp = true;
					}
					
					if (settings.ProgressiveFinaleBool)
					{
						if (GUI.Button (new Rect (20, 360, 200, 60), new GUIContent("THE BLACK HOLE", "..."))) 
						{
							isTurnedAround = true;
						}
					}
					
					if (backButtonPositionChangeThreshold == 0)
					{
						if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 205, 60), new GUIContent("Exit", "Return to the Desktop."))) 
						{
							initExitGame = true;
						}
						if (GUI.Button (new Rect(Screen.width - 225, Screen.height - 180, 205, 60), new GUIContent("Report A Bug", "Open an internet browser to\na bug report form.")))
						{
							Application.OpenURL("http://goo.gl/forms/LmNPvC1IogW9rmJr1");
						}
						if (Application.platform != RuntimePlatform.Android) {
							if (GUI.Button (new Rect(Screen.width - 225, Screen.height - 260, 205, 60), new GUIContent("Check For Updates", "Open an internet browser to\nthe game's download page.")))
							{
								Application.OpenURL("http://ethanthedrake.weebly.com/cubedodger-old.html");
							}
						}
					}
					else
					{
						if (GUI.Button (new Rect (Screen.width - 225, 380, 205, 60), new GUIContent("Exit", "Return to the Desktop."))) 
						{
							initExitGame = true;
						}
						if (GUI.Button (new Rect(Screen.width - 225, 300, 205, 60), new GUIContent("Report A Bug", "Open an internet browser to\na bug report form.")))
						{
							Application.OpenURL("http://goo.gl/forms/LmNPvC1IogW9rmJr1");
						}
						if (GUI.Button (new Rect(Screen.width - 225, 220, 205, 60), new GUIContent("Check For Updates", "Open an internet browser to\nthe game's download page.")))
						{
							Application.OpenURL("http://ethanthedrake.weebly.com/cubedodger-old.html");
						}
					}
				}
				else if (isTurnedAround)
				{
					if (GUI.Button (new Rect (20, 40, 200, 60), new GUIContent("Play Finale", "..."))) 
					{
						Debug.Log ("Not implemented yet!");
					}

					if (backButtonPositionChangeThreshold == 0)
					{
						if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 205, 60), new GUIContent("Turn Around", "..."))) 
						{
							isTurnedAround = false;
						}
					}
					else
					{
						if (GUI.Button (new Rect (Screen.width - 225, 380, 205, 60), new GUIContent("Turn Around", "..."))) 
						{
							isTurnedAround = false;
						}
					}
				}




			}
			else if (showModifiers == true)
			{
				GUI.Box(new Rect(Screen.width/2 - 80, 20, 160, 40),"");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label(new Rect(Screen.width/2 - 70, 15, 140, 40),"Modifiers");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				GUI.Box (new Rect ((Screen.width - 600)/2, 50, 600, 325), "");
				//GUI.color = Color.black;
				//GUI.skin = CDGUILargeTextSkin;
				//GUI.color = Color.black;
				//GUI.Button(new Rect(10, 10, 100, 20), new GUIContent("Click me", "This is the tooltip"));

				GUI.Label (new Rect((Screen.width - 600)/2 + 45, 65, 150, 50), new GUIContent("Difficulty:\n" + settings.difficultyString.ToString(), "The base difficulty level,\naffects cube spawn rate &\nbase speed. \nDefault: Normal, Score: x1"), GUI.skin.FindStyle("modifiersLabel15pt"));
				//GUI.Label (new Rect((Screen.width - 600)/2 + 45, 95, 150, 40), "The base difficulty level, affects cube spawn rate and speed.");
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 15, 60, 20, 50), "<"))
				{
					if (settings.difficulty > 0)
					{
						settings.difficulty = Mathf.Floor(settings.difficulty - 1);
						settings.checkSettings(true);
						leaderboards.pullLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);

						mobile_showHelp = 1;
					}
				}
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 205, 60, 20, 50), ">"))
				{
					if (settings.difficulty < 3)
					{
						settings.difficulty = Mathf.Floor(settings.difficulty + 1);
						settings.checkSettings(true);
						leaderboards.pullLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);

						mobile_showHelp = 1;
					}
				}
				GUI.Label (new Rect((Screen.width - 600)/2 + 45, 125, 150, 50), new GUIContent("Speed Mult.:\n" + settings.modifierSpeedMultiplier.ToString() + "x", "Multiply the base speed\nby this number.\n\nDefault: 1x, Score: x1"), GUI.skin.FindStyle("modifiersLabel15pt"));
				//GUI.Label (new Rect((Screen.width - 600)/2 + 45, 95, 150, 40), "The base difficulty level, affects cube spawn rate and speed.");
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 15, 120, 20, 50), "<"))
				{
					if (settings.modifierSpeedMultiplier > 0.5f)
					{
						settings.modifierSpeedMultiplier = settings.modifierSpeedMultiplier - .25f;

						mobile_showHelp = 2;
					}
				}
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 205, 120, 20, 50), ">"))
				{
					if (settings.modifierSpeedMultiplier < 2f)
					{
						settings.modifierSpeedMultiplier = settings.modifierSpeedMultiplier + .25f;

						mobile_showHelp = 2;
					}
				}
				GUI.skin = CDGUILargeTextSkin;
				GUI.color = Color.black;
				GUI.Label (new Rect((Screen.width - 600)/2 + 45, 300, 250, 50), new GUIContent("Score Multiplier: " + settings.scoreMultiplier + "x", "The amount of which your\nscore will be multiplied \ndepending on what modifiers \nare enabled."));
				GUI.color = Color.white;
				GUI.skin = CDGUIMenuSkin;

				//settings.modifierRandomizer = GUI.Toggle(new Rect((Screen.width - 600)/2 + 375, 65, 200, 30), settings.modifierRandomizer, new GUIContent("Randomizer Mode", "All modifiers randomized\nevery 1000 points starting\nat 500.\nDefault: Off, Score: x1"));
				if (Application.platform == RuntimePlatform.Android) {
					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 375, 65, 200, 30), "", CDGUIMenuSkin.FindStyle ("invisibleButton"))) {
						settings.modifierAirdrop = !settings.modifierAirdrop;

						mobile_showHelp = 3;
					}
					GUI.Toggle(new Rect((Screen.width - 600)/2 + 375, 65, 200, 30), settings.modifierAirdrop, new GUIContent("Airdrop Mode", "Cubes are dropped from\nthe sky.\n\nDefault: Off, Score: x2"));
				} else {
					settings.modifierAirdrop = GUI.Toggle(new Rect((Screen.width - 600)/2 + 375, 65, 200, 30), settings.modifierAirdrop, new GUIContent("Airdrop Mode", "Cubes are dropped from\nthe sky.\n\nDefault: Off, Score: x2"));
				}
				if (settings.modifierCubeType == 0)
				{
					if (settings.modifierCubeTypeString != "Cubes")
					{
						settings.modifierCubeTypeString = "Cubes";
					}
				}
				else if (settings.modifierCubeType == 1)
				{
					if (settings.modifierCubeTypeString != "Spheres")
					{
						settings.modifierCubeTypeString = "Spheres";
					}
				}
				else if (settings.modifierCubeType == 2)
				{
					if (settings.modifierCubeTypeString != "Mixed")
					{
						settings.modifierCubeTypeString = "Mixed";
					}
				}

				GUI.Label (new Rect((Screen.width - 600)/2 + 395, 90, 155, 50), new GUIContent("Cube Type: \n" + settings.modifierCubeTypeString, "Choose what you dodge.\nCubes, Spheres, or both! \nDefault: Cubes, \nScore: x1, x1.5, x2"), CDGUIMenuSkin.FindStyle("modifiersLabel15pt"));
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 375, 95, 20, 40), "<"))
				{
					if (settings.modifierCubeType > 0)
					{
						settings.modifierCubeType = settings.modifierCubeType - 1;

						mobile_showHelp = 4;
					}
				}
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 550, 95, 20, 40), ">"))
				{
					if (settings.modifierCubeType < 2)
					{
						settings.modifierCubeType = settings.modifierCubeType + 1;

						mobile_showHelp = 4;
					}
				}

				if (settings.modifierCameraType == 0)
				{
					if (settings.modifierCameraTypeString != "Static")
					{
						settings.modifierCameraTypeString = "Static";
					}
				}
				else if (settings.modifierCameraType == 1)
				{
					if (settings.modifierCameraTypeString != "Upside Down")
					{
						settings.modifierCameraTypeString = "Upside Down";
					}
				}
				else if (settings.modifierCameraType == 2)
				{
					if (settings.modifierCameraTypeString != "Spinning")
					{
						settings.modifierCameraTypeString = "Spinning";
					}
				}

				GUI.Label (new Rect((Screen.width - 600)/2 + 395, 140, 155, 50), new GUIContent("Camera Mode: \n" + settings.modifierCameraTypeString, "Choose how the camera\nbehaves.\nDefault: Static\nScore: x1, x1.5, x2"), CDGUIMenuSkin.FindStyle("modifiersLabel15pt"));
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 375, 145, 20, 40), "<"))
				{
					if (settings.modifierCameraType > 0)
					{
						settings.modifierCameraType = settings.modifierCameraType - 1;
						mobile_showHelp = 5;
					}
				}
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 550, 145, 20, 40), ">"))
				{
					if (settings.modifierCameraType < 2)
					{
						settings.modifierCameraType = settings.modifierCameraType + 1;
						mobile_showHelp = 5;
					}
				}

				if (Application.platform == RuntimePlatform.Android) {
					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 375, 200, 200, 30), "", CDGUIMenuSkin.FindStyle ("invisibleButton"))) {
						settings.modifierDrunkDriver = !settings.modifierDrunkDriver;

						mobile_showHelp = 6;
					}
					GUI.Toggle(new Rect((Screen.width - 600)/2 + 375, 200, 200, 30), settings.modifierDrunkDriver, new GUIContent("Drunk Driver Mode", "Motion Blur is enabled and\nthe camera will sway.\n\nDefault: Off, Score: x2"));
				} else {
					settings.modifierDrunkDriver = GUI.Toggle(new Rect((Screen.width - 600)/2 + 375, 200, 200, 30), settings.modifierDrunkDriver, new GUIContent("Drunk Driver Mode", "Motion Blur is enabled and\nthe camera will sway.\n\nDefault: Off, Score: x2"));
				}
				if (Application.platform == RuntimePlatform.Android) {
					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 375, 230, 200, 30), "", CDGUIMenuSkin.FindStyle ("invisibleButton"))) {
						settings.modifierPowerups = !settings.modifierPowerups;

						mobile_showHelp = 7;
					}
					settings.modifierPowerups = GUI.Toggle(new Rect((Screen.width - 600) / 2 + 375, 230, 200, 30), settings.modifierPowerups, new GUIContent("Power Ups", "Game changing Power Ups\nhave a random chance to\nspawn instead of a cube.\nDefault: Off, Score: x1"));
				} else {
					settings.modifierPowerups = GUI.Toggle(new Rect((Screen.width - 600) / 2 + 375, 230, 200, 30), settings.modifierPowerups, new GUIContent("Power Ups", "Game changing Power Ups\nhave a random chance to\nspawn instead of a cube.\nDefault: Off, Score: x1"));
				}
				if (Application.platform == RuntimePlatform.Android) {
					if (mobile_showHelp == 1) {
						GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "DIFFICULTY:\nThe base difficulty level, affects cube spawn rate & base speed.\nDefault: Normal \nScore: x1", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
					} else if (mobile_showHelp == 2) {
						GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "SPEED MULT.:\nMultiply the base speed by this number.\n\nDefault: 1x\nScore: x1", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
					} else if (mobile_showHelp == 3) {
						GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "AIRDROP:\nCubes are dropped from the sky.\n\nDefault: Off\nScore: x2", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
					} else if (mobile_showHelp == 4) {
						GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "CUBE TYPE:\nChoose what you dodge! Cubes, Spheres, or Both!\n\nDefault: Cubes\nScore: x1, x1.5, x2", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
					} else if (mobile_showHelp == 5) {
						GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "CAMERA MODE:\nChoose how the camera behaves.\n\nDefault: Static\nScore: x1, x1.5, x2", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
					} else if (mobile_showHelp == 6) {
						GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "DRUNK DRIVER:\nMotion Blur is enabled and the camera will sway.\nDefault: Off\nScore: x2", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
					} else if (mobile_showHelp == 7) {
						GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "POWER UPS:\nGame changing power ups have a chance to spawn instead of a cube.\nDefault: Off\nScore: x1", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
					}
				}

				//GUI.color = Color.white;
				if (backButtonPositionChangeThreshold == 0)
				{
					if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 150, 200, 60), "Back")) 
					{
						settings.checkSettings();
						settings.saveSettings();
						showModifiers = false;
						mobile_showHelp = 0;
					}
				}
				else
				{
					if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) 
					{
						settings.checkSettings();
						settings.saveSettings();
						showModifiers = false;
						mobile_showHelp = 0;
					}
				}

			}
			else if (showOptions == true)
			{
				if (limboWidthResolution == 0)
				{
					limboWidthResolution = Screen.width;
				}
				if (limboHeightResolution == 0)
				{
					limboHeightResolution = Screen.height;
				}

				if (showResolutionDropdown == false && showPresetQualityDropdown == false)
				{
					if (backButtonPositionChangeThreshold == 0)
					{
						if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) 
						{
							
							settings.saveSettings();
							showOptions = false;
							mobile_showHelp = 0;
							if (isPreviewingSong == true)
							{
								isPreviewingSong = false;
								currentSong.GetComponent<AudioSource>().Stop ();
							}
						}
					}
					else
					{
						if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) 
						{
							settings.saveSettings();
							showOptions = false;
							mobile_showHelp = 0;
							if (isPreviewingSong == true)
							{
								isPreviewingSong = false;
								currentSong.GetComponent<AudioSource>().Stop ();
							}
						}
					}

				}
				GUI.Box(new Rect((Screen.width - 600)/2 + 220, 10, 160, 50),"");
				GUI.skin = CDGUILargeTextSkin;
				GUI.color = Color.black;
				GUI.Label(new Rect((Screen.width - 600)/2 + 240, 10, 120, 40),"Options");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;

				if (showGraphicsOptions) {
					if (Application.platform == RuntimePlatform.Android || settings.simulateMobileOptimization) {
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 20, 20, 150, 40), "Controls", CDGUIMenuSkin.FindStyle ("menuTabActiveButton"))) {
							showGraphicsOptions = true;
							showAudioOptions = false;
						}
					} else {
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 20, 20, 150, 40), "Graphics", CDGUIMenuSkin.FindStyle ("menuTabActiveButton"))) {
							showGraphicsOptions = true;
							showAudioOptions = false;
						}
					}

					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 430, 20, 150, 40), "Audio")) {
						showAudioOptions = true;
						showGraphicsOptions = false;
					}
				} else if (showAudioOptions) {
					if (Application.platform == RuntimePlatform.Android || settings.simulateMobileOptimization) {
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 20, 20, 150, 40), "Controls")) {
							showGraphicsOptions = true;
							showAudioOptions = false;
						}
					} else {
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 20, 20, 150, 40), "Graphics")) {
							showGraphicsOptions = true;
							showAudioOptions = false;
						}
					}
					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 430, 20, 150, 40), "Audio", CDGUIMenuSkin.FindStyle ("menuTabActiveButton"))) {
						showAudioOptions = true;
						showGraphicsOptions = false;
					}
				}

				GUI.Box (new Rect ((Screen.width - 600)/2, 50, 600, 325), "");


				if (showGraphicsOptions && Application.platform != RuntimePlatform.Android && !settings.simulateMobileOptimization) {

					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 65, 245, 40), "Preset: " + settings.presetQualityString)) {
						if (showPresetQualityDropdown) {
							showPresetQualityDropdown = false;
						} else {
							showResolutionDropdown = false;
							showPresetQualityDropdown = true;
						}
					}
					
					if (showPresetQualityDropdown) {
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 105, 245, 40), "Custom")) {
							settings.updateGraphicsPreset (5);
							showPresetQualityDropdown = false;
						}
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 145, 245, 40), "Performance")) {
							settings.updateGraphicsPreset (0);
							showPresetQualityDropdown = false;
						}
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 185, 245, 40), "Low")) {
							settings.updateGraphicsPreset (1);
							showPresetQualityDropdown = false;
						}
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 225, 245, 40), "Medium")) {
							settings.updateGraphicsPreset (2);
							showPresetQualityDropdown = false;
						}
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 265, 245, 40), "High")) {
							settings.updateGraphicsPreset (3);
							showPresetQualityDropdown = false;
						}
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 305, 245, 40), "Ultra")) {
							settings.updateGraphicsPreset (4);
							showPresetQualityDropdown = false;
						}
						
						if (backButtonPositionChangeThreshold == 0) {
							if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) {
								showPresetQualityDropdown = false;
							}
						} else {
							if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) {
								showPresetQualityDropdown = false;
							}
						}
						
					} else {
						GUI.skin = CDGUILargeTextSkin;
						GUI.color = Color.black;
						GUI.Label (new Rect ((Screen.width - 600) / 2 + 350, 125, 200, 50), "Anti-Aliasing: \n" + settings.optionsAntiAliasing + "x"); 
						GUI.Label (new Rect ((Screen.width - 600) / 2 + 350, 200, 200, 50), "Shadow Distance: \n" + settings.optionsShadowDistance);
						GUI.Label (new Rect ((Screen.width - 600) / 2 + 350, 275, 200, 50), "Depth of Field: \n" + settings.optionsDepthOfField);
						GUI.skin = CDGUIMenuSkin;
						GUI.color = Color.white;
						if (settings.optionsPresetQuality == 5) {
							if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 125, 30, 50), "<")) {
								if (settings.optionsAntiAliasing > 0) {
									QualitySettings.antiAliasing = QualitySettings.antiAliasing - 2;
								}
							}
							if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 550, 125, 30, 50), ">")) {
								if (settings.optionsAntiAliasing < 8) {
									QualitySettings.antiAliasing = QualitySettings.antiAliasing + 2;
								}
							}
							if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 200, 30, 50), "<")) {
								if (settings.optionsShadowDistance > 0) {
									QualitySettings.shadowDistance = QualitySettings.shadowDistance - 5;
								}
							}
							if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 550, 200, 30, 50), ">")) {
								if (settings.optionsShadowDistance < 400) {
									QualitySettings.shadowDistance = QualitySettings.shadowDistance + 5;
								}
							}
							if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 320, 275, 30, 50), "<")) {
								if (settings.optionsDepthOfField == true) {
									settings.optionsDepthOfField = false;
								}
							}
							if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 550, 275, 30, 50), ">")) {
								if (settings.optionsDepthOfField == false) {
									settings.optionsDepthOfField = true;
								}
							}
							//gameCameraDoF.enabled = GUI.Toggle(new Rect ((Screen.width - 600)/2 + 350, 275, 200, 50), gameCameraDoF.enabled, "Depth of Field");
						} else {
							
						}
						
						
					}

					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 35, 65, 245, 40), "Resolution: " + limboWidthResolution + " x " + limboHeightResolution)) {
						if (showResolutionDropdown) {
							showResolutionDropdown = false;
						} else {
							showPresetQualityDropdown = false;
							showResolutionDropdown = true;
						}
					}
					
					if (showResolutionDropdown) {
						
						dropDownCounter = 0;
						
						resolutionDropdownScrollPosition = GUI.BeginScrollView (new Rect ((Screen.width - 600) / 2 + 35, 105, 245, Screen.height / 2), resolutionDropdownScrollPosition, new Rect (0, 0, 10, totalResolutionListHeight));
						foreach (Resolution res in resolutions) {
							if (GUI.Button (new Rect (0, dropDownCounter, 245, 40), res.width + " x " + res.height)) {
								limboWidthResolution = res.width;
								limboHeightResolution = res.height;
								//Screen.SetResolution(res.width,res.height, settings.optionsFullscreen);
								showResolutionDropdown = false;
							}
							if (getResolutionDropdownHeightOnce == false) {
								totalResolutionListHeight = totalResolutionListHeight + 40;
							}
							//print (res);
							dropDownCounter = dropDownCounter + 40;
						}
						GUI.EndScrollView ();
						getResolutionDropdownHeightOnce = true;
						if (backButtonPositionChangeThreshold == 0) {
							if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) {
								showResolutionDropdown = false;
							}
						} else {
							if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) {
								showResolutionDropdown = false;
							}
						}
						
					} else {
						limboFullscreen = GUI.Toggle (new Rect ((Screen.width - 600) / 2 + 35, 110, 150, 40), limboFullscreen, "Fullscreen");
						
						if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 35, 325, 200, 40), "Apply Resolution")) {
							Screen.SetResolution (limboWidthResolution, limboHeightResolution, limboFullscreen);
							settings.checkGraphics ();
							//limboWidthResolution = 0;
							//limboHeightResolution = 0;
						}
						
					}
				} else if (showGraphicsOptions && (Application.platform == RuntimePlatform.Android || settings.simulateMobileOptimization)) {
					GUI.skin = CDGUILargeTextSkin;
					GUI.color = Color.black;
					GUI.Label (new Rect ((Screen.width - 600) / 2 + 225, 75, 150, 25), "Controls: ");
					if (settings.mobileControlScheme == 0) {
						GUI.Label (new Rect ((Screen.width - 600) / 2 + 170, 100, 250, 25), "Accelerometer");
					} else if (settings.mobileControlScheme == 1) {
						GUI.Label (new Rect ((Screen.width - 600) / 2 + 170, 100, 250, 25), "Buttons");
					}

					GUI.skin = CDGUIMenuSkin;
					GUI.color = Color.white;

					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 50, 150, 200, 200), "Accelerometer", CDGUIMenuSkin.FindStyle ("controlSchemeButton0"))) {
						settings.mobileControlScheme = 0;
					}
					if (GUI.Button (new Rect ((Screen.width - 600) / 2 + 350, 150, 200, 200), "Buttons", CDGUIMenuSkin.FindStyle ("controlSchemeButton1"))) {
						settings.mobileControlScheme = 1;
					}

				}
				else if (showAudioOptions)
				{
					GUI.skin = CDGUILargeTextSkin;
					GUI.color = Color.black;
					GUI.Label (new Rect((Screen.width - 600)/2 + 35, 50, 290, 60), "Track Name");
					GUI.Label (new Rect((Screen.width - 600)/2 + 400, 50, 100, 60), "Menu");
					GUI.Label (new Rect((Screen.width - 600)/2 + 500, 50, 100, 60), "Game");
					GUI.skin = CDGUIMenuSkin;
					GUI.color = Color.white;

					if (GUI.Button (new Rect((Screen.width - 600)/2 + 290, 385, 100, 40), "Default"))
					{
						settings.optionsMenuBrokeAtTheCountEnabled = false;
						settings.optionsMenuBrokeBuildingTheSunEnabled = false;
						settings.optionsMenuBrokeBlownOutEnabled = false;
						settings.optionsMenuBrokeCalmTheFuckDownEnabled = false;
						settings.optionsMenuBrokeCaughtInTheBeatEnabled = false;
						settings.optionsMenuBrokeFuckItEnabled = false;
						settings.optionsMenuBrokeHellaEnabled = false;
						settings.optionsMenuBrokeHighSchoolSnapsEnabled = false;
						settings.optionsMenuBrokeLikeSwimmingEnabled = false;
						settings.optionsMenuBrokeLivingInReverseEnabled = false;
						settings.optionsMenuBrokeLuminousEnabled = false;
						settings.optionsMenuBrokeMellsParadeEnabled = true;
						settings.optionsMenuBrokeSomethingElatedEnabled = false;
						settings.optionsMenuBrokeTheGreatEnabled = false;
						settings.optionsMenuBrokeQuitBitchingEnabled = false;
						settings.optionsMenuChrisAnotherVersionOfYouEnabled = true;
						settings.optionsMenuChrisDividerEnabled = true;
						settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled = true;
						settings.optionsMenuChrisThe49thStreetGalleriaEnabled = true;
						settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = true;
						settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled = true;
						settings.optionsMenuDSMILEZInspirationEnabled = true;
						settings.optionsMenuDSMILEZLetYourBodyMoveEnabled = false;
						settings.optionsMenuDSMILEZLostInTheMusicEnabled = false;
						settings.optionsMenuDecktonicNightDriveEnabled = false;
						settings.optionsMenuDecktonicStarsEnabled = false;
						settings.optionsMenuDecktonicWatchYourDubstepEnabled = false;
						settings.optionsMenuDecktonicActIVEnabled = false;
						settings.optionsMenuDecktonicBassJamEnabled = false;
						settings.optionsMenuKaiEngelLowHorizonEnabled = true;
						settings.optionsMenuKaiEngelNothingEnabled = true;
						settings.optionsMenuKaiEngelSomethingEnabled = true;
						settings.optionsMenuKaiEngelWakeUpEnabled = true;
						settings.optionsMenuPierloBarbarianEnabled = false;
						settings.optionsMenuParijat4thNightEnabled = true;
						settings.optionsMenuRevolutionVoidHowExcitingEnabled = false;
						settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled = false;
						settings.optionsMenuSergeyLabyrinthEnabled = true;
						settings.optionsMenuSergeyNowYouAreHereEnabled = true;
						settings.optionsMenuToursEnthusiastEnabled = true;
						settings.optionsGameBrokeAtTheCountEnabled = true;
						settings.optionsGameBrokeBuildingTheSunEnabled = true;
						settings.optionsGameBrokeBlownOutEnabled = true;
						settings.optionsGameBrokeCalmTheFuckDownEnabled = true;
						settings.optionsGameBrokeCaughtInTheBeatEnabled = true;
						settings.optionsGameBrokeFuckItEnabled = true;
						settings.optionsGameBrokeHellaEnabled = true;
						settings.optionsGameBrokeHighSchoolSnapsEnabled = true;
						settings.optionsGameBrokeLikeSwimmingEnabled = true;
						settings.optionsGameBrokeLivingInReverseEnabled = true;
						settings.optionsGameBrokeLuminousEnabled = true;
						settings.optionsGameBrokeMellsParadeEnabled = true;
						settings.optionsGameBrokeSomethingElatedEnabled = true;
						settings.optionsGameBrokeTheGreatEnabled = true;
						settings.optionsGameBrokeQuitBitchingEnabled = true;
						settings.optionsGameChrisAnotherVersionOfYouEnabled = false;
						settings.optionsGameChrisDividerEnabled = false;
						settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled = false;
						settings.optionsGameChrisThe49thStreetGalleriaEnabled = false;
						settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = true;
						settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled = false;
						settings.optionsGameDecktonicNightDriveEnabled = true;
						settings.optionsGameDecktonicStarsEnabled = true;
						settings.optionsGameDecktonicWatchYourDubstepEnabled = true;
						settings.optionsGameDecktonicActIVEnabled = true;
						settings.optionsGameDecktonicBassJamEnabled = true;
						settings.optionsGameDSMILEZInspirationEnabled = true;
						settings.optionsGameDSMILEZLetYourBodyMoveEnabled = true;
						settings.optionsGameDSMILEZLostInTheMusicEnabled = true;
						settings.optionsGameKaiEngelLowHorizonEnabled = false;
						settings.optionsGameKaiEngelNothingEnabled = false;
						settings.optionsGameKaiEngelSomethingEnabled = false;
						settings.optionsGameKaiEngelWakeUpEnabled = false;
						settings.optionsGamePierloBarbarianEnabled = true;
						settings.optionsGameParijat4thNightEnabled = false;
						settings.optionsGameRevolutionVoidHowExcitingEnabled = true;
						settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled = true;
						settings.optionsGameSergeyLabyrinthEnabled = false;
						settings.optionsGameSergeyNowYouAreHereEnabled = false;
						settings.optionsGameToursEnthusiastEnabled = true;

						settings.optionsMenuADropBadInfluencesEnabled = false;
						settings.optionsMenuADropErrorEnabled = false;
						settings.optionsMenuADropFightOrDieEnabled = false;
						settings.optionsMenuArsFarewellTheInnocentEnabled = true;
						settings.optionsMenuArsSamaritanEnabled = true;
						settings.optionsMenuGravityLittleThingsEnabled = true;
						settings.optionsMenuGravityMicroscopeEnabled = true;
						settings.optionsMenuGravityOldHabitsEnabled = true;
						settings.optionsMenuGravityRadioactiveBoyEnabled = true;
						settings.optionsMenuGravityTrainTracksEnabled = true;
						settings.optionsGameADropBadInfluencesEnabled = true;
						settings.optionsGameADropErrorEnabled = true;
						settings.optionsGameADropFightOrDieEnabled = true;
						settings.optionsGameArsFarewellTheInnocentEnabled = false;
						settings.optionsGameArsSamaritanEnabled = true;
						settings.optionsGameGravityLittleThingsEnabled = true;
						settings.optionsGameGravityMicroscopeEnabled = true;
						settings.optionsGameGravityOldHabitsEnabled = true;
						settings.optionsGameGravityRadioactiveBoyEnabled = true;
						settings.optionsGameGravityTrainTracksEnabled = true;
					}
					if (GUI.Button (new Rect((Screen.width - 600)/2 + 395, 385, 100, 40), "All"))
					{
						settings.optionsMenuBrokeAtTheCountEnabled = true;
						settings.optionsMenuBrokeBuildingTheSunEnabled = true;
						settings.optionsMenuBrokeBlownOutEnabled = true;
						settings.optionsMenuBrokeCalmTheFuckDownEnabled = true;
						settings.optionsMenuBrokeCaughtInTheBeatEnabled = true;
						settings.optionsMenuBrokeFuckItEnabled = true;
						settings.optionsMenuBrokeHellaEnabled = true;
						settings.optionsMenuBrokeHighSchoolSnapsEnabled = true;
						settings.optionsMenuBrokeLikeSwimmingEnabled = true;
						settings.optionsMenuBrokeLivingInReverseEnabled = true;
						settings.optionsMenuBrokeLuminousEnabled = true;
						settings.optionsMenuBrokeMellsParadeEnabled = true;
						settings.optionsMenuBrokeSomethingElatedEnabled = true;
						settings.optionsMenuBrokeTheGreatEnabled = true;
						settings.optionsMenuBrokeQuitBitchingEnabled = true;
						settings.optionsMenuChrisAnotherVersionOfYouEnabled = true;
						settings.optionsMenuChrisDividerEnabled = true;
						settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled = true;
						settings.optionsMenuChrisThe49thStreetGalleriaEnabled = true;
						settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = true;
						settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled = true;
						settings.optionsMenuDSMILEZInspirationEnabled = true;
						settings.optionsMenuDSMILEZLetYourBodyMoveEnabled = true;
						settings.optionsMenuDSMILEZLostInTheMusicEnabled = true;
						settings.optionsMenuDecktonicNightDriveEnabled = true;
						settings.optionsMenuDecktonicStarsEnabled = true;
						settings.optionsMenuDecktonicWatchYourDubstepEnabled = true;
						settings.optionsMenuDecktonicActIVEnabled = true;
						settings.optionsMenuDecktonicBassJamEnabled = true;
						settings.optionsMenuKaiEngelLowHorizonEnabled = true;
						settings.optionsMenuKaiEngelNothingEnabled = true;
						settings.optionsMenuKaiEngelSomethingEnabled = true;
						settings.optionsMenuKaiEngelWakeUpEnabled = true;
						settings.optionsMenuPierloBarbarianEnabled = true;
						settings.optionsMenuParijat4thNightEnabled = true;
						settings.optionsMenuRevolutionVoidHowExcitingEnabled = true;
						settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled = true;
						settings.optionsMenuSergeyLabyrinthEnabled = true;
						settings.optionsMenuSergeyNowYouAreHereEnabled = true;
						settings.optionsMenuToursEnthusiastEnabled = true;
						settings.optionsGameBrokeAtTheCountEnabled = true;
						settings.optionsGameBrokeBuildingTheSunEnabled = true;
						settings.optionsGameBrokeBlownOutEnabled = true;
						settings.optionsGameBrokeCalmTheFuckDownEnabled = true;
						settings.optionsGameBrokeCaughtInTheBeatEnabled = true;
						settings.optionsGameBrokeFuckItEnabled = true;
						settings.optionsGameBrokeHellaEnabled = true;
						settings.optionsGameBrokeHighSchoolSnapsEnabled = true;
						settings.optionsGameBrokeLikeSwimmingEnabled = true;
						settings.optionsGameBrokeLivingInReverseEnabled = true;
						settings.optionsGameBrokeLuminousEnabled = true;
						settings.optionsGameBrokeMellsParadeEnabled = true;
						settings.optionsGameBrokeSomethingElatedEnabled = true;
						settings.optionsGameBrokeTheGreatEnabled = true;
						settings.optionsGameBrokeQuitBitchingEnabled = true;
						settings.optionsGameChrisAnotherVersionOfYouEnabled = true;
						settings.optionsGameChrisDividerEnabled = true;
						settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled = true;
						settings.optionsGameChrisThe49thStreetGalleriaEnabled = true;
						settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = true;
						settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled = true;
						settings.optionsGameDecktonicNightDriveEnabled = true;
						settings.optionsGameDecktonicStarsEnabled = true;
						settings.optionsGameDecktonicWatchYourDubstepEnabled = true;
						settings.optionsGameDecktonicActIVEnabled = true;
						settings.optionsGameDecktonicBassJamEnabled = true;
						settings.optionsGameDSMILEZInspirationEnabled = true;
						settings.optionsGameDSMILEZLetYourBodyMoveEnabled = true;
						settings.optionsGameDSMILEZLostInTheMusicEnabled = true;
						settings.optionsGameKaiEngelLowHorizonEnabled = true;
						settings.optionsGameKaiEngelNothingEnabled = true;
						settings.optionsGameKaiEngelSomethingEnabled = true;
						settings.optionsGameKaiEngelWakeUpEnabled = true;
						settings.optionsGamePierloBarbarianEnabled = true;
						settings.optionsGameParijat4thNightEnabled = true;
						settings.optionsGameRevolutionVoidHowExcitingEnabled = true;
						settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled = true;
						settings.optionsGameSergeyLabyrinthEnabled = true;
						settings.optionsGameSergeyNowYouAreHereEnabled = true;
						settings.optionsGameToursEnthusiastEnabled = true;

						settings.optionsMenuADropBadInfluencesEnabled = true;
						settings.optionsMenuADropErrorEnabled = true;
						settings.optionsMenuADropFightOrDieEnabled = true;
						settings.optionsMenuArsFarewellTheInnocentEnabled = true;
						settings.optionsMenuArsSamaritanEnabled = true;
						settings.optionsMenuGravityLittleThingsEnabled = true;
						settings.optionsMenuGravityMicroscopeEnabled = true;
						settings.optionsMenuGravityOldHabitsEnabled = true;
						settings.optionsMenuGravityRadioactiveBoyEnabled = true;
						settings.optionsMenuGravityTrainTracksEnabled = true;
						settings.optionsGameADropBadInfluencesEnabled = true;
						settings.optionsGameADropErrorEnabled = true;
						settings.optionsGameADropFightOrDieEnabled = true;
						settings.optionsGameArsFarewellTheInnocentEnabled = true;
						settings.optionsGameArsSamaritanEnabled = true;
						settings.optionsGameGravityLittleThingsEnabled = true;
						settings.optionsGameGravityMicroscopeEnabled = true;
						settings.optionsGameGravityOldHabitsEnabled = true;
						settings.optionsGameGravityRadioactiveBoyEnabled = true;
						settings.optionsGameGravityTrainTracksEnabled = true;
					}
					if (GUI.Button (new Rect((Screen.width - 600)/2 + 500, 385, 100, 40), "None"))
					{
						settings.optionsMenuBrokeAtTheCountEnabled = false;
						settings.optionsMenuBrokeBuildingTheSunEnabled = false;
						settings.optionsMenuBrokeBlownOutEnabled = false;
						settings.optionsMenuBrokeCalmTheFuckDownEnabled = false;
						settings.optionsMenuBrokeCaughtInTheBeatEnabled = false;
						settings.optionsMenuBrokeFuckItEnabled = false;
						settings.optionsMenuBrokeHellaEnabled = false;
						settings.optionsMenuBrokeHighSchoolSnapsEnabled = false;
						settings.optionsMenuBrokeLikeSwimmingEnabled = false;
						settings.optionsMenuBrokeLivingInReverseEnabled = false;
						settings.optionsMenuBrokeLuminousEnabled = false;
						settings.optionsMenuBrokeMellsParadeEnabled = false;
						settings.optionsMenuBrokeSomethingElatedEnabled = false;
						settings.optionsMenuBrokeTheGreatEnabled = false;
						settings.optionsMenuBrokeQuitBitchingEnabled = false;
						settings.optionsMenuChrisAnotherVersionOfYouEnabled = false;
						settings.optionsMenuChrisDividerEnabled = false;
						settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled = false;
						settings.optionsMenuChrisThe49thStreetGalleriaEnabled = false;
						settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = false;
						settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled = false;
						settings.optionsMenuDSMILEZInspirationEnabled = false;
						settings.optionsMenuDSMILEZLetYourBodyMoveEnabled = false;
						settings.optionsMenuDSMILEZLostInTheMusicEnabled = false;
						settings.optionsMenuDecktonicNightDriveEnabled = false;
						settings.optionsMenuDecktonicStarsEnabled = false;
						settings.optionsMenuDecktonicWatchYourDubstepEnabled = false;
						settings.optionsMenuDecktonicActIVEnabled = false;
						settings.optionsMenuDecktonicBassJamEnabled = false;
						settings.optionsMenuKaiEngelLowHorizonEnabled = false;
						settings.optionsMenuKaiEngelNothingEnabled = false;
						settings.optionsMenuKaiEngelSomethingEnabled = false;
						settings.optionsMenuKaiEngelWakeUpEnabled = false;
						settings.optionsMenuPierloBarbarianEnabled = false;
						settings.optionsMenuParijat4thNightEnabled = false;
						settings.optionsMenuRevolutionVoidHowExcitingEnabled = false;
						settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled = false;
						settings.optionsMenuSergeyLabyrinthEnabled = false;
						settings.optionsMenuSergeyNowYouAreHereEnabled = false;
						settings.optionsMenuToursEnthusiastEnabled = false;
						settings.optionsGameBrokeAtTheCountEnabled = false;
						settings.optionsGameBrokeBuildingTheSunEnabled = false;
						settings.optionsGameBrokeBlownOutEnabled = false;
						settings.optionsGameBrokeCalmTheFuckDownEnabled = false;
						settings.optionsGameBrokeCaughtInTheBeatEnabled = false;
						settings.optionsGameBrokeFuckItEnabled = false;
						settings.optionsGameBrokeHellaEnabled = false;
						settings.optionsGameBrokeHighSchoolSnapsEnabled = false;
						settings.optionsGameBrokeLikeSwimmingEnabled = false;
						settings.optionsGameBrokeLivingInReverseEnabled = false;
						settings.optionsGameBrokeLuminousEnabled = false;
						settings.optionsGameBrokeMellsParadeEnabled = false;
						settings.optionsGameBrokeSomethingElatedEnabled = false;
						settings.optionsGameBrokeTheGreatEnabled = false;
						settings.optionsGameBrokeQuitBitchingEnabled = false;
						settings.optionsGameChrisAnotherVersionOfYouEnabled = false;
						settings.optionsGameChrisDividerEnabled = false;
						settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled = false;
						settings.optionsGameChrisThe49thStreetGalleriaEnabled = false;
						settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = false;
						settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled = false;
						settings.optionsGameDecktonicNightDriveEnabled = false;
						settings.optionsGameDecktonicStarsEnabled = false;
						settings.optionsGameDecktonicWatchYourDubstepEnabled = false;
						settings.optionsGameDecktonicActIVEnabled = false;
						settings.optionsGameDecktonicBassJamEnabled = false;
						settings.optionsGameDSMILEZInspirationEnabled = false;
						settings.optionsGameDSMILEZLetYourBodyMoveEnabled = false;
						settings.optionsGameDSMILEZLostInTheMusicEnabled = false;
						settings.optionsGameKaiEngelLowHorizonEnabled = false;
						settings.optionsGameKaiEngelNothingEnabled = false;
						settings.optionsGameKaiEngelSomethingEnabled = false;
						settings.optionsGameKaiEngelWakeUpEnabled = false;
						settings.optionsGamePierloBarbarianEnabled = false;
						settings.optionsGameParijat4thNightEnabled = false;
						settings.optionsGameRevolutionVoidHowExcitingEnabled = false;
						settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled = false;
						settings.optionsGameSergeyLabyrinthEnabled = false;
						settings.optionsGameSergeyNowYouAreHereEnabled = false;
						settings.optionsGameToursEnthusiastEnabled = false;

						settings.optionsMenuADropBadInfluencesEnabled = false;
						settings.optionsMenuADropErrorEnabled = false;
						settings.optionsMenuADropFightOrDieEnabled = false;
						settings.optionsMenuArsFarewellTheInnocentEnabled = false;
						settings.optionsMenuArsSamaritanEnabled = false;
						settings.optionsMenuGravityLittleThingsEnabled = false;
						settings.optionsMenuGravityMicroscopeEnabled = false;
						settings.optionsMenuGravityOldHabitsEnabled = false;
						settings.optionsMenuGravityRadioactiveBoyEnabled = false;
						settings.optionsMenuGravityTrainTracksEnabled = false;
						settings.optionsGameADropBadInfluencesEnabled = false;
						settings.optionsGameADropErrorEnabled = false;
						settings.optionsGameADropFightOrDieEnabled = false;
						settings.optionsGameArsFarewellTheInnocentEnabled = false;
						settings.optionsGameArsSamaritanEnabled = false;
						settings.optionsGameGravityLittleThingsEnabled = false;
						settings.optionsGameGravityMicroscopeEnabled = false;
						settings.optionsGameGravityOldHabitsEnabled = false;
						settings.optionsGameGravityRadioactiveBoyEnabled = false;
						settings.optionsGameGravityTrainTracksEnabled = false;
					}

		

					audioTracksScrollPosition = GUI.BeginScrollView(new Rect((Screen.width - 600)/2, 100, 600, 274), audioTracksScrollPosition, new Rect(0,0,550,2010));
					GUI.Label (new Rect(35, 10, 370, 40), "A Drop A Day - Bad Influences", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("ADrop1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 10, 30, 30), playButtonTexture)) {
							pickMusic (41, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 10, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuADropBadInfluencesEnabled = GUI.Toggle (new Rect(400, 10, 100, 25), settings.optionsMenuADropBadInfluencesEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameADropBadInfluencesEnabled = GUI.Toggle (new Rect(500, 10, 100, 25), settings.optionsGameADropBadInfluencesEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));

					GUI.Label (new Rect(35, 50, 370, 40), "A Drop A Day - ERROR: Velocity Not...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!achievements.unlockedErrorSong){
						if (GUI.Button (new Rect (5, 50, 30, 30), new GUIContent (lockedButtonTexture, "TRACK LOCKED: Complete the\nEpilogue in Progressive\nMode to unlock 'A Drop A Day -\nError: Velocity Not Defined'"))) {
							mobile_showHelp = 8;
						}
					} else if (!GameObject.Find ("ADrop2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 50, 30, 30), playButtonTexture)) {
							pickMusic (42, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 50, 30, 30), stopButtonTexture)){
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					if (achievements.unlockedErrorSong) {
						settings.optionsMenuADropErrorEnabled = GUI.Toggle (new Rect(400, 50, 100, 25), settings.optionsMenuADropErrorEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
						settings.optionsGameADropErrorEnabled = GUI.Toggle (new Rect(500, 50, 100, 25), settings.optionsGameADropErrorEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					}
					GUI.Label (new Rect(35, 90, 370, 40), "A Drop A Day - Fight Or Die", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("ADrop3").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 90, 30, 30), playButtonTexture)) {
							pickMusic (43, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 90, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuADropFightOrDieEnabled = GUI.Toggle (new Rect(400, 90, 100, 25), settings.optionsMenuADropFightOrDieEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameADropFightOrDieEnabled = GUI.Toggle (new Rect(500, 90, 100, 25), settings.optionsGameADropFightOrDieEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 130, 370, 40), "Ars Sonor - Farewell The Innocent", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!achievements.unlockedFarewellTheInnocent){
						if (GUI.Button (new Rect (5, 130, 30, 30), new GUIContent (lockedButtonTexture, "TRACK LOCKED: Complete\nWorld 4 in Progressive Mode\nto unlock 'Ars Sonor -\nFarewell the Innocent'"))) {
							mobile_showHelp = 9;
						}
					} else if (!GameObject.Find ("Ars1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 130, 30, 30), playButtonTexture)) {
							pickMusic (44, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 130, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					if (achievements.unlockedFarewellTheInnocent) {
						settings.optionsMenuArsFarewellTheInnocentEnabled = GUI.Toggle (new Rect(400, 130, 100, 25), settings.optionsMenuArsFarewellTheInnocentEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
						settings.optionsGameArsFarewellTheInnocentEnabled = GUI.Toggle (new Rect(500, 130, 100, 25), settings.optionsGameArsFarewellTheInnocentEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					}
					GUI.Label (new Rect(35, 170, 370, 40), "Ars Sonor - Samaritan", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!achievements.unlockedSamaritan){
						if (GUI.Button (new Rect (5, 170, 30, 30), new GUIContent (lockedButtonTexture, "TRACK LOCKED: Complete\nWorld 4 in Progressive Mode\nto unlock 'Ars Sonor -\nSamaritan'"))) {
							mobile_showHelp = 10;
						}
					} else if (!GameObject.Find ("Ars2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 170, 30, 30), playButtonTexture)) {
							pickMusic (45, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 170, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					if (achievements.unlockedSamaritan) {
						settings.optionsMenuArsSamaritanEnabled = GUI.Toggle (new Rect(400, 170, 100, 25), settings.optionsMenuArsSamaritanEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
						settings.optionsGameArsSamaritanEnabled = GUI.Toggle (new Rect(500, 170, 100, 25), settings.optionsGameArsSamaritanEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					}
					GUI.Label (new Rect(35, 210, 370, 40), "Broke For Free - At The Count", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 210, 30, 30), playButtonTexture)) {
							pickMusic (1, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 210, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeAtTheCountEnabled = GUI.Toggle (new Rect(400, 210, 100, 25), settings.optionsMenuBrokeAtTheCountEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeAtTheCountEnabled = GUI.Toggle (new Rect(500, 210, 100, 25), settings.optionsGameBrokeAtTheCountEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 250, 370, 40), "Broke For Free - Blown Out", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 250, 30, 30), playButtonTexture)) {
							pickMusic (2, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 250, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeBlownOutEnabled = GUI.Toggle (new Rect(400, 250, 100, 25), settings.optionsMenuBrokeBlownOutEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeBlownOutEnabled = GUI.Toggle (new Rect(500, 250, 100, 25), settings.optionsGameBrokeBlownOutEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 290, 370, 40), "Broke For Free - Building The Sun", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke3").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 290, 30, 30), playButtonTexture)) {
							pickMusic (3, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 290, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeBuildingTheSunEnabled = GUI.Toggle (new Rect(400, 290, 100, 25), settings.optionsMenuBrokeBuildingTheSunEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeBuildingTheSunEnabled = GUI.Toggle (new Rect(500, 290, 100, 25), settings.optionsGameBrokeBuildingTheSunEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 330, 370, 40), "Broke For Free - Calm The Fuck D...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke4").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 330, 30, 30), playButtonTexture)) {
							pickMusic (4, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 330, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeCalmTheFuckDownEnabled = GUI.Toggle (new Rect(400, 330, 100, 25), settings.optionsMenuBrokeCalmTheFuckDownEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeCalmTheFuckDownEnabled = GUI.Toggle (new Rect(500, 330, 100, 25), settings.optionsGameBrokeCalmTheFuckDownEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 370, 370, 40), "Broke For Free - Caught In The Beat", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke5").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 370, 30, 30), playButtonTexture)) {
							pickMusic (5, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 370, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeCaughtInTheBeatEnabled = GUI.Toggle (new Rect(400, 370, 100, 25), settings.optionsMenuBrokeCaughtInTheBeatEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeCaughtInTheBeatEnabled = GUI.Toggle (new Rect(500, 370, 100, 25), settings.optionsGameBrokeCaughtInTheBeatEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 410, 370, 40), "Broke For Free - Fuck It", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke6").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 410, 30, 30), playButtonTexture)) {
							pickMusic (6, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 410, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeFuckItEnabled = GUI.Toggle (new Rect(400, 410, 100, 25), settings.optionsMenuBrokeFuckItEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeFuckItEnabled = GUI.Toggle (new Rect(500, 410, 100, 25), settings.optionsGameBrokeFuckItEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 450, 370, 40), "Broke For Free - Hella", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke7").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 450, 30, 30), playButtonTexture)) {
							pickMusic (7, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 450, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeHellaEnabled = GUI.Toggle (new Rect(400, 450, 100, 25), settings.optionsMenuBrokeHellaEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeHellaEnabled = GUI.Toggle (new Rect(500, 450, 100, 25), settings.optionsGameBrokeHellaEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 490, 370, 40), "Broke For Free - High School Snaps", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke8").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 490, 30, 30), playButtonTexture)) {
							pickMusic (8, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 490, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeHighSchoolSnapsEnabled = GUI.Toggle (new Rect(400, 490, 100, 25), settings.optionsMenuBrokeHighSchoolSnapsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeHighSchoolSnapsEnabled = GUI.Toggle (new Rect(500, 490, 100, 25), settings.optionsGameBrokeHighSchoolSnapsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 530, 370, 40), "Broke For Free - Like Swimming", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke9").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 530, 30, 30), playButtonTexture)) {
							pickMusic (9, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 530, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeLikeSwimmingEnabled = GUI.Toggle (new Rect(400, 530, 100, 25), settings.optionsMenuBrokeLikeSwimmingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeLikeSwimmingEnabled = GUI.Toggle (new Rect(500, 530, 100, 25), settings.optionsGameBrokeLikeSwimmingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 570, 370, 40), "Broke For Free - Living In Reverse", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke10").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 570, 30, 30), playButtonTexture)) {
							pickMusic (10, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 570, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeLivingInReverseEnabled = GUI.Toggle (new Rect(400, 570, 100, 25), settings.optionsMenuBrokeLivingInReverseEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeLivingInReverseEnabled = GUI.Toggle (new Rect(500, 570, 100, 25), settings.optionsGameBrokeLivingInReverseEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 610, 370, 40), "Broke For Free - Luminous", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke11").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 610, 30, 30), playButtonTexture)) {
							pickMusic (11, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 610, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeLuminousEnabled = GUI.Toggle (new Rect(400, 610, 100, 25), settings.optionsMenuBrokeLuminousEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeLuminousEnabled = GUI.Toggle (new Rect(500, 610, 100, 25), settings.optionsGameBrokeLuminousEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 650, 370, 40), "Broke For Free - Mell's Parade", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke12").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 650, 30, 30), playButtonTexture)) {
							pickMusic (12, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 650, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeMellsParadeEnabled = GUI.Toggle (new Rect(400, 650, 100, 25), settings.optionsMenuBrokeMellsParadeEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeMellsParadeEnabled = GUI.Toggle (new Rect(500, 650, 100, 25), settings.optionsGameBrokeMellsParadeEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 690, 370, 40), "Broke For Free - Quit Bitching", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke13").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 690, 30, 30), playButtonTexture)) {
							pickMusic (13, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 690, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeQuitBitchingEnabled = GUI.Toggle (new Rect(400, 690, 100, 25), settings.optionsMenuBrokeQuitBitchingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeQuitBitchingEnabled = GUI.Toggle (new Rect(500, 690, 100, 25), settings.optionsGameBrokeQuitBitchingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 730, 370, 40), "Broke For Free - Something Elated", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke14").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 730, 30, 30), playButtonTexture)) {
							pickMusic (14, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 730, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeSomethingElatedEnabled = GUI.Toggle (new Rect(400, 730, 100, 25), settings.optionsMenuBrokeSomethingElatedEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeSomethingElatedEnabled = GUI.Toggle (new Rect(500, 730, 100, 25), settings.optionsGameBrokeSomethingElatedEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 770, 370, 40), "Broke For Free - The Great", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("broke15").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 770, 30, 30), playButtonTexture)) {
							pickMusic (15, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 770, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuBrokeTheGreatEnabled = GUI.Toggle (new Rect(400, 770, 100, 25), settings.optionsMenuBrokeTheGreatEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameBrokeTheGreatEnabled = GUI.Toggle (new Rect(500, 770, 100, 25), settings.optionsGameBrokeTheGreatEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 810, 370, 40), "Chris Zabriskie - Another Version...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("chris1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 810, 30, 30), playButtonTexture)) {
							pickMusic (16, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 810, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuChrisAnotherVersionOfYouEnabled = GUI.Toggle (new Rect(400, 810, 100, 25), settings.optionsMenuChrisAnotherVersionOfYouEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameChrisAnotherVersionOfYouEnabled = GUI.Toggle (new Rect(500, 810, 100, 25), settings.optionsGameChrisAnotherVersionOfYouEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 850, 370, 40), "Chris Zabriskie - Divider", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("chris2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 850, 30, 30), playButtonTexture)) {
							pickMusic (17, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 850, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuChrisDividerEnabled = GUI.Toggle (new Rect(400, 850, 100, 25), settings.optionsMenuChrisDividerEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameChrisDividerEnabled = GUI.Toggle (new Rect(500, 850, 100, 25), settings.optionsGameChrisDividerEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 890, 370, 40), "Chris Zabriskie - Everybody's Got...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("chris3").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 890, 30, 30), playButtonTexture)) {
							pickMusic (18, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 890, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled = GUI.Toggle (new Rect(400, 890, 100, 25), settings.optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled = GUI.Toggle (new Rect(500, 890, 100, 25), settings.optionsGameChrisEverybodysGotProblemsThatArentMineEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 930, 370, 40), "Chris Zabriskie - The 49th Street...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("chris4").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 930, 30, 30), playButtonTexture)) {
							pickMusic (19, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 930, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuChrisThe49thStreetGalleriaEnabled = GUI.Toggle (new Rect(400, 930, 100, 25), settings.optionsMenuChrisThe49thStreetGalleriaEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameChrisThe49thStreetGalleriaEnabled = GUI.Toggle (new Rect(500, 930, 100, 25), settings.optionsGameChrisThe49thStreetGalleriaEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 970, 370, 40), "Chris Zabriskie - The Life and Deat...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("chris5").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 970, 30, 30), playButtonTexture)) {
							pickMusic (20, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 970, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = GUI.Toggle (new Rect(400, 970, 100, 25), settings.optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = GUI.Toggle (new Rect(500, 970, 100, 25), settings.optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1010, 370, 40), "Chris Zabriskie - There's a Special...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("chris6").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1010, 30, 30), playButtonTexture)) {
							pickMusic (21, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1010, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled = GUI.Toggle (new Rect(400, 1010, 100, 25), settings.optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled = GUI.Toggle (new Rect(500, 1010, 100, 25), settings.optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1050, 370, 40), "D SMILEZ - Inspiration (I Won't Give...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("DSMILEZ1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1050, 30, 30), playButtonTexture)) {
							pickMusic (22, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1050, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDSMILEZInspirationEnabled = GUI.Toggle (new Rect(400, 1050, 100, 25), settings.optionsMenuDSMILEZInspirationEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDSMILEZInspirationEnabled = GUI.Toggle (new Rect(500, 1050, 100, 25), settings.optionsGameDSMILEZInspirationEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1090, 370, 40), "D SMILEZ - Let Your Body Move", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("DSMILEZ2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1090, 30, 30), playButtonTexture)) {
							pickMusic (23, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1090, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDSMILEZLetYourBodyMoveEnabled = GUI.Toggle (new Rect(400, 1090, 100, 25), settings.optionsMenuDSMILEZLetYourBodyMoveEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDSMILEZLetYourBodyMoveEnabled = GUI.Toggle (new Rect(500, 1090, 100, 25), settings.optionsGameDSMILEZLetYourBodyMoveEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1130, 370, 40), "D SMILEZ - Lost In The Music", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("DSMILEZ3").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1130, 30, 30), playButtonTexture)) {
							pickMusic (24, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1130, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDSMILEZLostInTheMusicEnabled = GUI.Toggle (new Rect(400, 1130, 100, 25), settings.optionsMenuDSMILEZLostInTheMusicEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDSMILEZLostInTheMusicEnabled = GUI.Toggle (new Rect(500, 1130, 100, 25), settings.optionsGameDSMILEZLostInTheMusicEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1170, 370, 40), "Decktonic - Act IV", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("decktonic1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1170, 30, 30), playButtonTexture)) {
							pickMusic (25, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1170, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDecktonicActIVEnabled = GUI.Toggle (new Rect(400, 1170, 100, 25), settings.optionsMenuDecktonicActIVEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDecktonicActIVEnabled = GUI.Toggle (new Rect(500, 1170, 100, 25), settings.optionsGameDecktonicActIVEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1210, 370, 40), "Decktonic - B@$$ J▲|\\/|", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("decktonic2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1210, 30, 30), playButtonTexture)) {
							pickMusic (37, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1210, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDecktonicBassJamEnabled = GUI.Toggle (new Rect(400, 1210, 100, 25), settings.optionsMenuDecktonicBassJamEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDecktonicBassJamEnabled = GUI.Toggle (new Rect(500, 1210, 100, 25), settings.optionsGameDecktonicBassJamEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1250, 370, 40), "Decktonic - Night Drive", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("decktonic3").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1250, 30, 30), playButtonTexture)) {
							pickMusic (38, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1250, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDecktonicNightDriveEnabled = GUI.Toggle (new Rect(400, 1250, 100, 25), settings.optionsMenuDecktonicNightDriveEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDecktonicNightDriveEnabled = GUI.Toggle (new Rect(500, 1250, 100, 25), settings.optionsGameDecktonicNightDriveEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1290, 370, 40), "Decktonic - Stars", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("decktonic4").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1290, 30, 30), playButtonTexture)) {
							pickMusic (39, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1290, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDecktonicStarsEnabled = GUI.Toggle (new Rect(400, 1290, 100, 25), settings.optionsMenuDecktonicStarsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDecktonicStarsEnabled = GUI.Toggle (new Rect(500, 1290, 100, 25), settings.optionsGameDecktonicStarsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1330, 370, 40), "Decktonic - Watch Your Dubstep Ve...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("decktonic5").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1330, 30, 30), playButtonTexture)) {
							pickMusic (40, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1330, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuDecktonicWatchYourDubstepEnabled = GUI.Toggle (new Rect(400, 1330, 100, 25), settings.optionsMenuDecktonicWatchYourDubstepEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameDecktonicWatchYourDubstepEnabled = GUI.Toggle (new Rect(500, 1330, 100, 25), settings.optionsGameDecktonicWatchYourDubstepEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1370, 370, 40), "Gravity Sound - Little Things", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!achievements.unlockedLittleThings) {
						if (GUI.Button (new Rect (5, 1370, 30, 30), new GUIContent (lockedButtonTexture, "TRACK LOCKED: Watch\nthe Credits to unlock\n'Gravity Sound - Little Things'"))) {
							mobile_showHelp = 11;
						}
					} else if (!GameObject.Find ("gravity1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1370, 30, 30), playButtonTexture)) {
							pickMusic (46, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1370, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					if (achievements.unlockedLittleThings) {
						settings.optionsMenuGravityLittleThingsEnabled = GUI.Toggle (new Rect(400, 1370, 100, 25), settings.optionsMenuGravityLittleThingsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
						settings.optionsGameGravityLittleThingsEnabled = GUI.Toggle (new Rect(500, 1370, 100, 25), settings.optionsGameGravityLittleThingsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					}
					GUI.Label (new Rect(35, 1410, 370, 40), "Gravity Sound - Microscope", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("gravity2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1410, 30, 30), playButtonTexture)) {
							pickMusic (47, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1410, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuGravityMicroscopeEnabled = GUI.Toggle (new Rect(400, 1410, 100, 25), settings.optionsMenuGravityMicroscopeEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameGravityMicroscopeEnabled = GUI.Toggle (new Rect(500, 1410, 100, 25), settings.optionsGameGravityMicroscopeEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1450, 370, 40), "Gravity Sound - Old Habits", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("gravity3").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1450, 30, 30), playButtonTexture)) {
							pickMusic (48, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1450, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuGravityOldHabitsEnabled = GUI.Toggle (new Rect(400, 1450, 100, 25), settings.optionsMenuGravityOldHabitsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameGravityOldHabitsEnabled = GUI.Toggle (new Rect(500, 1450, 100, 25), settings.optionsGameGravityOldHabitsEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1490, 370, 40), "Gravity Sound - Radioactive Boy", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("gravity4").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1490, 30, 30), playButtonTexture)) {
							pickMusic (49, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1490, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuGravityRadioactiveBoyEnabled = GUI.Toggle (new Rect(400, 1490, 100, 25), settings.optionsMenuGravityRadioactiveBoyEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameGravityRadioactiveBoyEnabled = GUI.Toggle (new Rect(500, 1490, 100, 25), settings.optionsGameGravityRadioactiveBoyEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1530, 370, 40), "Gravity Sound - Train Tracks", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("gravity5").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1530, 30, 30), playButtonTexture)) {
							pickMusic (50, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1530, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuGravityTrainTracksEnabled = GUI.Toggle (new Rect(400, 1530, 100, 25), settings.optionsMenuGravityTrainTracksEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameGravityTrainTracksEnabled = GUI.Toggle (new Rect(500, 1530, 100, 25), settings.optionsGameGravityTrainTracksEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));

					GUI.Label (new Rect(35, 1570, 370, 40), "Kai Engel - Low Horizon", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("kai1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1570, 30, 30), playButtonTexture)) {
							pickMusic (26, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1570, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuKaiEngelLowHorizonEnabled = GUI.Toggle (new Rect(400, 1570, 100, 25), settings.optionsMenuKaiEngelLowHorizonEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameKaiEngelLowHorizonEnabled = GUI.Toggle (new Rect(500, 1570, 100, 25), settings.optionsGameKaiEngelLowHorizonEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1610, 370, 40), "Kai Engel - Nothing", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("kai2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1610, 30, 30), playButtonTexture)) {
							pickMusic (27, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1610, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuKaiEngelNothingEnabled = GUI.Toggle (new Rect(400, 1610, 100, 25), settings.optionsMenuKaiEngelNothingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameKaiEngelNothingEnabled = GUI.Toggle (new Rect(500, 1610, 100, 25), settings.optionsGameKaiEngelNothingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1650, 370, 40), "Kai Engel - Something", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("kai3").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1650, 30, 30), playButtonTexture)) {
							pickMusic (28, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1650, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuKaiEngelSomethingEnabled = GUI.Toggle (new Rect(400, 1650, 100, 25), settings.optionsMenuKaiEngelSomethingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameKaiEngelSomethingEnabled = GUI.Toggle (new Rect(500, 1650, 100, 25), settings.optionsGameKaiEngelSomethingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1690, 370, 40), "Kai Engel - Wake Up!", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("kai4").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1690, 30, 30), playButtonTexture)) {
							pickMusic (29, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1690, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuKaiEngelWakeUpEnabled = GUI.Toggle (new Rect(400, 1690, 100, 25), settings.optionsMenuKaiEngelWakeUpEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameKaiEngelWakeUpEnabled = GUI.Toggle (new Rect(500, 1690, 100, 25), settings.optionsGameKaiEngelWakeUpEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1730, 370, 40), "Parijat Mishra - 4th Night", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("parijat1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1730, 30, 30), playButtonTexture)) {
							pickMusic (31, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1730, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuParijat4thNightEnabled = GUI.Toggle (new Rect(400, 1730, 100, 25), settings.optionsMenuParijat4thNightEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameParijat4thNightEnabled = GUI.Toggle (new Rect(500, 1730, 100, 25), settings.optionsGameParijat4thNightEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1770, 370, 40), "Pierlo - Barbarian", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("pierlo1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1770, 30, 30), playButtonTexture)) {
							pickMusic (30, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1770, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuPierloBarbarianEnabled = GUI.Toggle (new Rect(400, 1770, 100, 25), settings.optionsMenuPierloBarbarianEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGamePierloBarbarianEnabled = GUI.Toggle (new Rect(500, 1770, 100, 25), settings.optionsGamePierloBarbarianEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1810, 370, 40), "Revolution Void - How Exciting", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("revolution1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1810, 30, 30), playButtonTexture)) {
							pickMusic (32, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1810, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuRevolutionVoidHowExcitingEnabled = GUI.Toggle (new Rect(400, 1810, 100, 25), settings.optionsMenuRevolutionVoidHowExcitingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameRevolutionVoidHowExcitingEnabled = GUI.Toggle (new Rect(500, 1810, 100, 25), settings.optionsGameRevolutionVoidHowExcitingEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1850, 370, 40), "Revolution Void - Someone Else's M...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("revolution2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1850, 30, 30), playButtonTexture)) {
							pickMusic (33, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1850, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled = GUI.Toggle (new Rect(400, 1850, 100, 25), settings.optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled = GUI.Toggle (new Rect(500, 1850, 100, 25), settings.optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1890, 370, 40), "Sergey Cheremisinov - Labyrinth", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("sergey1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1890, 30, 30), playButtonTexture)) {
							pickMusic (34, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1890, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuSergeyLabyrinthEnabled = GUI.Toggle (new Rect(400, 1890, 100, 25), settings.optionsMenuSergeyLabyrinthEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameSergeyLabyrinthEnabled = GUI.Toggle (new Rect(500, 1890, 100, 25), settings.optionsGameSergeyLabyrinthEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1930, 370, 40), "Sergey Cheremisinov - Now You Are...", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("sergey2").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1930, 30, 30), playButtonTexture)) {
							pickMusic (35, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1930, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuSergeyNowYouAreHereEnabled = GUI.Toggle (new Rect(400, 1930, 100, 25), settings.optionsMenuSergeyNowYouAreHereEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameSergeyNowYouAreHereEnabled = GUI.Toggle (new Rect(500, 1930, 100, 25), settings.optionsGameSergeyNowYouAreHereEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					GUI.Label (new Rect(35, 1970, 370, 40), "Tours - Enthusiast", CDGUIMenuSkin.FindStyle("Label15Pt"));
					if (!GameObject.Find ("tours1").GetComponent<AudioSource> ().isPlaying) {
						if (GUI.Button (new Rect (5, 1970, 30, 30), playButtonTexture)) {
							pickMusic (36, false);
						}
					} else {
						if (GUI.Button (new Rect (5, 1970, 30, 30), stopButtonTexture))
						{
							currentSong.GetComponent<AudioSource>().Stop();
						}
					}
					settings.optionsMenuToursEnthusiastEnabled = GUI.Toggle (new Rect(400, 1970, 100, 25), settings.optionsMenuToursEnthusiastEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));
					settings.optionsGameToursEnthusiastEnabled = GUI.Toggle (new Rect(500, 1970, 100, 25), settings.optionsGameToursEnthusiastEnabled, "", CDGUIMenuSkin.FindStyle ("ToggleAudioMenu"));


					GUI.EndScrollView();
					GUI.color = Color.black;
					GUI.Box (new Rect((Screen.width - 600)/2 + 400, 50, 5, 325), "");
					GUI.Box (new Rect((Screen.width - 600)/2 + 500, 50, 5, 325), "");
					GUI.Box (new Rect((Screen.width - 600)/2, 100, 600, 5), "");

					GUI.color = Color.white;
					if (Application.platform == RuntimePlatform.Android) {
						if (mobile_showHelp == 8) {
							GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "TRACK LOCKED:\nComplete the Epilogue in Progressive Mode to unlock 'A Drop A Day - Error: Velocity Not Defined'", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
						} else if (mobile_showHelp == 9) {
							GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "TRACK LOCKED:\nComplete World 4 in Progressive Mode to unlock 'Ars Sonor - Farewell The Innocent'", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
						} else if (mobile_showHelp == 10) {
							GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "TRACK LOCKED:\nComplete World 4 in Progressive Mode to unlock 'Ars Sonor - Samaritan'", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
						} else if (mobile_showHelp == 11) {
							GUI.Label (new Rect (50, Screen.height - 175, 350, 150), "TRACK LOCKED:\nWatch the credits to unlock 'Gravity Sound - Little Things'", CDGUIMenuSkin.FindStyle ("mobile_HelpBox"));
						}
					}
					GUI.color = Color.black;
				}



			}
			else if (showLeaderboards && !showLeaderboardsResetAreYouSure)
			{
				if (leaderboardsDifficulty == -1)
				{
					leaderboardsDifficulty = settings.difficulty;
					leaderboardsRandomizer = settings.modifierRandomizer;
					leaderboardsProgressive = settings.modifierProgressive;
					if (leaderboardsRandomizer || leaderboardsProgressive)
					{
						settings.difficulty = 3;
					}
				}

				if (backButtonPositionChangeThreshold == 0)
				{
					if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) 
					{
						showLeaderboards = false;
						settings.difficulty = leaderboardsDifficulty;
						settings.modifierRandomizer = leaderboardsRandomizer;
						settings.modifierProgressive = leaderboardsProgressive;
						settings.checkSettings(true);
						leaderboardsDifficulty = -1;
					}
				}
				else
				{
					if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) 
					{
						showLeaderboards = false;
						settings.difficulty = leaderboardsDifficulty;
						settings.modifierRandomizer = leaderboardsRandomizer;
						settings.modifierProgressive = leaderboardsProgressive;
						settings.checkSettings(true);
						leaderboardsDifficulty = -1;
					}
				}
				if (GUI.Button (new Rect((Screen.width - 600)/2 + 5, 385, 250, 40), "Reset Leaderboards"))
				{
					showLeaderboardsResetAreYouSure = true;
				}
				GUI.Box(new Rect(Screen.width/2 - 105, 20, 210, 40),"");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label(new Rect(Screen.width/2 - 100, 15, 200, 40),"Leaderboards");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				GUI.Box (new Rect ((Screen.width - 600)/2, 50, 600, 325), "");

				GUI.skin = CDGUILargeTextSkin;
				GUI.color = Color.black;
				if (!settings.modifierRandomizer && !settings.modifierProgressive)
				{
					GUI.Label (new Rect((Screen.width / 2 - 150), 60, 300, 40), settings.difficultyString + " - Standard");
				}
				else if (settings.modifierProgressive)
				{
					GUI.Label (new Rect((Screen.width / 2 - 150), 60, 300, 40), "Progressive");
				}
				else if (settings.modifierRandomizer)
				{
					GUI.Label (new Rect((Screen.width / 2 - 150), 60, 300, 40), "Randomized");
				}


				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				if (GUI.Button(new Rect((Screen.width / 2 - 200), 55, 40, 50), "<"))
				{

					if (settings.difficulty > 0 && !settings.modifierRandomizer && !settings.modifierProgressive)
					{
						settings.difficulty--;
					}
					else if (settings.difficulty == 3 && !settings.modifierRandomizer && settings.modifierProgressive)
					{
						settings.modifierProgressive = false;
					}
					else if (settings.difficulty == 3 && settings.modifierRandomizer && !settings.modifierProgressive)
					{
						settings.modifierRandomizer = false;
						settings.modifierProgressive = true;
					}
					settings.checkSettings(true);
					leaderboards.pullLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);
				}
				if (GUI.Button(new Rect((Screen.width / 2 + 155), 55, 40, 50), ">"))
				{
					if (settings.difficulty == 3 && !settings.modifierRandomizer && settings.modifierProgressive)
					{
						settings.modifierProgressive = false;
						settings.modifierRandomizer = true;
					}
					else if (settings.difficulty == 3 && !settings.modifierRandomizer && !settings.modifierProgressive)
					{
						settings.modifierProgressive = true;
					}
					else if (settings.difficulty < 3)
					{
						settings.difficulty++;
					}

					settings.checkSettings(true);
					leaderboards.pullLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);
				}

				GUI.Box (new Rect((Screen.width - 575)/2, 110, 575, 260), "", CDGUIMenuSkin.FindStyle("BoxBlackBG"));

				GUI.Label (new Rect((Screen.width - 575)/2, 115, 575, 50), leaderboards.leaderboard1.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards1"));
				GUI.Label (new Rect((Screen.width - 575)/2, 165, 575, 35), leaderboards.leaderboard2.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards2"));
				GUI.Label (new Rect((Screen.width - 575)/2, 200, 575, 25), leaderboards.leaderboard3.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards3"));
				GUI.Label (new Rect((Screen.width - 575)/2, 225, 575, 20), leaderboards.leaderboard4.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
				GUI.Label (new Rect((Screen.width - 575)/2, 245, 575, 20), leaderboards.leaderboard5.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
				GUI.Label (new Rect((Screen.width - 575)/2, 265, 575, 20), leaderboards.leaderboard6.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
				GUI.Label (new Rect((Screen.width - 575)/2, 285, 575, 20), leaderboards.leaderboard7.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
				GUI.Label (new Rect((Screen.width - 575)/2, 305, 575, 20), leaderboards.leaderboard8.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
				GUI.Label (new Rect((Screen.width - 575)/2, 325, 575, 20), leaderboards.leaderboard9.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));
				GUI.Label (new Rect((Screen.width - 575)/2, 345, 575, 20), leaderboards.leaderboard10.ToString(), CDGUIMenuSkin.FindStyle ("leaderboards4"));

			}
			else if (showCredits == true)
			{
				if (!creditsDoOnce) {
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
						if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) 
						{
							showExtras = false;
							creditsStep = 4;
						}
					}
					else
					{
						if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) 
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
			else if (showAchievements)
			{
				GUI.Box(new Rect(Screen.width/2 - 100, 20, 200, 40),"");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label(new Rect(Screen.width/2 - 100, 15, 200, 40),"ACHIEVEMENTS");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				GUI.Box (new Rect ((Screen.width - 600)/2, 50, 600, 325), "");

				achievementsScrollPosition = GUI.BeginScrollView(new Rect((Screen.width - 600)/2, 50, 600, 325), achievementsScrollPosition, new Rect(0,0,550,2825));
				achListGUIHeight = 0;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Cube Avoider - Get 100 Points in any gamemode with any settings.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach100points) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "You know that achievement in most games that you unlock in the first few seconds? This is pretty much that.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Cube Dodger - Get 1000 Points in any gamemode with any settings.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach1000points) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "You listened to the title and decided to not crash into any cubes for a while! Congrats!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Cube Cutter - Get 5000 Points in any gamemode with any settings.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach5000points) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Your dedication to not running into geometric shapes is astounding.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Cube Dancer - Get 10000 Points in any gamemode with any settings.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach10000points) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "You danced around those cubes like nobody was watching!... except I was... and you should be ashamed.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Cube Master - Get 20000 Points in any gamemode with any settings.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach20000points) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "I can probably get a good price for your autograph on eBay or something...", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Easy Livin' - Get 5000 points in Standard Mode on Easy Difficulty or higher.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach5000Easy) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "GG EZ", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Conventionally Cool - Get 5000 points in Standard Mode on Normal Difficulty or higher.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach5000Standard) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "What's cooler than being cool? ICE COLD!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Hardened Veteran - Get 5000 Points in Standard Mode on Hard difficulty or higher.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach5000Hard) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "You fought in the great cube war, and lived on to tell the tale!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Extremist - Get 5000 Points in Standard Mode on Extreme Difficulty.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach5000Extreme) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "You take cube dodging TO THE EXTREME!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "R.I.P. - Die.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achDie) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Die. Die! DIE!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Care Package - Get 1000 points in Standard Mode with the Airdrop Modifier turned on.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach1000Airdrop) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Enemies Everywhere!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Dodgeball - Get 1000 Points in Standard Mode with the Cube Type Modifier set to Spheres.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach1000Spheres) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Because 'Geometric Shape Avoider' doesn't roll off the tongue as well.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "D.U.I. - Get 1000 Points in Standard Mode with the Drunk Driver modifier turned on.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach1000DrunkDriver) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "You Ignored basic human decency and drove drunk! What would your mother think?", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Arcade Cabinet - Get 1000 Points in Standard Mode with the Power Ups modifier turned on.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach1000Powerups) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "UP UP DOWN DOWN LEFT RIGHT LEFT RIGHT B A START", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Gotta Go Fast! - Get 1000 Points in Standard Mode with the Speed Multiplier modifier set to 1.5x or Higher.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach1000SpeedMult) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Fun Fact: There used to be 'International Hedgehog Olympic Games' that included sprints, hurdles, and floor exercises!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Christopher Columbus - Reach the edge of the map.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achXLimiters) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Error: 404 - India Not Found.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Luck of the Draw - Get 10000 points in Randomizer Mode.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach10000Randomizer) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "RNGesus shines upon you on this wonderful day!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Meat and Potatoes - Clear World 1 in Progressive Mode.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achProgressive1) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Great, now I'm hungry...", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Lawrence Approved - Clear World 2 in Progressive Mode.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achProgressive2) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Put on your fingerless gloves and hop in the van!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Glossophobia - Clear World 3 in Progressive Mode.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achProgressive3) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "All the world's a stage and all the cubes and spheres merely players...", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Event Horizon - Clear World 4 in Progressive Mode.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achProgressive4) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Ventured beyond the point of no return.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "The End - Clear The Epilogue in Progressive Mode.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achProgressive5) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "The lone and level sands stretch far away...", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Officially Crazy - Complete Progressive Mode - From the beginning to the epilogue - in one continuous run.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achProgressiveContinuous) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Please go outside. Your friends and family miss you.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Non-Conformist - End up on the wrong side of the cube cutters.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achBrokenCubeCutters) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "It's not a bug, it's a feature!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Cube Destroyer - Blow up a cube with a powerup.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achShootCube) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Ka-BOOOOOM!", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 25 + achListGUIHeight, 450, 20), "Masochist - Get 500 points in Standard Mode on Extreme Difficulty with Speed Multiplier: 2X, Cube Type: Spheres, Camera Mode: Spinning, and everything else on.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.ach500ExtremeWithAllModifiers) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Some developers just want to watch the world burn.", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;
				GUI.Label (new Rect (100, 20 + achListGUIHeight, 450, 20), "Thank You! - Watch the Credits without skipping.", GUI.skin.FindStyle("achievementTitle"));
				if (achievements.achFinishCredits) {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementUnlocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "<3", GUI.skin.FindStyle("achievementDescription"));
				} else {
					GUI.Box (new Rect (0, 0 + achListGUIHeight, 100, 100), achievementLocked, GUI.skin.FindStyle("achievementPicture"));
					GUI.Label (new Rect (100, 70 + achListGUIHeight, 450, 20), "Locked!", GUI.skin.FindStyle("achievementDescription"));
				}
				GUI.color = Color.black;
				GUI.Box (new Rect (0, 100 + achListGUIHeight, 600, 5), "");
				GUI.color = Color.white;
				achListGUIHeight = achListGUIHeight + 105;

				GUI.EndScrollView ();

				if (backButtonPositionChangeThreshold == 0)
				{
					if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) 
					{
						showAchievements = false;
					}
				}
				else
				{
					if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) 
					{
						showAchievements = false;
					}
				}
			}
			else if (showGameModeSelection == true)
			{
				GUI.Box(new Rect(Screen.width/2 - 80, 35, 160, 40),"");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label(new Rect(Screen.width/2 - 70, 30, 140, 40),"PLAY");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				//GUI.Box (new Rect ((Screen.width - 600)/2, 50, 600, 325), "");

				if (settings.showControlsTutorial) {
					GUI.Box (new Rect (Screen.width / 2 - 400, 70, 800, 400), "");
					if (Application.isMobilePlatform || settings.simulateMobileOptimization) {
						GUI.DrawTexture (new Rect (Screen.width / 2 - 400, 70, 800, 400), controlsTutorialMobile);
					} else {
						GUI.DrawTexture (new Rect (Screen.width / 2 - 400, 70, 800, 400), controlsTutorialPC);
					}

					if (GUI.Button (new Rect (Screen.width / 2 - (125 / 2), 460, 125, 40), "Continue")) {
						settings.showControlsTutorial = false;
						settings.saveSettings ();
					}

				} else if (showProgressiveOptions){
					GUI.Box (new Rect (Screen.width / 2 - 400, 290, 800, 145), "");

					settings.progressiveCheckpoints = GUI.Toggle (new Rect (Screen.width / 2 - 350, 350, 250, 100), settings.progressiveCheckpoints, "Activate Checkpoints");

					//Unselected Buttons
					if (settings.progressiveCurrentCheckpoint != 0) {
						if (GUI.Button (new Rect (Screen.width / 2 - 385, 80, 150, 225), "World 1", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld1-Unselected"))) {
							settings.progressiveCurrentCheckpoint = 0;
						}
					}
					if (settings.progressiveCurrentCheckpoint != 1) {
						if (achievements.achProgressive1) {
							if (GUI.Button (new Rect (Screen.width / 2 - 230, 80, 150, 225), "World 2", CDGUIMenuSkin.FindStyle ("bigMenuButtonStyleWorld2-Unselected"))) {
								settings.progressiveCurrentCheckpoint = 1;
							}
						} else {
							GUI.Button (new Rect (Screen.width / 2 - 230, 80, 150, 225), "Locked", CDGUIMenuSkin.FindStyle ("bigMenuButtonStyleLocked"));
						}
					}
					if (settings.progressiveCurrentCheckpoint != 2) {
						if (achievements.achProgressive2) {
							if (GUI.Button (new Rect (Screen.width / 2 - 75, 80, 150, 225), "World 3", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld3-Unselected"))) {
								settings.progressiveCurrentCheckpoint = 2;
							}
						} else {
							GUI.Button (new Rect (Screen.width / 2 - 75, 80, 150, 225), "Locked", CDGUIMenuSkin.FindStyle ("bigMenuButtonStyleLocked"));
						}
					}
					if (settings.progressiveCurrentCheckpoint != 3) {
						if (achievements.achProgressive3) {
							if (GUI.Button (new Rect (Screen.width / 2 + 80, 80, 150, 225), "World 4", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld4-Unselected"))) {
								settings.progressiveCurrentCheckpoint = 3;
							}
						} else {
							GUI.Button (new Rect (Screen.width / 2 + 80, 80, 150, 225), "Locked", CDGUIMenuSkin.FindStyle ("bigMenuButtonStyleLocked"));
						}
					}
					if (settings.progressiveCurrentCheckpoint != 4) {
						if (achievements.achProgressive4) {
							if (GUI.Button (new Rect (Screen.width / 2 + 235, 80, 150, 225), "EPILOGUE", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld5-Unselected"))) {
								settings.progressiveCurrentCheckpoint = 4;
							}
						} else {
							GUI.Button (new Rect (Screen.width / 2 + 235, 80, 150, 225), "Locked", CDGUIMenuSkin.FindStyle ("bigMenuButtonStyleLocked"));
						}
					}
					//Selected Buttons
					if (settings.progressiveCurrentCheckpoint == 0) {
						if (GUI.Button (new Rect (Screen.width / 2 - 385, 80, 150, 225), "World 1", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld1-Selected"))) {
							settings.progressiveCurrentCheckpoint = 0;
						}
					}

					if (settings.progressiveCurrentCheckpoint == 1) {
						if (GUI.Button (new Rect (Screen.width / 2 - 230, 80, 150, 225), "World 2", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld2-Selected"))) {
							settings.progressiveCurrentCheckpoint = 1;
						}
					}
					if (settings.progressiveCurrentCheckpoint == 2) {
						if (GUI.Button (new Rect (Screen.width / 2 - 75, 80, 150, 225), "World 3", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld3-Selected"))) {
							settings.progressiveCurrentCheckpoint = 2;
						}
					}
					if (settings.progressiveCurrentCheckpoint == 3) {
						if (GUI.Button (new Rect (Screen.width / 2 + 80, 80, 150, 225), "World 4", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld4-Selected"))) {
							settings.progressiveCurrentCheckpoint = 3;
						}
					}
					if (settings.progressiveCurrentCheckpoint == 4) {
						if (GUI.Button (new Rect (Screen.width / 2 + 235, 80, 150, 225), "EPILOGUE", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleWorld5-Selected"))) {
							settings.progressiveCurrentCheckpoint = 4;
						}
					}

					if (backButtonPositionChangeThreshold == 0)
					{
						if (GUI.Button (new Rect (25, Screen.height - 100, 200, 60), "Back")) 
						{
							showProgressiveOptions = false;
							settings.saveSettings ();
						}
					}
					else
					{
						if (GUI.Button (new Rect (25, 380, 200, 60), "Back")) 
						{
							showProgressiveOptions = false;
							settings.saveSettings ();
						}
					}

					if (backButtonPositionChangeThreshold == 0)
					{
						if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Play")) 
						{
							showProgressiveOptions = false;
							showGameModeSelection = false;
							initLoadGame = true;
							settings.saveSettings ();
						}
					}
					else
					{
						if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Play")) 
						{
							showProgressiveOptions = false;
							showGameModeSelection = false;
							initLoadGame = true;
							settings.saveSettings ();
						}
					}
				} else {
					if (GUI.Button (new Rect ((Screen.width - 590)/2, 60, 195, 300), "STANDARD", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleStandardPlay")))
					{
						settings.modifierRandomizer = false;
						settings.modifierProgressive = false;
						initLoadGame = true;
						showGameModeSelection = false;
					}
					if (GUI.Button (new Rect ((Screen.width - 195)/2, 60, 195, 300), "PROGRESSIVE", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleProgressivePlay")))
					{
						//Placeholder for when the mode is actually implemented
						settings.modifierProgressive = true;
						settings.modifierRandomizer = false;
						showProgressiveOptions = true;

						if (settings.progressiveCurrentCheckpoint == 1 && !achievements.achProgressive1) {
							settings.progressiveCurrentCheckpoint = 0;
						} else if (settings.progressiveCurrentCheckpoint == 2 && !achievements.achProgressive2) {
							settings.progressiveCurrentCheckpoint = 0;
						} else if (settings.progressiveCurrentCheckpoint == 3 && !achievements.achProgressive3) {
							settings.progressiveCurrentCheckpoint = 0;
						} else if (settings.progressiveCurrentCheckpoint == 4 && !achievements.achProgressive4) {
							settings.progressiveCurrentCheckpoint = 0;
						}
						/*initLoadGame = true;
						showGameModeSelection = false;*/
					}
					if (GUI.Button (new Rect ((Screen.width + 200)/2, 60, 195, 300), "RANDOMIZED", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleRandomizedPlay")))
					{
						settings.modifierRandomizer = true;
						settings.modifierProgressive = false;
						initLoadGame = true;
						showGameModeSelection = false;
					}

					if (backButtonPositionChangeThreshold == 0)
					{
						if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) 
						{
							showGameModeSelection = false;
							pickGameTransDown = false;
							pickGameTransUp = true;
						}
					}
					else
					{
						if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) 
						{
							showGameModeSelection = false;
							pickGameTransDown = false;
							pickGameTransUp = true;
						}
					}
				}
			}
			else if (showExtras && !showLeaderboardsResetAreYouSure)
			{
				GUI.Box(new Rect(Screen.width/2 - 80, 35, 160, 40),"");
				GUI.color = Color.black;
				GUI.skin = CDGUILargeTextSkin;
				GUI.Label(new Rect(Screen.width/2 - 70, 30, 140, 40),"EXTRAS");
				GUI.skin = CDGUIMenuSkin;
				GUI.color = Color.white;
				//GUI.Box (new Rect ((Screen.width - 600)/2, 50, 600, 325), "");

				if (GUI.Button (new Rect ((Screen.width - 590)/2, 60, 195, 300), "Leaderboards", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleLeaderboards")))
				{
					showLeaderboards = true;
					leaderboards.pullLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);
				}
				if (GUI.Button (new Rect ((Screen.width - 195)/2, 60, 195, 300), "Achievements", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleAchievements")))
				{
					showAchievements = true;
				}
				if (GUI.Button (new Rect ((Screen.width + 200)/2, 60, 195, 300), "Credits", CDGUIMenuSkin.FindStyle("bigMenuButtonStyleCredits")))
				{
					creditsCameraVector = new Vector3 (0, 4, player.transform.position.z + 400);

					//showExtras = false;
					//fatMenuTransUp = false;
					//fatMenuTransDown = true;
					showCredits = true;
				}

				if (backButtonPositionChangeThreshold == 0)
				{
					if (GUI.Button (new Rect (Screen.width - 225, Screen.height - 100, 200, 60), "Back")) 
					{
						showExtras = false;
						gameCamera.transform.eulerAngles = new Vector3(325, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
						fatMenuTransUp = false;
						fatMenuTransDown = true;
					}
				}
				else
				{
					if (GUI.Button (new Rect (Screen.width - 225, 380, 200, 60), "Back")) 
					{
						showExtras = false;
						gameCamera.transform.eulerAngles = new Vector3(325, gameCamera.transform.eulerAngles.y, gameCamera.transform.eulerAngles.z);
						fatMenuTransUp = false;
						fatMenuTransDown = true;
					}
				}
				
			}

			GUI.color = GUIColor;
			
			if (showStartup == true && gameCameraScreenOverlay.intensity >= 0)
			{
				GUI.skin = CDGUILargeTextSkin;
				if (!Application.isMobilePlatform) {
					GUI.Label (new Rect (Screen.width / 2 - (350 / 2), Screen.height / 2 + 100, 350, 40), "Press any key to continue");
				} else {
					GUI.Label (new Rect(Screen.width/2 - (350/2), Screen.height/2 + 100, 350, 40), "Tap to continue");
				}

				GUI.skin = CDGUIMenuSkin;
			}

			if (!settings.showSettingsResetScreen && !leaderboards.showLeaderboardsError && !showLeaderboards && showCredits == false && showOptions == false && showModifiers == false && showStartup == false && initLoadGame == false && initExitGame == false && showExtras == false && showGameModeSelection == false && showAchievements == false) 
			{
				GUI.color = GUIColor;
				GUI.Label (new Rect (10, Screen.height - 30, 100, 20), gameVersion, CDGUIMenuSkin.FindStyle("menuBottomLabels"));
				GUI.Label (new Rect (Screen.width - 250, Screen.height - 30, 250, 25), "Created by Ethan 'the Drake' Casey", CDGUIMenuSkin.FindStyle("menuBottomLabels"));
			}

			if (GUI.tooltip != null && GUI.tooltip != "" && Application.platform != RuntimePlatform.Android)
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
