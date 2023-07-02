using UnityEngine;
using System.Collections;

public class leaderboardsHolder : MonoBehaviour {

	SceneHandlerScript sceneScript;
	menuHandlerScript menuScript;
	settingsScript settings;

	public string SaveFilePath;

	string documentsDataPath = "Documents/My Games/Cube Dodger";
	string leaderboardsFileName = "/leaderboards.cdtf";
	string[] leaderboardsDefaultContents;
	string[] leaderboardsFileContent;
	string[] leaderboardsSaveContent;
	int lineNumber;
	int lineDummyVarInt;
	public bool showLeaderboardsError;

	//string[] leaderboardsDifficultyString = {"Easy", "Normal", "Hard", "Extreme"};


	public int leaderboard1;
	public int leaderboard2;
	public int leaderboard3;
	public int leaderboard4;
	public int leaderboard5;
	public int leaderboard6;
	public int leaderboard7;
	public int leaderboard8;
	public int leaderboard9;
	public int leaderboard10;

	int[] leaderboardScoresSortedList = new int[]{0,0,0,0,0,0,0,0,0,0};

	void Awake(){
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			SaveFilePath = "C:/Users/" + System.Environment.UserName + "/" + documentsDataPath;
			Debug.Log ("LB: " + SaveFilePath);
		} else if (Application.platform == RuntimePlatform.Android) {
			SaveFilePath = Application.persistentDataPath;
			Debug.Log ("LB: " + SaveFilePath);
		}
	}

	// Use this for initialization
	void Start () 
	{
		settings = gameObject.GetComponent<settingsScript> ();
		if (Application.loadedLevelName == "menu")
		{
			menuScript = GameObject.Find("menuHandler").GetComponent<menuHandlerScript>();
		}
		else if (Application.loadedLevelName == "game")
		{
			sceneScript = GameObject.Find ("sceneHandler").GetComponent<SceneHandlerScript>();
		}

		checkFile ();
	}

	void checkFile()
	{
		try
		{
			leaderboardsFileContent = System.IO.File.ReadAllLines(SaveFilePath + leaderboardsFileName);
		}
		catch (System.IO.FileNotFoundException)
		{
			resetLeaderboards();
		}
		catch (System.IO.IsolatedStorage.IsolatedStorageException)
		{
			resetLeaderboards();
		}
		
		lineNumber = 0;
		foreach (string line in leaderboardsFileContent)
		{
			if (lineNumber != 0 && lineNumber != 1 && lineNumber != 12 && lineNumber != 23 && lineNumber != 34 && lineNumber != 45 && lineNumber != 56)
			{
				if (int.TryParse(line, out lineDummyVarInt) == false)
				{
					Debug.Log ("Leaderboards file is corrupted. Wiping leaderboards and generating a new file.");
					createLeaderboardsErrorLog(line, lineNumber, "Error at line " + lineNumber + ", '" + line + "', Wiping leaderboards and generating a new file.");
					resetLeaderboards();
				}
			}
			lineNumber++;
		}
		if (lineNumber != 67)
		{
			Debug.Log (lineNumber + ", File wrong size error.");
			Debug.Log ("Leaderboards file is corrupted or you updated to a new version of Cube Dodger. Wiping leaderboards and generating a new file.");
			try
			{
				createLeaderboardsErrorLog(leaderboardsFileContent[lineNumber], lineNumber, "Error at line " + lineNumber + ", '" + leaderboardsFileContent[lineNumber] + "', File wrong size error. Wiping leaderboards and generating a new file.");
			}
			catch (System.IndexOutOfRangeException)
			{
				createLeaderboardsErrorLog("No Information Available", lineNumber, "Error at line " + lineNumber + ", '" + "No Information Available (File corrupted or deleted)" + "', Wiping leaderboards and generating a new file.");
			}
			resetLeaderboards();
		}
	}

	public void resetLeaderboards()
	{
		leaderboardsDefaultContents = new string[]
		{
			"Leaderboards file. Do not alter this file manually, your scores may be wiped.",
			"[Standard - Easy]",
			"0", //Line 2
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"[Standard - Normal]",
			"0", //13
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"[Standard - Hard]",
			"0", //24
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"[Standard - Extreme]",
			"0", //35
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"[Progressive]",
			"0", //46
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"[Randomized]",
			"0", //57
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
			"0",
		};
		//System.IO.File.Create(SaveFilePath + leaderboardsFileName);
		System.IO.File.WriteAllLines(SaveFilePath + leaderboardsFileName, leaderboardsDefaultContents);
		
		leaderboardsFileContent = System.IO.File.ReadAllLines(SaveFilePath + leaderboardsFileName);
	}

	void createLeaderboardsErrorLog(string line, int lineNumber, string ErrorMessage)
	{
		System.IO.Directory.CreateDirectory (SaveFilePath + "/ErrorLogs");
		if (menuScript != null)
		{
			System.IO.File.WriteAllText(SaveFilePath + "/ErrorLogs/LeaderboardsErrorLog.txt", "[" + System.DateTime.Now.ToString() + "] [Game Version: " + settings.gameVersion + "] Error Log. Error at line " + lineNumber + ", '" + line + "', " + ErrorMessage);
			showLeaderboardsError = true;
		}
		else if (sceneScript != null)
		{
			System.IO.File.WriteAllText(SaveFilePath + "/ErrorLogs/LeaderboardsErrorLog.txt", "[" + System.DateTime.Now.ToString() + "] [Game Version: " + settings.gameVersion + "] Error Log. Error at line " + lineNumber + ", '" + line + "', " + ErrorMessage); 
			sceneScript.gamePaused = true;
			showLeaderboardsError = true;
		}
	}

	public void pullLeaderboards(float difficulty, bool randomizer, bool progressive, int difficultyInt = 0)
	{
		difficultyInt = Mathf.FloorToInt (difficulty);
		checkFile ();
		if (randomizer == false && progressive == false)
		{
			leaderboard1 = int.Parse (leaderboardsFileContent [2 + (difficultyInt * 11)]);
			leaderboard2 = int.Parse (leaderboardsFileContent [3 + (difficultyInt * 11)]);
			leaderboard3 = int.Parse (leaderboardsFileContent [4 + (difficultyInt * 11)]);
			leaderboard4 = int.Parse (leaderboardsFileContent [5 + (difficultyInt * 11)]);
			leaderboard5 = int.Parse (leaderboardsFileContent [6 + (difficultyInt * 11)]);
			leaderboard6 = int.Parse (leaderboardsFileContent [7 + (difficultyInt * 11)]);
			leaderboard7 = int.Parse (leaderboardsFileContent [8 + (difficultyInt * 11)]);
			leaderboard8 = int.Parse (leaderboardsFileContent [9 + (difficultyInt * 11)]);
			leaderboard9 = int.Parse (leaderboardsFileContent [10 + (difficultyInt * 11)]);
			leaderboard10= int.Parse (leaderboardsFileContent [11 + (difficultyInt * 11)]);
		}
		else if (progressive)
		{
			leaderboard1 = int.Parse (leaderboardsFileContent [46]);
			leaderboard2 = int.Parse (leaderboardsFileContent [47]);
			leaderboard3 = int.Parse (leaderboardsFileContent [48]);
			leaderboard4 = int.Parse (leaderboardsFileContent [49]);
			leaderboard5 = int.Parse (leaderboardsFileContent [50]);
			leaderboard6 = int.Parse (leaderboardsFileContent [51]);
			leaderboard7 = int.Parse (leaderboardsFileContent [52]);
			leaderboard8 = int.Parse (leaderboardsFileContent [53]);
			leaderboard9 = int.Parse (leaderboardsFileContent [54]);
			leaderboard10= int.Parse (leaderboardsFileContent [55]);
		}
		else if (randomizer)
		{
			leaderboard1 = int.Parse (leaderboardsFileContent [57]);
			leaderboard2 = int.Parse (leaderboardsFileContent [58]);
			leaderboard3 = int.Parse (leaderboardsFileContent [59]);
			leaderboard4 = int.Parse (leaderboardsFileContent [60]);
			leaderboard5 = int.Parse (leaderboardsFileContent [61]);
			leaderboard6 = int.Parse (leaderboardsFileContent [62]);
			leaderboard7 = int.Parse (leaderboardsFileContent [63]);
			leaderboard8 = int.Parse (leaderboardsFileContent [64]);
			leaderboard9 = int.Parse (leaderboardsFileContent [65]);
			leaderboard10= int.Parse (leaderboardsFileContent [66]);
		}

	}
	public void saveLeaderboards(float difficulty, bool randomizer, bool progressive, int difficultyInt = 0)
	{
		//C# is not capable of rewriting individual lines without rewriting the entire file. It's a pain, but whatever.

		//1. Read the entire existing leaderboards file, copy each line into a string[].
		Debug.Log (leaderboardsFileContent);
		if (!settings.modifierRandomizer)
		{
			difficultyInt = Mathf.FloorToInt(settings.difficulty);
		}
		else
		{
			difficultyInt = 5;
		}
		//2. Derive which lines need to be edited using the difficulty variable.
		if (!progressive && !randomizer)
		{
			leaderboardsFileContent[2 + (difficultyInt * 11)] = leaderboard1.ToString();
			leaderboardsFileContent[3 + (difficultyInt * 11)] = leaderboard2.ToString();
			leaderboardsFileContent[4 + (difficultyInt * 11)] = leaderboard3.ToString();
			leaderboardsFileContent[5 + (difficultyInt * 11)] = leaderboard4.ToString();
			leaderboardsFileContent[6 + (difficultyInt * 11)] = leaderboard5.ToString();
			leaderboardsFileContent[7 + (difficultyInt * 11)] = leaderboard6.ToString();
			leaderboardsFileContent[8 + (difficultyInt * 11)] = leaderboard7.ToString();
			leaderboardsFileContent[9 + (difficultyInt * 11)] = leaderboard8.ToString();
			leaderboardsFileContent[10 + (difficultyInt * 11)] = leaderboard9.ToString();
			leaderboardsFileContent[11 + (difficultyInt * 11)] = leaderboard10.ToString();
		}
		else if (progressive)
		{
			leaderboardsFileContent[46] = leaderboard1.ToString();
			leaderboardsFileContent[47] = leaderboard2.ToString();
			leaderboardsFileContent[48] = leaderboard3.ToString();
			leaderboardsFileContent[49] = leaderboard4.ToString();
			leaderboardsFileContent[50] = leaderboard5.ToString();
			leaderboardsFileContent[51] = leaderboard6.ToString();
			leaderboardsFileContent[52] = leaderboard7.ToString();
			leaderboardsFileContent[53] = leaderboard8.ToString();
			leaderboardsFileContent[54] = leaderboard9.ToString();
			leaderboardsFileContent[55] = leaderboard10.ToString();
		}
		else if (randomizer)
		{
			leaderboardsFileContent[57] = leaderboard1.ToString();
			leaderboardsFileContent[58] = leaderboard2.ToString();
			leaderboardsFileContent[59] = leaderboard3.ToString();
			leaderboardsFileContent[60] = leaderboard4.ToString();
			leaderboardsFileContent[61] = leaderboard5.ToString();
			leaderboardsFileContent[62] = leaderboard6.ToString();
			leaderboardsFileContent[63] = leaderboard7.ToString();
			leaderboardsFileContent[64] = leaderboard8.ToString();
			leaderboardsFileContent[65] = leaderboard9.ToString();
			leaderboardsFileContent[66] = leaderboard10.ToString();
		}

		//System.IO.File.Create(SaveFilePath + leaderboardsFileName);
		System.IO.File.WriteAllLines(SaveFilePath + leaderboardsFileName, leaderboardsFileContent);
	}

	public void recordScore(float score, int scoreInt = 0)
	{
		checkFile ();
		pullLeaderboards (settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);
		scoreInt = Mathf.FloorToInt (score);
		if (scoreInt > leaderboard10)
		{
			leaderboardScoresSortedList [0] = leaderboard1;
			leaderboardScoresSortedList [1] = leaderboard2;
			leaderboardScoresSortedList [2] = leaderboard3;
			leaderboardScoresSortedList [3] = leaderboard4;
			leaderboardScoresSortedList [4] = leaderboard5;
			leaderboardScoresSortedList [5] = leaderboard6;
			leaderboardScoresSortedList [6] = leaderboard7;
			leaderboardScoresSortedList [7] = leaderboard8;
			leaderboardScoresSortedList [8] = leaderboard9;
			leaderboardScoresSortedList [9] = scoreInt;

			System.Array.Sort(leaderboardScoresSortedList);
			System.Array.Reverse(leaderboardScoresSortedList);

			leaderboard1 = leaderboardScoresSortedList[0];
			leaderboard2 = leaderboardScoresSortedList[1];
			leaderboard3 = leaderboardScoresSortedList[2];
			leaderboard4 = leaderboardScoresSortedList[3];
			leaderboard5 = leaderboardScoresSortedList[4];
			leaderboard6 = leaderboardScoresSortedList[5];
			leaderboard7 = leaderboardScoresSortedList[6];
			leaderboard8 = leaderboardScoresSortedList[7];
			leaderboard9 = leaderboardScoresSortedList[8];
			leaderboard10 = leaderboardScoresSortedList[9]; 

			saveLeaderboards(settings.difficulty, settings.modifierRandomizer, settings.modifierProgressive);
		}

	}
	// Update is called once per frame
	void Update () 
	{
		if (sceneScript == null && menuScript == null)
		{
			if (Application.loadedLevelName == "menu")
			{
				menuScript = GameObject.Find("menuHandler").GetComponent<menuHandlerScript>();
			}
			else if (Application.loadedLevelName == "game")
			{
				sceneScript = GameObject.Find ("SceneHandler").GetComponent<SceneHandlerScript>();
			}
		}



	}
}
