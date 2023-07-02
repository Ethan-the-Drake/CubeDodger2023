using UnityEngine;
using System.Collections;

public class AchievementsScript : MonoBehaviour {
 
	string documentsDataPath = "Documents/My Games/Cube Dodger";
	string achFileName = "save.cdsf";

	public string SaveFilePath;

	public GUISkin achGUISkin;

	System.IO.StreamReader achStreamReader;
	string achFileContents;
	string[] achLines;
	int achLineNumber;
	bool tryParseDummyVarBool;
	int achFileNumberOfLines = 0;

	string[] defaultAch;
	string[] customAch;
	string achFileLine0String = "This file is written while the game is running to save the player's achievement progress. Please do not alter this file directly, or you could lose your save data.";

	public bool ach100points;
	public bool ach1000points;
	public bool ach5000points;
	public bool ach10000points;
	public bool ach20000points;
	public bool ach5000Easy;
	public bool ach5000Standard;
	public bool ach5000Hard;
	public bool ach5000Extreme;
	public bool ach1000Airdrop;
	public bool achDie;
	public bool ach1000Spheres;
	public bool ach1000DrunkDriver;
	public bool ach1000Powerups;
	public bool ach1000SpeedMult;
	public bool achXLimiters;
	public bool ach10000Randomizer;
	public bool achProgressive1;
	public bool achProgressive2;
	public bool achProgressive3;
	public bool achProgressive4;
	public bool achProgressive5;
	public bool achProgressiveContinuous;
	public bool achBrokenCubeCutters;
	public bool achShootCube;
	public bool ach500ExtremeWithAllModifiers;
	public bool achFinishCredits;

	public bool unlockedErrorSong;
	public bool unlockedFarewellTheInnocent;
	public bool unlockedSamaritan;
	public bool unlockedLittleThings;

	//public bool doPopAchievement;
	public int[] achQueue;
	bool lookingForOpenQueue;
	public int currentQueue;
	public int popAchievementID;
	float achievementTimer;
	int achPopHeight;

	public bool isContinuousProgressiveRun;

	public void Awake(){
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			SaveFilePath = "C:/Users/" + System.Environment.UserName + "/" + documentsDataPath;
			Debug.Log ("ACH: " + SaveFilePath);
		} else if (Application.platform == RuntimePlatform.Android) {
			SaveFilePath = Application.persistentDataPath;
			Debug.Log ("ACH: " + SaveFilePath);
		}
			
		checkSaveData ();
	}

	public void queueAch(int achievement){
		currentQueue = 0;
		foreach (int ach in achQueue) {
			if (achQueue [currentQueue] == 0) {
				achQueue [currentQueue] = achievement;
				break;
			} else {
				currentQueue++;
			}
		}
	}

	void OnGUI() {
		if (achQueue [0] != 0 && popAchievementID == 0) {
			popAchievementID = achQueue [0];
			currentQueue = 0;
			foreach (int ach in achQueue){
				if (currentQueue + 1 < achQueue.Length) {
					achQueue [currentQueue] = achQueue [currentQueue + 1];
					currentQueue++;	
				} else {
					achQueue [currentQueue] = 0;
				}
			}
			Debug.Log ("popping achievement " + popAchievementID);
		}
		else if (popAchievementID != 0) {
			popAchievement (popAchievementID);
		}
	}

	public void popAchievement(int achievement){
		GUI.skin = achGUISkin;
		GUI.Box (new Rect (0, Screen.height - achPopHeight, 250, 100), "");
		GUI.Label (new Rect (0, Screen.height - achPopHeight - 15, 250, 50), "ACHIEVEMENT UNLOCKED", GUI.skin.FindStyle("achievementBox"));

		if (achievement == 1) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Cube Avoider", GUI.skin.FindStyle ("achievementBox"));
		} else if (achievement == 2) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Cube Dodger", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 3) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Cube Cutter", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 4) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Cube Dancer", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 5) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Cube Master", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 6) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Easy Livin'", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 7) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Conventionally Cool", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 8) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Hardened Veteran", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 9) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Extremist", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 10) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Care Package", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 11) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "R.I.P.", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 12) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Dodgeball", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 13) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "DUI", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 14) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Arcade Cabinet", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 15) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Gotta Go Fast!", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 16) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Christopher Columbus", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 17) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Luck of the Draw", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 18) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Meat and Potatoes", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 19) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Lawrence Approved", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 20) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Glossophobia", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 21) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Event Horizon", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 22) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "The End", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 23) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Officially Crazy", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 24) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Non-Conformist", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 25) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Cube Destroyer", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 26) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Masochist", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 27) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Thank You!", GUI.skin.FindStyle ("achievementBox")); 
		} else if (achievement == 255) {
			GUI.Label (new Rect (0, Screen.height - achPopHeight + 35, 250, 40), "Dummy Achievement AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", GUI.skin.FindStyle("achievementBox"));
		} 

		if (achPopHeight < 100 && achievementTimer < 5) {
			achPopHeight = achPopHeight + 5;
		} else if (achievementTimer < 5) {
			achievementTimer = achievementTimer + 1 * Time.deltaTime;
		} else if (achPopHeight > 0) {
			achPopHeight = achPopHeight - 5;
		} else {
			popAchievementID = 0;

			achievementTimer = 0;
			achPopHeight = 0;
		}
	}

	public void checkSaveData()
	{
		try
		{
			achStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + achFileName);
			achFileContents = achStreamReader.ReadToEnd ();
		}
		catch (System.IO.FileNotFoundException)
		{
			print ("No ach File Found, First Launch or Corrupted File? Creating a new file.");
			resetAchievements();

			achStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + achFileName);
			achFileContents = achStreamReader.ReadToEnd ();

		}
		catch (System.IO.IsolatedStorage.IsolatedStorageException)
		{
			print ("No ach File Found, First Launch or Corrupted File? Creating a new file.");
			resetAchievements();

			achStreamReader = new System.IO.StreamReader (SaveFilePath + "/" + achFileName);
			achFileContents = achStreamReader.ReadToEnd ();
		}
		achLines = achFileContents.Split ("\n" [0]);
		achLineNumber = 0;
		foreach (string line in achLines) {
			if (achLineNumber != 0 && achLineNumber <= 31)
			{
				if (bool.TryParse(line, out tryParseDummyVarBool) == false)
				{
					Debug.Log ("line " + achLineNumber + " does not conform with expected format for ach file. Returned " + line + ", Expected Bool. Deleting and resetting ach file.");
					achStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + achFileName);
					checkSaveData();
					break;
				}
			}
			else if (achLineNumber != 0 && achLineNumber <= 31)
			{
				Debug.Log ("Unknown error at line " + achLineNumber + ". Here's the line: " + line);
			}

			if (achLineNumber < 31)
			{
				try
				{
					print (achLines[achLineNumber + 1]);
				}
				catch (System.IndexOutOfRangeException)
				{
					Debug.Log ("line " + achLineNumber + " does not conform with expected format for ach file. Returned " + line + ", File too small. Deleting and resetting ach file.");
					achStreamReader.Close ();
					System.IO.File.Delete(SaveFilePath + "/" + achFileName);
					checkSaveData();
					break;
				}
			}

			achLineNumber++;
		}

		ach100points = bool.Parse (achLines [1]);
		ach1000points = bool.Parse (achLines [2]);
		ach5000points = bool.Parse (achLines [3]);
		ach10000points = bool.Parse (achLines [4]);
		ach20000points = bool.Parse (achLines [5]);
		ach5000Easy = bool.Parse (achLines [6]);
		ach5000Standard = bool.Parse (achLines [7]);
		ach5000Hard = bool.Parse (achLines [8]);
		ach5000Extreme = bool.Parse (achLines [9]);
		ach1000Airdrop = bool.Parse (achLines [10]);
		achDie = bool.Parse (achLines [11]);
		ach1000Spheres = bool.Parse (achLines [12]);
		ach1000DrunkDriver = bool.Parse (achLines [13]);
		ach1000Powerups = bool.Parse (achLines [14]);
		ach1000SpeedMult = bool.Parse (achLines [15]);
		achXLimiters = bool.Parse (achLines [16]);
		ach10000Randomizer = bool.Parse (achLines [17]);
		achProgressive1 = bool.Parse (achLines [18]);
		achProgressive2 = bool.Parse (achLines [19]);
		achProgressive3 = bool.Parse (achLines [20]);
		achProgressive4 = bool.Parse (achLines [21]);
		achProgressive5 = bool.Parse (achLines [22]);
		achProgressiveContinuous = bool.Parse (achLines [23]);
		achBrokenCubeCutters = bool.Parse (achLines [24]);
		achShootCube = bool.Parse (achLines [25]);
		ach500ExtremeWithAllModifiers = bool.Parse (achLines [26]);
		achFinishCredits = bool.Parse (achLines [27]);
		
		unlockedErrorSong = bool.Parse (achLines [28]);
		unlockedFarewellTheInnocent = bool.Parse (achLines [29]);
		unlockedSamaritan = bool.Parse (achLines [30]);
		unlockedLittleThings = bool.Parse (achLines [31]);
		
		achStreamReader.Close ();
		
	}

	public void saveAchievements()
	{
		try
		{
			Debug.Log ("Saving Achievements...");
			customAch = new string[]
			{achFileLine0String, 
				ach100points.ToString(),
				ach1000points.ToString(),
				ach5000points.ToString(),
				ach10000points.ToString(),
				ach20000points.ToString(),
				ach5000Easy.ToString(),
				ach5000Standard.ToString(),
				ach5000Hard.ToString(),
				ach5000Extreme.ToString(),
				ach1000Airdrop.ToString(),
				achDie.ToString(),
				ach1000Spheres.ToString(),
				ach1000DrunkDriver.ToString(),
				ach1000Powerups.ToString(),
				ach1000SpeedMult.ToString(),
				achXLimiters.ToString(),
				ach10000Randomizer.ToString(),
				achProgressive1.ToString(),
				achProgressive2.ToString(),
				achProgressive3.ToString(),
				achProgressive4.ToString(),
				achProgressive5.ToString(),
				achProgressiveContinuous.ToString(),
				achBrokenCubeCutters.ToString(),
				achShootCube.ToString(),
				ach500ExtremeWithAllModifiers.ToString(),
				achFinishCredits.ToString(),

				unlockedErrorSong.ToString(),
				unlockedFarewellTheInnocent.ToString(),
				unlockedSamaritan.ToString(),
				unlockedLittleThings.ToString(),
			};
			System.IO.File.WriteAllLines(SaveFilePath + "/" + achFileName, customAch);
		}
		catch (System.IO.DirectoryNotFoundException)
		{
			print ("Error - Directory Not Found. Recreating directory.");
			System.IO.Directory.CreateDirectory(SaveFilePath);
			System.IO.File.Create(SaveFilePath + "/" + achFileName);
			resetAchievements();
		}


	}

	public void resetAchievements(){
		//todo
		defaultAch = new string[]
		{achFileLine0String, 
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false",
			"false"
		};
		System.IO.File.WriteAllLines(SaveFilePath + "/" + achFileName, defaultAch);
	}
}
