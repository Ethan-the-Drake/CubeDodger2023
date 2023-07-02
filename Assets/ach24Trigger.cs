using UnityEngine;
using System.Collections;

public class ach24Trigger : MonoBehaviour {

	AchievementsScript achievements;

	void Awake(){
		achievements = GameObject.Find ("settingsHolder").GetComponent<AchievementsScript> ();
		if (achievements == null) {
			Destroy (gameObject);
			Debug.Log ("Warning: Ach24 Unavailable due to bad achievements assignment.");
		}
	}

	void OnTriggerEnter(Collider collider){
		if (!achievements.achBrokenCubeCutters && collider.gameObject.name == "playerREF") {
			achievements.achBrokenCubeCutters = true;
			achievements.queueAch (24);
			achievements.saveAchievements ();
		}
	}
}
