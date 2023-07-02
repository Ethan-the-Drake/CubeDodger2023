using UnityEngine;
using System.Collections;

public class settingsScript : MonoBehaviour {

	public string gameVersion;

	public bool screenshotMode = true;

	string documentsDataPath = "Documents/My Games/Cube Dodger";
	string settingsFileName = "settings.cdtf";
	string settings2_1FileName = "settings2-1.cdtf";

	public string SaveFilePath;

	AchievementsScript achievements;

	System.IO.StreamReader settingsStreamReader;
	string settingsFileContents;
	string[] settingsLines;
	int settingsLineNumber;
	int tryParseDummyVarInt;
	bool tryParseDummyVarBool;
	float tryParseDummyVarFloat;
	int settingsFileNumberOfLines = 121;
	int settings2_1NumberOfLines = 4;

	string[] defaultSettings;
	string[] customSettings;
	string settingsFileLine0String = "This file is written while the game is running to save the player's graphics, audio, and modifiers. Please do not alter this file directly, or you could lose your data.";

	//public Texture2D cursorTex;

	public bool showSettingsResetScreen;

	public bool optionsFullscreen;
	public int optionsPresetQuality;
	public string presetQualityString;

	public int optionsAntiAliasing;
	public float optionsShadowDistance;
	public bool optionsDepthOfField;

	//modifiers------------------------------

	public bool modifierRandomizer;
	public float modifierSpeedMultiplier;

	public bool modifierDrunkDriver;
	public int modifierDrunkDriverCamAnimSwitch;
	public float modifierDrunkDriverCamAnimAmount;
	public bool modifierSeizure;
	public int modifierCubeType; //0 = cubes, 1 = balls, 2 = mixed
	public string modifierCubeTypeString;

	public int modifierCameraType; //0 = static, 1 = upside down, 2 = spinning
	public string modifierCameraTypeString;

	public bool modifierAirdrop;
	public bool modifierRandomVelocity;
	//public bool  modifierUpsideDown;
	
	public bool modifierFarsighted;
	public bool modifierNearsighted;

	//1.2.0-----------------------
	public bool modifierProgressive;
	public bool ProgressiveFinaleBool;

    public bool modifierPowerups;
	//----------------------------

	//---------------------------------------

	public float scoreMultiplier;
	//audio options//------------------------

	public bool optionsMenuADropBadInfluencesEnabled;
	public bool optionsMenuADropErrorEnabled;
	public bool optionsMenuADropFightOrDieEnabled;
	public bool optionsMenuArsFarewellTheInnocentEnabled;
	public bool optionsMenuArsSamaritanEnabled;
	public bool optionsMenuBrokeAtTheCountEnabled;
	public bool optionsMenuBrokeBuildingTheSunEnabled;
	public bool optionsMenuBrokeBlownOutEnabled;
	public bool optionsMenuBrokeCalmTheFuckDownEnabled;
	public bool optionsMenuBrokeCaughtInTheBeatEnabled;
	public bool optionsMenuBrokeFuckItEnabled;
	public bool optionsMenuBrokeHellaEnabled;
	public bool optionsMenuBrokeHighSchoolSnapsEnabled;
	public bool optionsMenuBrokeLikeSwimmingEnabled;
	public bool optionsMenuBrokeLivingInReverseEnabled;
	public bool optionsMenuBrokeLuminousEnabled;
	public bool optionsMenuBrokeMellsParadeEnabled;
	public bool optionsMenuBrokeSomethingElatedEnabled;
	public bool optionsMenuBrokeTheGreatEnabled;
	public bool optionsMenuBrokeQuitBitchingEnabled;
	public bool optionsMenuChrisAnotherVersionOfYouEnabled;
	public bool optionsMenuChrisDividerEnabled;
	public bool optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled;
	public bool optionsMenuChrisThe49thStreetGalleriaEnabled;
	public bool optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled;
	public bool optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled;
	public bool optionsMenuDSMILEZInspirationEnabled;
	public bool optionsMenuDSMILEZLetYourBodyMoveEnabled;
	public bool optionsMenuDSMILEZLostInTheMusicEnabled;
	public bool optionsMenuDecktonicNightDriveEnabled;
	public bool optionsMenuDecktonicStarsEnabled;
	public bool optionsMenuDecktonicWatchYourDubstepEnabled;
	public bool optionsMenuDecktonicActIVEnabled;
	public bool optionsMenuDecktonicBassJamEnabled;
	public bool optionsMenuGravityLittleThingsEnabled;
	public bool optionsMenuGravityMicroscopeEnabled;
	public bool optionsMenuGravityOldHabitsEnabled;
	public bool optionsMenuGravityRadioactiveBoyEnabled;
	public bool optionsMenuGravityTrainTracksEnabled;
	public bool optionsMenuKaiEngelLowHorizonEnabled;
	public bool optionsMenuKaiEngelNothingEnabled;
	public bool optionsMenuKaiEngelSomethingEnabled;
	public bool optionsMenuKaiEngelWakeUpEnabled;
	public bool optionsMenuPierloBarbarianEnabled;
	//public bool optionsMenuPossimisteStarCaesarEnabled;
	public bool optionsMenuParijat4thNightEnabled;
	public bool optionsMenuRevolutionVoidHowExcitingEnabled;
	public bool optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled;
	public bool optionsMenuSergeyLabyrinthEnabled;
	public bool optionsMenuSergeyNowYouAreHereEnabled;
	public bool optionsMenuToursEnthusiastEnabled;
	public bool optionsGameADropBadInfluencesEnabled;
	public bool optionsGameADropErrorEnabled;
	public bool optionsGameADropFightOrDieEnabled;
	public bool optionsGameArsFarewellTheInnocentEnabled;
	public bool optionsGameArsSamaritanEnabled;
	public bool optionsGameBrokeAtTheCountEnabled;
	public bool optionsGameBrokeBuildingTheSunEnabled;
	public bool optionsGameBrokeBlownOutEnabled;
	public bool optionsGameBrokeCalmTheFuckDownEnabled;
	public bool optionsGameBrokeCaughtInTheBeatEnabled;
	public bool optionsGameBrokeFuckItEnabled;
	public bool optionsGameBrokeHellaEnabled;
	public bool optionsGameBrokeHighSchoolSnapsEnabled;
	public bool optionsGameBrokeLikeSwimmingEnabled;
	public bool optionsGameBrokeLivingInReverseEnabled;
	public bool optionsGameBrokeLuminousEnabled;
	public bool optionsGameBrokeMellsParadeEnabled;
	public bool optionsGameBrokeSomethingElatedEnabled;
	public bool optionsGameBrokeTheGreatEnabled;
	public bool optionsGameBrokeQuitBitchingEnabled;
	public bool optionsGameChrisAnotherVersionOfYouEnabled;
	public bool optionsGameChrisDividerEnabled;
	public bool optionsGameChrisEverybodysGotProblemsThatArentMineEnabled;
	public bool optionsGameChrisThe49thStreetGalleriaEnabled;
	public bool optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled;
	public bool optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled;
	public bool optionsGameDSMILEZInspirationEnabled;
	public bool optionsGameDSMILEZLetYourBodyMoveEnabled;
	public bool optionsGameDSMILEZLostInTheMusicEnabled;
	public bool optionsGameDecktonicNightDriveEnabled;
	public bool optionsGameDecktonicStarsEnabled;
	public bool optionsGameDecktonicWatchYourDubstepEnabled;
	public bool optionsGameDecktonicActIVEnabled;
	public bool optionsGameDecktonicBassJamEnabled;
	public bool optionsGameGravityLittleThingsEnabled;
	public bool optionsGameGravityMicroscopeEnabled;
	public bool optionsGameGravityOldHabitsEnabled;
	public bool optionsGameGravityRadioactiveBoyEnabled;
	public bool optionsGameGravityTrainTracksEnabled;
	public bool optionsGameKaiEngelLowHorizonEnabled;
	public bool optionsGameKaiEngelNothingEnabled;
	public bool optionsGameKaiEngelSomethingEnabled;
	public bool optionsGameKaiEngelWakeUpEnabled;
	public bool optionsGamePierloBarbarianEnabled;
	//public bool optionsGamePossimisteStarCaesarEnabled;
	public bool optionsGameParijat4thNightEnabled;
	public bool optionsGameRevolutionVoidHowExcitingEnabled;
	public bool optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled;
	public bool optionsGameSergeyLabyrinthEnabled;
	public bool optionsGameSergeyNowYouAreHereEnabled;
	public bool optionsGameToursEnthusiastEnabled;
	//---------------------------------------

	/*public bool hasUnlockedErrorVelocityNotDefined;
	public bool hasUnlockedFarewellTheInnocent;
	public bool hasUnlockedSamaritan;
	public bool hasUnlockedLittleThings;*/

	public float difficulty = 1;
	public string difficultyString = "Normal";

	public bool doMenuIntro = true;

	public GameObject gameCamera;

	public bool showDebugInfo = true;
	public bool enableDevCheats = false;
	public int cubeDrawDistance;
	public bool useAndroidLighting;
	public bool simulateMobileOptimization;

	public float progressiveContinuousScore;

	//------2.1.0 New Vars
	public int progressiveCurrentCheckpoint;

	public bool isNewVerison = true;
	public bool showControlsTutorial = true;
	public bool progressiveCheckpoints;
	public int mobileControlScheme; //0 = Accelerometer, 1 = Buttons

	//-------3.0 New Vars
	public 

	// Use this for initialization
	void Awake () 
	{
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			SaveFilePath = "C:/Users/" + System.Environment.UserName + "/" + documentsDataPath;
			Debug.Log (SaveFilePath);
			Application.targetFrameRate = 240;
			useAndroidLighting = false;
		} else if (Application.platform == RuntimePlatform.Android) {
			SaveFilePath = Application.persistentDataPath;
			Debug.Log (SaveFilePath);
			Application.targetFrameRate = 30;
			useAndroidLighting = true;
		}

		if (GameObject.Find ("settingsHolder").GetComponent<settingsScript>().doMenuIntro == false && doMenuIntro == true)
		{
			try {
			GameObject.Find ("menuHandler").GetComponent<menuHandlerScript>().settingsHolder = (gameObject);
			}
			catch {
			}
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);

		gameCamera = GameObject.Find ("cameraREF");

		checkFirstTimeStartup ();
		checkProgressiveFinale ();
		getSettingsFromFile ();

		achievements = gameObject.GetComponent<AchievementsScript> ();
		//achievements.checkSaveData ();

		checkSettings (true);
		checkGraphics ();

		if (!achievements.unlockedErrorSong) {
			optionsMenuADropErrorEnabled = false;
			optionsGameADropErrorEnabled = false;
		}
		if (!achievements.unlockedFarewellTheInnocent) {
			optionsMenuArsFarewellTheInnocentEnabled = false;
			optionsGameArsFarewellTheInnocentEnabled = false;
		}
		if (!achievements.unlockedSamaritan) {
			optionsMenuArsSamaritanEnabled = false;
			optionsGameArsSamaritanEnabled = false;
		}
		if (!achievements.unlockedLittleThings) {
			optionsMenuGravityLittleThingsEnabled = false;
			optionsGameGravityLittleThingsEnabled = false;
		}

		//updateGraphicsPreset (optionsPresetQuality);
		//Cursor.SetCursor (cursorTex, Vector2.zero, CursorMode.ForceSoftware);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!achievements.unlockedErrorSong) {
			optionsMenuADropErrorEnabled = false;
			optionsGameADropErrorEnabled = false;
		}
		if (!achievements.unlockedFarewellTheInnocent) {
			optionsMenuArsFarewellTheInnocentEnabled = false;
			optionsGameArsFarewellTheInnocentEnabled = false;
		}
		if (!achievements.unlockedSamaritan) {
			optionsMenuArsSamaritanEnabled = false;
			optionsGameArsSamaritanEnabled = false;
		}
		if (!achievements.unlockedLittleThings) {
			optionsMenuGravityLittleThingsEnabled = false;
			optionsGameGravityLittleThingsEnabled = false;
		}

		if (Application.loadedLevelName == "menu")
		{
			checkSettings();
			checkGraphics();
			if (GameObject.Find ("menuHandler").GetComponent<menuHandlerScript>().showModifiers)
			{
				checkScoreMultiplier();
			}
		}


		if (gameCamera == null && (Application.loadedLevelName == "menu" || Application.loadedLevelName == "game"))
		{
			gameCamera = GameObject.Find ("cameraREF");
		}
	}

	public void checkScoreMultiplier()
	{
		scoreMultiplier = 1;
		if (modifierAirdrop)
		{
			scoreMultiplier = scoreMultiplier + 1;
		}
		
		if (modifierCubeType == 1)
		{
			scoreMultiplier = scoreMultiplier + 1;
		}
		else if (modifierCubeType == 2)
		{
			scoreMultiplier = scoreMultiplier + 0.5f;
		}

		if (modifierCameraType == 1)
		{
			scoreMultiplier = scoreMultiplier + 0.5f;
		}
		else if (modifierCameraType == 2)
		{
			scoreMultiplier = scoreMultiplier + 1;
		}

		if (modifierDrunkDriver)
		{
			scoreMultiplier = scoreMultiplier + 1;
		}
		if (modifierFarsighted)
		{
			scoreMultiplier = scoreMultiplier + 1;
		}
		if (modifierNearsighted)
		{
			scoreMultiplier = scoreMultiplier + 1;
		}
		if (modifierRandomVelocity)
		{
			scoreMultiplier = scoreMultiplier + 1;
		}


		if (difficulty == 0)
		{
			scoreMultiplier = scoreMultiplier * .5f;
		}
		else if (difficulty == 1)
		{
			scoreMultiplier = scoreMultiplier * 1;
		}
		else if (difficulty == 2)
		{
			scoreMultiplier = scoreMultiplier * 1.5f;
		}
		else if (difficulty == 3)
		{
			scoreMultiplier = scoreMultiplier * 2;
		}

		if (modifierSpeedMultiplier <= 1)
		{
			scoreMultiplier = scoreMultiplier * modifierSpeedMultiplier;
		}
		else if (modifierSpeedMultiplier == 1.25f)
		{
			scoreMultiplier = scoreMultiplier * (1.2f);
		}
		else if (modifierSpeedMultiplier == 1.5f)
		{
			scoreMultiplier = scoreMultiplier * (1.3f);
		}
		else if (modifierSpeedMultiplier == 1.75f)
		{
			scoreMultiplier = scoreMultiplier * (1.4f);
		}
		else if (modifierSpeedMultiplier == 2)
		{
			scoreMultiplier = scoreMultiplier * (1.5f);
		}

	}

	void checkFirstTimeStartup()
	{
		try
		{
			print (SaveFilePath + "/firstBool.cdtf");
			System.IO.File.ReadAllText(SaveFilePath + "/firstBool.cdtf");
		}
		catch (System.IO.FileNotFoundException)
		{
			print ("First Time Setup!");
			GameObject.Find ("menuHandler").GetComponent<menuHandlerScript>().showFirstTimeMessage = true;
			System.IO.Directory.CreateDirectory(SaveFilePath);
			System.IO.File.Create(SaveFilePath + "/firstBool.cdtf");
		}
		catch (System.IO.IsolatedStorage.IsolatedStorageException)
		{
			print ("First Time Setup! - Isolated Storage");
			GameObject.Find ("menuHandler").GetComponent<menuHandlerScript>().showFirstTimeMessage = true;	
			System.IO.Directory.CreateDirectory(SaveFilePath);
			System.IO.File.Create(SaveFilePath + "/firstBool.cdtf");
		}
		catch (System.IO.DirectoryNotFoundException)
		{
			print ("First Time Setup! - Directory Not Found");
			GameObject.Find ("menuHandler").GetComponent<menuHandlerScript>().showFirstTimeMessage = true;	
			System.IO.Directory.CreateDirectory(SaveFilePath);
			System.IO.File.Create(SaveFilePath + "/firstBool.cdtf");
		}
	}
	//1.2.0---------------------------
	void checkProgressiveFinale()
	{
		try
		{
			print (SaveFilePath + "/PF.cdtf");
			System.IO.File.ReadAllText(SaveFilePath + "/PF.cdtf");
			ProgressiveFinaleBool = true;
		}
		catch (System.IO.FileNotFoundException)
		{
			print ("No PF File.");
		}
		catch (System.IO.IsolatedStorage.IsolatedStorageException)
		{
			print ("No PF File. - Isolated Storage");
		}
		catch (System.IO.DirectoryNotFoundException)
		{
			print ("No PF File. - Directory Not Found");
		}
	}

	//public void writeProgressiveFinale() {
		//System.IO.File.WriteAllText (SaveFilePath + "/PF.cdtf", "true");
	//}
	//---------------------------------
	void getSettingsFromFile() //Gets and Sets settings from a text file.
	{
		//Read information from a file, change the settings based on what the user previously selected.
		
		try
		{
			settingsStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + settingsFileName);
			settingsFileContents = settingsStreamReader.ReadToEnd ();
		}
		catch (System.IO.FileNotFoundException)
		{
			print ("No Settings File Found, First Launch or Corrupted File? Creating a new file.");
			resetSettings();

			settingsStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + settingsFileName);
			settingsFileContents = settingsStreamReader.ReadToEnd ();

		}
		catch (System.IO.IsolatedStorage.IsolatedStorageException)
		{
			print ("No Settings File Found, First Launch or Corrupted File? Creating a new file.");
			resetSettings();
			
			settingsStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + settingsFileName);
			settingsFileContents = settingsStreamReader.ReadToEnd ();
		}
		settingsLines = settingsFileContents.Split ("\n" [0]);
		settingsLineNumber = 0;
		foreach (string line in settingsLines)
		{
			if (settingsLineNumber != 0 && settingsLineNumber <= 2)
			{

				if (int.TryParse(line, out tryParseDummyVarInt) == false)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Int. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Integer. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 6)
			{
				
				if (int.TryParse(line, out tryParseDummyVarInt) == false || int.Parse(line) % 5 == 1)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Int Divisible by 5. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Integer Divisible by 5. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 3 || (settingsLineNumber >= 7 && settingsLineNumber <= 87) || (settingsLineNumber == 90) || (settingsLineNumber == 91) || (settingsLineNumber >= 93 && settingsLineNumber < 95) || (settingsLineNumber > 95 && settingsLineNumber <= 120))
			{
				if (bool.TryParse(line, out tryParseDummyVarBool)  == false)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Bool. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Boolean. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 4)
			{
				if (int.TryParse(line, out tryParseDummyVarInt)  == false || int.Parse(line) >= 6 || int.Parse (line) <= -1)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Int between 0 and 5. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Integer between 0 and 5. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 5)
			{
				if (int.TryParse(line, out tryParseDummyVarInt) == false || int.Parse(line)%2 == 1)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Even Int. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Even Integer. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 88 || settingsLineNumber == 95)
			{
				if (int.TryParse(line, out tryParseDummyVarInt)  == false || int.Parse(line) >= 4 || int.Parse (line) <= -1)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Int between 0 and 3. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Integer between 0 and 3. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 89)
			{
				if (float.TryParse(line, out tryParseDummyVarFloat)  == false || float.Parse(line) > 2 || float.Parse(line) < 0.5)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Int between 0 and 2. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Float between 0.5 and 2. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 92)
			{
				if (int.TryParse(line, out tryParseDummyVarInt)  == false || int.Parse(line) >= 3 || int.Parse(line) <= -1)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Int between 0 and 2. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Integer between 0 and 2. Deleting and resetting settings file.");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber != 0 && settingsLineNumber < 120)
			{
				Debug.Log ("Unknown error at line " + settingsLineNumber + ". Here's the line: " + line);
			}

			if (settingsLineNumber < settingsFileNumberOfLines)
			{
				try
				{
					print (settingsLines[settingsLineNumber + 1]);
				}
				catch (System.IndexOutOfRangeException)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", File too small. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "File too small. Deleting and resetting settings file. ");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			
			settingsLineNumber++;
		}
		//Set the  
		//Resolution
		Screen.SetResolution (int.Parse (settingsLines [1]), int.Parse (settingsLines [2]), bool.Parse (settingsLines [3]));
		//Graphics
		optionsPresetQuality = int.Parse (settingsLines [4]);
		optionsAntiAliasing = int.Parse (settingsLines [5]);
		optionsShadowDistance = int.Parse (settingsLines [6]);
		optionsDepthOfField = bool.Parse (settingsLines [7]);
		//Audio
		optionsMenuBrokeAtTheCountEnabled = bool.Parse (settingsLines [8]);
		optionsGameBrokeAtTheCountEnabled = bool.Parse (settingsLines [9]);
		optionsMenuBrokeBuildingTheSunEnabled = bool.Parse (settingsLines [10]);
		optionsGameBrokeBuildingTheSunEnabled = bool.Parse (settingsLines [11]);
		optionsMenuBrokeBlownOutEnabled = bool.Parse (settingsLines [12]);
		optionsGameBrokeBlownOutEnabled = bool.Parse (settingsLines [13]);
		optionsMenuBrokeFuckItEnabled = bool.Parse (settingsLines [14]);
		optionsGameBrokeFuckItEnabled = bool.Parse (settingsLines [15]);
		optionsMenuBrokeCalmTheFuckDownEnabled = bool.Parse (settingsLines [16]);
		optionsGameBrokeCalmTheFuckDownEnabled = bool.Parse (settingsLines [17]);
		optionsMenuBrokeCaughtInTheBeatEnabled = bool.Parse (settingsLines [18]);
		optionsGameBrokeCaughtInTheBeatEnabled = bool.Parse (settingsLines [19]);
		optionsMenuBrokeHellaEnabled = bool.Parse (settingsLines [20]);
		optionsGameBrokeHellaEnabled = bool.Parse (settingsLines [21]);
		optionsMenuBrokeHighSchoolSnapsEnabled = bool.Parse (settingsLines [22]);
		optionsGameBrokeHighSchoolSnapsEnabled = bool.Parse (settingsLines [23]);
		optionsMenuBrokeLivingInReverseEnabled = bool.Parse (settingsLines [24]);
		optionsGameBrokeLivingInReverseEnabled = bool.Parse (settingsLines [25]);
		optionsMenuBrokeMellsParadeEnabled = bool.Parse (settingsLines [26]);
		optionsGameBrokeMellsParadeEnabled = bool.Parse (settingsLines [27]);
		optionsMenuBrokeLikeSwimmingEnabled = bool.Parse (settingsLines [28]);
		optionsGameBrokeLikeSwimmingEnabled = bool.Parse (settingsLines [29]);
		optionsMenuBrokeLuminousEnabled = bool.Parse (settingsLines [30]);
		optionsGameBrokeLuminousEnabled = bool.Parse (settingsLines [31]);
		optionsMenuBrokeSomethingElatedEnabled = bool.Parse (settingsLines [32]);
		optionsGameBrokeSomethingElatedEnabled = bool.Parse (settingsLines [33]);
		optionsMenuBrokeTheGreatEnabled = bool.Parse (settingsLines [34]);
		optionsGameBrokeTheGreatEnabled = bool.Parse (settingsLines [35]);
		optionsMenuBrokeQuitBitchingEnabled = bool.Parse (settingsLines [36]);
		optionsGameBrokeQuitBitchingEnabled = bool.Parse (settingsLines [37]);
		optionsMenuChrisAnotherVersionOfYouEnabled = bool.Parse (settingsLines [38]);
		optionsGameChrisAnotherVersionOfYouEnabled = bool.Parse (settingsLines [39]);
		optionsMenuChrisDividerEnabled = bool.Parse (settingsLines [40]);
		optionsGameChrisDividerEnabled = bool.Parse (settingsLines [41]);
		optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled = bool.Parse (settingsLines [42]);
		optionsGameChrisEverybodysGotProblemsThatArentMineEnabled = bool.Parse (settingsLines [43]);
		optionsMenuChrisThe49thStreetGalleriaEnabled = bool.Parse (settingsLines [44]);
		optionsGameChrisThe49thStreetGalleriaEnabled = bool.Parse (settingsLines [45]);
		optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = bool.Parse (settingsLines [46]);
		optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled = bool.Parse (settingsLines [47]);
		optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled = bool.Parse (settingsLines [48]);
		optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled = bool.Parse (settingsLines [49]);
		optionsMenuDSMILEZInspirationEnabled = bool.Parse (settingsLines [50]);
		optionsGameDSMILEZInspirationEnabled = bool.Parse (settingsLines [51]);
		optionsMenuDSMILEZLetYourBodyMoveEnabled = bool.Parse (settingsLines [52]);
		optionsGameDSMILEZLetYourBodyMoveEnabled = bool.Parse (settingsLines [53]);
		optionsMenuDSMILEZLostInTheMusicEnabled = bool.Parse (settingsLines [54]);
		optionsGameDSMILEZLostInTheMusicEnabled = bool.Parse (settingsLines [55]);
		optionsMenuDecktonicNightDriveEnabled = bool.Parse (settingsLines [56]);
		optionsGameDecktonicNightDriveEnabled = bool.Parse (settingsLines [57]);
		optionsMenuDecktonicStarsEnabled = bool.Parse (settingsLines [58]);
		optionsGameDecktonicStarsEnabled = bool.Parse (settingsLines [59]);
		optionsMenuDecktonicWatchYourDubstepEnabled = bool.Parse (settingsLines [60]);
		optionsGameDecktonicWatchYourDubstepEnabled = bool.Parse (settingsLines [61]);
		optionsMenuDecktonicActIVEnabled = bool.Parse (settingsLines [62]);
		optionsGameDecktonicActIVEnabled = bool.Parse (settingsLines [63]);
		optionsMenuDecktonicBassJamEnabled = bool.Parse (settingsLines [64]);
		optionsGameDecktonicBassJamEnabled = bool.Parse (settingsLines [65]);
		optionsMenuKaiEngelLowHorizonEnabled = bool.Parse (settingsLines [66]);
		optionsGameKaiEngelLowHorizonEnabled = bool.Parse (settingsLines [67]);
		optionsMenuKaiEngelNothingEnabled = bool.Parse (settingsLines [68]);
		optionsGameKaiEngelNothingEnabled = bool.Parse (settingsLines [69]);
		optionsMenuKaiEngelSomethingEnabled = bool.Parse (settingsLines [70]);
		optionsGameKaiEngelSomethingEnabled = bool.Parse (settingsLines [71]);
		optionsMenuKaiEngelWakeUpEnabled = bool.Parse (settingsLines [72]);
		optionsGameKaiEngelWakeUpEnabled = bool.Parse (settingsLines [73]);
		optionsMenuPierloBarbarianEnabled = bool.Parse (settingsLines [74]);
		optionsGamePierloBarbarianEnabled = bool.Parse (settingsLines [75]);
		optionsMenuParijat4thNightEnabled = bool.Parse (settingsLines [76]);
		optionsGameParijat4thNightEnabled = bool.Parse (settingsLines [77]);
		optionsMenuRevolutionVoidHowExcitingEnabled = bool.Parse (settingsLines [78]);
		optionsGameRevolutionVoidHowExcitingEnabled = bool.Parse (settingsLines [79]);
		optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled = bool.Parse (settingsLines [80]);
		optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled = bool.Parse (settingsLines [81]);
		optionsMenuSergeyLabyrinthEnabled = bool.Parse (settingsLines [82]);
		optionsGameSergeyLabyrinthEnabled = bool.Parse (settingsLines [83]);
		optionsMenuSergeyNowYouAreHereEnabled = bool.Parse (settingsLines [84]);
		optionsGameSergeyNowYouAreHereEnabled = bool.Parse (settingsLines [85]);
		optionsMenuToursEnthusiastEnabled = bool.Parse (settingsLines [86]);
		optionsGameToursEnthusiastEnabled = bool.Parse (settingsLines [87]);

		difficulty = int.Parse (settingsLines [88]);
		modifierSpeedMultiplier = float.Parse (settingsLines [89]);
		modifierRandomizer = bool.Parse (settingsLines [90]);
		modifierAirdrop = bool.Parse (settingsLines [91]);
		modifierCubeType = int.Parse (settingsLines [92]);
		modifierDrunkDriver = bool.Parse (settingsLines [93]);
		modifierFarsighted = bool.Parse (settingsLines [94]);
		modifierCameraType = int.Parse (settingsLines [95]);
		modifierNearsighted = bool.Parse (settingsLines [96]);
		modifierRandomVelocity = bool.Parse (settingsLines [97]);
		modifierSeizure = bool.Parse (settingsLines [98]);
		//1.2.0 new settings------------------------------------
		modifierProgressive = bool.Parse (settingsLines [99]);
        	modifierPowerups = bool.Parse (settingsLines[100]);
		//------------------------------------------------------

		//new 2.0 songs
		optionsMenuADropBadInfluencesEnabled = bool.Parse (settingsLines [101]);
		optionsGameADropBadInfluencesEnabled = bool.Parse (settingsLines [102]);
		optionsMenuADropErrorEnabled = bool.Parse (settingsLines [103]);
		optionsGameADropErrorEnabled = bool.Parse (settingsLines [104]);
		optionsMenuADropFightOrDieEnabled = bool.Parse (settingsLines [105]);
		optionsGameADropFightOrDieEnabled = bool.Parse (settingsLines [106]);
		optionsMenuArsFarewellTheInnocentEnabled = bool.Parse (settingsLines [107]);
		optionsGameArsFarewellTheInnocentEnabled = bool.Parse (settingsLines [108]);
		optionsMenuArsSamaritanEnabled = bool.Parse (settingsLines [109]);
		optionsGameArsSamaritanEnabled = bool.Parse (settingsLines [110]);
		optionsMenuGravityLittleThingsEnabled = bool.Parse (settingsLines [111]);
		optionsGameGravityLittleThingsEnabled = bool.Parse (settingsLines [112]);
		optionsMenuGravityMicroscopeEnabled = bool.Parse (settingsLines [113]);
		optionsGameGravityMicroscopeEnabled = bool.Parse (settingsLines [114]);
		optionsMenuGravityOldHabitsEnabled = bool.Parse (settingsLines [115]);
		optionsGameGravityOldHabitsEnabled = bool.Parse (settingsLines [116]);
		optionsMenuGravityRadioactiveBoyEnabled = bool.Parse (settingsLines [117]);
		optionsGameGravityRadioactiveBoyEnabled = bool.Parse (settingsLines [118]);
		optionsMenuGravityTrainTracksEnabled = bool.Parse (settingsLines [119]);
		optionsGameGravityTrainTracksEnabled = bool.Parse (settingsLines [120]);

		//Keep at bottom of function.
		settingsStreamReader.Close ();
		//2.1 SETTINGS----------------------------------------------
		try
		{
			settingsStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + settings2_1FileName);
			settingsFileContents = settingsStreamReader.ReadToEnd ();
		}
		catch (System.IO.FileNotFoundException)
		{
			print ("No 2.1 Settings File Found, First Launch or Corrupted File? Creating a new file.");
			reset2_1Settings();

			settingsStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + settings2_1FileName);
			settingsFileContents = settingsStreamReader.ReadToEnd ();

		}
		catch (System.IO.IsolatedStorage.IsolatedStorageException)
		{
			print ("No 2.1 Settings File Found, First Launch or Corrupted File? Creating a new file.");
			reset2_1Settings();

			settingsStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + settings2_1FileName);
			settingsFileContents = settingsStreamReader.ReadToEnd ();
		}
		settingsLines = settingsFileContents.Split ("\n" [0]);
		settingsLineNumber = 0;
		foreach (string line in settingsLines)
		{
			if (settingsLineNumber != 0 && settingsLineNumber <= 3)
			{

				if (bool.TryParse(line, out tryParseDummyVarBool) == false)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", Expected Bool. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settings2_1FileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Bool. Deleting and resetting settings 2.1 file.");
					//showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber == 4)
			{

				if (int.TryParse(line, out tryParseDummyVarInt) == false)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings 2.1 file. Returned " + line + ", Expected Int. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "Expected Bool. Deleting and resetting settings file.");
					//showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}
			else if (settingsLineNumber != 0 && settingsLineNumber < 5)
			{
				Debug.Log ("Unknown error at line " + settingsLineNumber + ". Here's the line: " + line);
			}

			if (settingsLineNumber < settings2_1NumberOfLines)
			{
				try
				{
					print (settingsLines[settingsLineNumber + 1]);
				}
				catch (System.IndexOutOfRangeException)
				{
					Debug.Log ("line " + settingsLineNumber + " does not conform with expected format for Settings file. Returned " + line + ", File too small. Deleting and resetting settings file.");
					settingsStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + settingsFileName);
					createSettingsErrorLog(line, settingsLineNumber, "File too small. Deleting and resetting settings file. ");
					showSettingsResetScreen = true;
					getSettingsFromFile();
					break;
				}
			}

			settingsLineNumber++;
		}
		isNewVerison = bool.Parse (settingsLines [1]);
		showControlsTutorial = bool.Parse (settingsLines [2]);
		progressiveCheckpoints = bool.Parse (settingsLines [3]);
		mobileControlScheme = int.Parse (settingsLines [4]);

		//Keep at bottom of function.
		settingsStreamReader.Close ();
	}

	public void saveSettings()
	{
		try
		{
			Debug.Log ("Saving...");
			customSettings = new string[]
			{settingsFileLine0String, 
				//Resolution
				Screen.width.ToString(),//Res Width
				Screen.height.ToString(), //Res Height
				optionsFullscreen.ToString(), //Fullscreen
				//Graphics
				optionsPresetQuality.ToString(), //Preset
				optionsAntiAliasing.ToString(), //Anti-Aliasing
				optionsShadowDistance.ToString(), //Shadow Distance
				optionsDepthOfField.ToString(), //Depth of Field
				//Audio													Game
				optionsMenuBrokeAtTheCountEnabled.ToString(),
				optionsGameBrokeAtTheCountEnabled.ToString(),
				optionsMenuBrokeBuildingTheSunEnabled.ToString(),
				optionsGameBrokeBuildingTheSunEnabled.ToString(),
				optionsMenuBrokeBlownOutEnabled.ToString(),
				optionsGameBrokeBlownOutEnabled.ToString(),
				optionsMenuBrokeFuckItEnabled.ToString(),
				optionsGameBrokeFuckItEnabled.ToString(),
				optionsMenuBrokeCalmTheFuckDownEnabled.ToString(),
				optionsGameBrokeCalmTheFuckDownEnabled.ToString(),
				optionsMenuBrokeCaughtInTheBeatEnabled.ToString(),
				optionsGameBrokeCaughtInTheBeatEnabled.ToString(),
				optionsMenuBrokeHellaEnabled.ToString(),
				optionsGameBrokeHellaEnabled.ToString(),
				optionsMenuBrokeHighSchoolSnapsEnabled.ToString(),
				optionsGameBrokeHighSchoolSnapsEnabled.ToString(),
				optionsMenuBrokeLivingInReverseEnabled.ToString(),
				optionsGameBrokeLivingInReverseEnabled.ToString(),
				optionsMenuBrokeMellsParadeEnabled.ToString(),
				optionsGameBrokeMellsParadeEnabled.ToString(),
				optionsMenuBrokeLikeSwimmingEnabled.ToString(),
				optionsGameBrokeLikeSwimmingEnabled.ToString(),
				optionsMenuBrokeLuminousEnabled.ToString(),
				optionsGameBrokeLuminousEnabled.ToString(),
				optionsMenuBrokeSomethingElatedEnabled.ToString(),
				optionsGameBrokeSomethingElatedEnabled.ToString(),
				optionsMenuBrokeTheGreatEnabled.ToString(),
				optionsGameBrokeTheGreatEnabled.ToString(),
				optionsMenuBrokeQuitBitchingEnabled.ToString(),
				optionsGameBrokeQuitBitchingEnabled.ToString(),
				optionsMenuChrisAnotherVersionOfYouEnabled.ToString(),
				optionsGameChrisAnotherVersionOfYouEnabled.ToString(),
				optionsMenuChrisDividerEnabled.ToString(),
				optionsGameChrisDividerEnabled.ToString(),
				optionsMenuChrisEverybodysGotProblemsThatArentMineEnabled.ToString(),
				optionsGameChrisEverybodysGotProblemsThatArentMineEnabled.ToString(),
				optionsMenuChrisThe49thStreetGalleriaEnabled.ToString(),
				optionsGameChrisThe49thStreetGalleriaEnabled.ToString(),
				optionsMenuChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled.ToString(),
				optionsGameChrisTheLifeAndDeathOfACertainKZabriskiePatriarchEnabled.ToString(),
				optionsMenuChrisTheresASpecialPlaceForSomePeopleEnabled.ToString(),
				optionsGameChrisTheresASpecialPlaceForSomePeopleEnabled.ToString(),
				optionsMenuDSMILEZInspirationEnabled.ToString(),
				optionsGameDSMILEZInspirationEnabled.ToString(),
				optionsMenuDSMILEZLetYourBodyMoveEnabled.ToString(),
				optionsGameDSMILEZLetYourBodyMoveEnabled.ToString(),
				optionsMenuDSMILEZLostInTheMusicEnabled.ToString(),
				optionsGameDSMILEZLostInTheMusicEnabled.ToString(),
				optionsMenuDecktonicNightDriveEnabled.ToString(),
				optionsGameDecktonicNightDriveEnabled.ToString(),
				optionsMenuDecktonicStarsEnabled.ToString(),
				optionsGameDecktonicStarsEnabled.ToString(),
				optionsMenuDecktonicWatchYourDubstepEnabled.ToString(),
				optionsGameDecktonicWatchYourDubstepEnabled.ToString(),
				optionsMenuDecktonicActIVEnabled.ToString(),
				optionsGameDecktonicActIVEnabled.ToString(),
				optionsMenuDecktonicBassJamEnabled.ToString(),
				optionsGameDecktonicBassJamEnabled.ToString(),
				optionsMenuKaiEngelLowHorizonEnabled.ToString(),
				optionsGameKaiEngelLowHorizonEnabled.ToString(),
				optionsMenuKaiEngelNothingEnabled.ToString(),
				optionsGameKaiEngelNothingEnabled.ToString(),
				optionsMenuKaiEngelSomethingEnabled.ToString(),
				optionsGameKaiEngelSomethingEnabled.ToString(),
				optionsMenuKaiEngelWakeUpEnabled.ToString(),
				optionsGameKaiEngelWakeUpEnabled.ToString(),
				optionsMenuPierloBarbarianEnabled.ToString(),
				optionsGamePierloBarbarianEnabled.ToString(),
				optionsMenuParijat4thNightEnabled.ToString(),
				optionsGameParijat4thNightEnabled.ToString(),
				optionsMenuRevolutionVoidHowExcitingEnabled.ToString(),
				optionsGameRevolutionVoidHowExcitingEnabled.ToString(),
				optionsMenuRevolutionVoidSomeoneElsesMemoriesEnabled.ToString(),
				optionsGameRevolutionVoidSomeoneElsesMemoriesEnabled.ToString(),
				optionsMenuSergeyLabyrinthEnabled.ToString(),
				optionsGameSergeyLabyrinthEnabled.ToString(),
				optionsMenuSergeyNowYouAreHereEnabled.ToString(),
				optionsGameSergeyNowYouAreHereEnabled.ToString(),
				optionsMenuToursEnthusiastEnabled.ToString(),
				optionsGameToursEnthusiastEnabled.ToString(),											
				//Modifiers
				difficulty.ToString(), //Difficulty
				modifierSpeedMultiplier.ToString(), //Speed Mult
				modifierRandomizer.ToString(), //Randomizer
				modifierAirdrop.ToString(), //Airdrop
				modifierCubeType.ToString(), //Cube Type
				modifierDrunkDriver.ToString(), //Drunk
				modifierFarsighted.ToString(), //Farsighted
				modifierCameraType.ToString(), //Upside Down
				modifierNearsighted.ToString(), //Nearsighted
				modifierRandomVelocity.ToString(), //Random Vel.
				modifierSeizure.ToString(), //Seizure
				//---------1.2.0------
				modifierProgressive.ToString(), //Progressive
               			modifierPowerups.ToString(), //Powerups
				//--------------------
				//2.0
				optionsMenuADropBadInfluencesEnabled.ToString(),
				optionsGameADropBadInfluencesEnabled.ToString(),
				optionsMenuADropErrorEnabled.ToString(),
				optionsGameADropErrorEnabled.ToString(),
				optionsMenuADropFightOrDieEnabled.ToString(),
				optionsGameADropFightOrDieEnabled.ToString(),
				optionsMenuArsFarewellTheInnocentEnabled.ToString(),
				optionsGameArsFarewellTheInnocentEnabled.ToString(),
				optionsMenuArsSamaritanEnabled.ToString(),
				optionsGameArsSamaritanEnabled.ToString(),
				optionsMenuGravityLittleThingsEnabled.ToString(),
				optionsGameGravityLittleThingsEnabled.ToString(),
				optionsMenuGravityMicroscopeEnabled.ToString(),
				optionsGameGravityMicroscopeEnabled.ToString(),
				optionsMenuGravityOldHabitsEnabled.ToString(),
				optionsGameGravityOldHabitsEnabled.ToString(),
				optionsMenuGravityRadioactiveBoyEnabled.ToString(),
				optionsGameGravityRadioactiveBoyEnabled.ToString(),
				optionsMenuGravityTrainTracksEnabled.ToString(),
				optionsGameGravityTrainTracksEnabled.ToString(),

			};
			System.IO.File.WriteAllLines(SaveFilePath + "/" + settingsFileName, customSettings);

			//2.1
			customSettings = new string[]
			{settingsFileLine0String,
				isNewVerison.ToString(), //isNewVerison?
				showControlsTutorial.ToString(), //ShowControlsTutorial?
				progressiveCheckpoints.ToString(), //UseProgressiveCheckpoints
				mobileControlScheme.ToString(), //MobileControlScheme
			};
			System.IO.File.WriteAllLines(SaveFilePath + "/" + settings2_1FileName, customSettings);

		}
		catch (System.IO.DirectoryNotFoundException)
		{
			print ("Error - Directory Not Found. Recreating directory.");
			GameObject.Find ("menuHandler").GetComponent<menuHandlerScript>().showFirstTimeMessage = true;	
			System.IO.Directory.CreateDirectory(SaveFilePath);
			System.IO.File.Create(SaveFilePath + "/settings.cdtf");
			resetSettings();
		}


	}

	void resetSettings()
	{
		if (Application.platform == RuntimePlatform.Android) {
			defaultSettings = new string[]
			{settingsFileLine0String, 
				//Resolution
				1024.ToString(),//Res Width
				576.ToString(), //Res Height
				"true", //Fullscreen
				//Graphics
				"0", //Preset
				"0", //Anti-Aliasing
				"0", //Shadow Distance
				"false", //Depth of Field
				//Audio
				//Menu, Game
				"false", "true", //At The Count
				"false", "true", //Black Lung
				"false", "true", //Breakfast w/ Tiffany
				"false", "true", //Bring Me The Night
				"false", "true", //Calm The Fuck Down
				"false", "true", //Caught In The Beat
				"false", "true", //Hella
				"false", "true", //High School Snaps
				"false", "true", //Living In Reverse
				"true", "true", //Mell's Parade
				"false", "true", //LikeSwimming
				"false", "true", //Over Easy
				"false", "true", //Something Elated
				"false", "true", //The Great
				"false", "true", //Warm Up Suit
				"true", "false", //Another Version...
				"true", "false", //Divider
				"true", "false", //Everybody's Got...
				"true", "false", //The 49th Street...
				"true", "true", //The Life and Death...
				"true", "false", //There's A Special...
				"true", "true", //Inspiration
				"false", "true", //Let Your Body Move
				"false", "true", //Lose In The Music
				"false", "true", //Act IV
				"false", "true", //B@$$ JA|/\|
				"false", "true", //Night Drive
				"false", "true", //Stars
				"false", "true", //Watch Your Dubstep...
				"true", "false", //Low Horizon
				"true", "false", //Nothing
				"true", "false", //Something
				"true", "false", //Wake Up
				"false", "true", //Barbarian
				"true", "true", //Parijat Mishra - 4th Night
				"false", "true", //How Exciting
				"false", "true", //Something Else's...
				"true", "false", //Labyrinth
				"true", "false", //Now You Are...
				"true", "true", //Enthusiast
				//Modifiers
				"1", //Difficulty
				"1", //Speed Mult
				"false", //Randomizer
				"false", //Airdrop
				"0", //Cube Type
				"false", //Drunk
				"false", //Farsighted
				"0", //Camera Type
				"false", //Nearsighted
				"false", //Random Vel.
				"false", //Seizure
				//1.2.0
				"false",
				"false", //powerups
				//-----------2.0
				"false", "true", //A Drop A Day - Bad Influences
				"false", "true", //A Drop A Day - ERROR: Velocity Not Defined
				"false", "true", //A Drop A Day - Fight Or Die
				"true", "false", //Ars Sonor - Farewell The Innocent
				"true", "true", //Ars Sonor - Samaritan
				"true", "true", //Gravity Sound - Little Things
				"true", "true", //Gravity Sound - Microscope
				"true", "true", //Gravity Sound - Old Habits
				"true", "true", //Gravity Sound - Radioactive Boy
				"true", "true", //Gravity Sound - Train Tracks
			};
		} else {
			defaultSettings = new string[]
			{settingsFileLine0String, 
				//Resolution
				Screen.width.ToString(),//Res Width
				Screen.height.ToString(), //Res Height
				"true", //Fullscreen
				//Graphics
				"4", //Preset
				"4", //Anti-Aliasing
				"250", //Shadow Distance
				"true", //Depth of Field
				//Audio
				//Menu, Game
				"false", "true", //At The Count
				"false", "true", //Black Lung
				"false", "true", //Breakfast w/ Tiffany
				"false", "true", //Bring Me The Night
				"false", "true", //Calm The Fuck Down
				"false", "true", //Caught In The Beat
				"false", "true", //Hella
				"false", "true", //High School Snaps
				"false", "true", //Living In Reverse
				"true", "true", //Mell's Parade
				"false", "true", //LikeSwimming
				"false", "true", //Over Easy
				"false", "true", //Something Elated
				"false", "true", //The Great
				"false", "true", //Warm Up Suit
				"true", "false", //Another Version...
				"true", "false", //Divider
				"true", "false", //Everybody's Got...
				"true", "false", //The 49th Street...
				"true", "true", //The Life and Death...
				"true", "false", //There's A Special...
				"true", "true", //Inspiration
				"false", "true", //Let Your Body Move
				"false", "true", //Lose In The Music
				"false", "true", //Act IV
				"false", "true", //B@$$ JA|/\|
				"false", "true", //Night Drive
				"false", "true", //Stars
				"false", "true", //Watch Your Dubstep...
				"true", "false", //Low Horizon
				"true", "false", //Nothing
				"true", "false", //Something
				"true", "false", //Wake Up
				"false", "true", //Barbarian
				"true", "true", //Parijat Mishra - 4th Night
				"false", "true", //How Exciting
				"false", "true", //Something Else's...
				"true", "false", //Labyrinth
				"true", "false", //Now You Are...
				"true", "true", //Enthusiast
				//Modifiers
				"1", //Difficulty
				"1", //Speed Mult
				"false", //Randomizer
				"false", //Airdrop
				"0", //Cube Type
				"false", //Drunk
				"false", //Farsighted
				"0", //Camera Type
				"false", //Nearsighted
				"false", //Random Vel.
				"false", //Seizure
				//1.2.0
				"false",
				"false", //powerups
				//-----------2.0
				"false", "true", //A Drop A Day - Bad Influences
				"false", "true", //A Drop A Day - ERROR: Velocity Not Defined
				"false", "true", //A Drop A Day - Fight Or Die
				"true", "false", //Ars Sonor - Farewell The Innocent
				"true", "true", //Ars Sonor - Samaritan
				"true", "true", //Gravity Sound - Little Things
				"true", "true", //Gravity Sound - Microscope
				"true", "true", //Gravity Sound - Old Habits
				"true", "true", //Gravity Sound - Radioactive Boy
				"true", "true", //Gravity Sound - Train Tracks
			};
		}

		System.IO.File.WriteAllLines(SaveFilePath + "/" + settingsFileName, defaultSettings);
	}

	void reset2_1Settings()
	{
		defaultSettings = new string[]
		{settingsFileLine0String, 
			//Resolution
			"true",//IsNewVersion?
			"true", //Show controls tutorial
			"false", //use progressive checkpoints
			"0", //mobileControlScheme
		};

		System.IO.File.WriteAllLines(SaveFilePath + "/" + settings2_1FileName, defaultSettings);
	}

	void createSettingsErrorLog(string line, int lineNumber, string ErrorMessage)
	{
		System.IO.Directory.CreateDirectory (SaveFilePath + "/ErrorLogs");
		System.IO.File.WriteAllText(SaveFilePath + "/ErrorLogs/SettingsErrorLog.txt", "[" + System.DateTime.Now.ToString() + "] [Game Version: " + gameVersion + "] Error Log. Error at line " + lineNumber + ", '" + line + "', " + ErrorMessage); 

	}

	public void checkGraphics()
	{
		optionsFullscreen = Screen.fullScreen;
		optionsPresetQuality = QualitySettings.GetQualityLevel ();
		updateGraphicsPreset (optionsPresetQuality);
		optionsAntiAliasing = QualitySettings.antiAliasing;
		optionsShadowDistance = QualitySettings.shadowDistance;
		optionsDepthOfField = gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField> ().enabled;
	}

	public void checkSettings(bool difficultyChanged = false)
	{
		if (difficultyString == "" || difficultyChanged)
		{
			if (difficulty == 0)
			{
				difficultyString = "Easy";
			}
			else if (difficulty == 1)
			{
				difficultyString = "Normal";
			}
			else if (difficulty == 2)
			{
				difficultyString = "Hard";
			}
			else if (difficulty == 3)
			{
				difficultyString = "Extreme";
			}
			else
			{
				difficultyString = "Invalid";
			}
		}

		if (modifierCubeTypeString == "")
		{
			if (modifierCubeType == 0)
			{
				modifierCubeTypeString = "Cubes";
			}
			else if (modifierCubeType == 1)
			{
				modifierCubeTypeString = "Spheres";
			}
			else if (modifierCubeType == 2)
			{
				modifierCubeTypeString = "Spheres";
			}
			else
			{
				modifierCubeTypeString = "Invalid";
			}
		}

		if (modifierCameraTypeString == "")
		{
			if (modifierCubeType == 0)
			{
				modifierCameraTypeString = "Static";
			}
			else if (modifierCubeType == 1)
			{
				modifierCameraTypeString = "Upside Down";
			}
			else if (modifierCubeType == 2)
			{
				modifierCameraTypeString = "Spinning";
			}
			else
			{
				modifierCameraTypeString = "Invalid";
			}
		}

	}

	public void updateGraphicsPreset(int graphicsPreset)
	{
		if (graphicsPreset == 0)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
			presetQualityString = "Performance";
			optionsPresetQuality = 0;

			QualitySettings.SetQualityLevel(0);

		}
		if (graphicsPreset == 1)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
			presetQualityString = "Low";
			optionsPresetQuality = 1;
			
			QualitySettings.SetQualityLevel(1);
		}
		if (graphicsPreset == 2)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
			presetQualityString = "Medium";
			optionsPresetQuality = 2;
			
			QualitySettings.SetQualityLevel(2);
		}
		if (graphicsPreset == 3)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = true;
			presetQualityString = "High";
			optionsPresetQuality = 3;
			
			QualitySettings.SetQualityLevel(3);
		}
		if (graphicsPreset == 4)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = true;
			presetQualityString = "Ultra";
			optionsPresetQuality = 4;
			
			QualitySettings.SetQualityLevel(4);
		}
		if (graphicsPreset == 5)
		{
			gameCamera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = optionsDepthOfField;
			presetQualityString = "Custom";
			optionsPresetQuality = 5;

			QualitySettings.SetQualityLevel(5);
		}
	}

	void OnGUI(){
		if (showDebugInfo) {
			GUI.color = Color.green;
			if (GameObject.Find ("playerREF") != null) {
				GUI.Label (new Rect (5, Screen.height - 120, 600, 20), "Player X: " + GameObject.Find ("playerREF").transform.position.x + " Y: " + GameObject.Find ("playerREF").transform.position.y + " Z: " + GameObject.Find ("playerREF").transform.position.z);
			}
			GUI.Label (new Rect (5, Screen.height - 100, 300, 20), "ACCELER. X: " + Input.acceleration.x + " Y: " + Input.acceleration.y + " Z: " + Input.acceleration.z);
			if (Application.loadedLevelName == "game") {
				GUI.Label (new Rect (5, Screen.height - 80, 600, 20), "Analog: " + GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>().analogTurn);
			}
			GUI.Label (new Rect (5, Screen.height - 60, 300, 20), "Level: " + Application.loadedLevelName);
			GUI.Label (new Rect (5, Screen.height - 40, 300, 20), "FPS: " + 1 / Time.deltaTime);
			if (enableDevCheats) {
				GUI.Label (new Rect (5, Screen.height - 140, 300, 20), "Dev Cheats Enabled!");
			}
			if (Application.loadedLevelName == "menu") {
				if (GUI.Button (new Rect (20, Screen.height - 220, 200, 75), "Enable Dev Cheats")) {
					if (enableDevCheats) {
						enableDevCheats = false;
					} else {
						enableDevCheats = true;
					}
				}
				if (GUI.Button (new Rect (230, Screen.height - 220, 200, 75), "Skip to credits_menu")) {
					Application.LoadLevel ("credits_menu");
				}
			}
			if (Application.loadedLevelName == "game_progressive" || Application.loadedLevelName == "game_progressive_finale") {
				if (GUI.Button (new Rect (Screen.width - 210, 5, 200, 75), "Skip 1000")) {
					GameObject.Find ("playerREF").transform.position = new Vector3 (GameObject.Find ("playerREF").transform.position.x, GameObject.Find ("playerREF").transform.position.y, GameObject.Find ("playerREF").transform.position.z + 1000);
				}
				if (GUI.Button (new Rect (Screen.width - 420, 5, 200, 75), "Reduce Camera Clip")) {
					GameObject.Find ("cameraREF").GetComponent<Camera> ().farClipPlane = 1000;
				}
			}
			if (Application.loadedLevelName == "credits_menu" || Application.loadedLevelName == "game_progressive_finale") {
				if (GUI.Button (new Rect (Screen.width - 630, 5, 200, 75), "Increase atmosphere")) {
					GameObject.Find ("masterLight").GetComponent<autoIntensity> ().dayAtmosphereThickness = GameObject.Find ("masterLight").GetComponent<autoIntensity> ().dayAtmosphereThickness + 0.5f;
					GameObject.Find ("masterLight").GetComponent<autoIntensity> ().nightAtmosphereThickness = GameObject.Find ("masterLight").GetComponent<autoIntensity> ().nightAtmosphereThickness + 0.5f;
				}
				if (GUI.Button (new Rect (Screen.width - 840, 5, 200, 75), "Decrease atmosphere")) {
					GameObject.Find ("masterLight").GetComponent<autoIntensity> ().dayAtmosphereThickness = GameObject.Find ("masterLight").GetComponent<autoIntensity> ().dayAtmosphereThickness - 0.5f;
					GameObject.Find ("masterLight").GetComponent<autoIntensity> ().nightAtmosphereThickness = GameObject.Find ("masterLight").GetComponent<autoIntensity> ().nightAtmosphereThickness - 0.5f;

				}
				if (GUI.Button (new Rect (Screen.width - 1050, 5, 200, 75), "Toggle Light")) {
					GameObject.Find ("masterLight").GetComponent<Light> ().enabled = !GameObject.Find ("masterLight").GetComponent<Light> ().enabled;
				}
			}

		}
	}
}
