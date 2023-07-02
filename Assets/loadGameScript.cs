using UnityEngine;
using System.Collections;

public class loadGameScript : MonoBehaviour {

	bool waitAFrame;
	public GUISkin CDGUIGameSkin;
	
	
	void OnGUI () 
	{
		Cursor.visible = false;
		GUI.skin = CDGUIGameSkin;
		GUI.Label (new Rect (10, Screen.height - 30, 80, 25), "Loading...");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (waitAFrame)
		{
			Application.LoadLevel ("game");
		}
		else
		{
			waitAFrame = true;
		}
	}
}
