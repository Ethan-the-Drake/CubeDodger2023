using UnityEngine;
using System.Collections;

public class world4_particleSystemsScript : MonoBehaviour {

	ProgressiveSceneHandlerScript progSceneHandle;
	public ParticleSystem particle;
	public ParticleSystem.EmissionModule em;
	public ParticleSystem.VelocityOverLifetimeModule volm;

	float targetRate;
	//float currentRate;
	float targetVelocity;
	//float currentVelocity;

	// Use this for initialization
	void Awake () {
		progSceneHandle = GameObject.Find ("SceneHandler").GetComponent<ProgressiveSceneHandlerScript> ();
		particle = gameObject.GetComponent<ParticleSystem> ();
		em = particle.emission;
		volm = particle.velocityOverLifetime;
	}
	
	// Update is called once per frame
	void Update () {
		if (progSceneHandle != null) {
			if (progSceneHandle.progressiveStep < 16) {
				targetRate = 0;
				targetVelocity = 0;
				//em.rate = 0;
				//volm.z = 0;
			} else if (progSceneHandle.progressiveStep == 16) {
				targetRate = 10;
				targetVelocity = 25;
				//em.rate = 10;
				//volm.z = 50;
			} else if (progSceneHandle.progressiveStep == 17) {
				targetRate = 15;
				targetVelocity = 50;
				//em.rate = 15;
				//volm.z = 100;
			} else if (progSceneHandle.progressiveStep == 18) {
				targetRate = 20;
				targetVelocity = 100;
				//em.rate = 20;
				//volm.z = 150;
			} else if (progSceneHandle.progressiveStep == 19) {
				targetRate = 30;
				targetVelocity = 200;
				//em.rate = 30;
				//volm.z = 300;
			} else if (progSceneHandle.progressiveStep == 20) {
				targetRate = 50;
				targetVelocity = 300;
				//em.rate = 50;
				//volm.z = 500;
			}

		}
		//currentRate = em.rate.constant;
		//currentVelocity = volm.z.constant;

		if (em.rate.constant < targetRate) {
			//currentRate = em.rate.constant;
			em.rate = em.rate.constant + 0.5f * Time.deltaTime;
		}

		if (volm.z.constant < targetVelocity) {
			volm.z = volm.z.constant + 2.5f * Time.deltaTime;
		}

	}
}
