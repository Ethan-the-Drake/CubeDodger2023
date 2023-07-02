using UnityEngine;
using System.Collections;

public class XLimiterParticleScript : MonoBehaviour {

	GameObject player;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("playerREF");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		try {
			if (!GameObject.Find ("settingsHolder").GetComponent<settingsScript>().screenshotMode)
			{
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, player.transform.position.z+200);
			}
			else
			{
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, player.transform.position.z);
			}
		} catch {
			Destroy (gameObject);
		}
	}
}
