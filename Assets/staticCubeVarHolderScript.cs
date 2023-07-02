using UnityEngine;
using System.Collections;

public class staticCubeVarHolderScript : MonoBehaviour {

	//This script exists for optimization reasons. When initializing gameobject variables for static cubes, which all end up being the same, calling it thousands of times
	//increases load times... so we store them in this script in a parent object and have the static cube script reference this one instead which should hopefully
	//reduce load times.

	public settingsScript settings;
	public GameObject gameCamera;
}
