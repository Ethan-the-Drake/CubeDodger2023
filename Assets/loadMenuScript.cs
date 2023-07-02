using UnityEngine;
using System.Collections;

public class loadMenuScript : MonoBehaviour {

	bool waitAFrame;
	public GUISkin CDGUILoadingSkin;
	
	void Start()
	{

	}
	
	void OnGUI () 
	{
		Cursor.visible = false;
		if (GameObject.Find ("settingsHolder") != null)
		{	
			GUI.skin = CDGUILoadingSkin;
			GUI.Label (new Rect (10, Screen.height - 30, 80, 25), "Loading...");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (waitAFrame)
		{
			Application.LoadLevel ("menu");
		}
		else
		{
			waitAFrame = true;
		}
	}
}
