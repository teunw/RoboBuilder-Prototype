using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleTrigger : MonoBehaviour {

	public ParticleSystem ParticleSystem { get; private set; }
	
	// Use this for initialization
	public void Start ()
	{
	}

	public void TriggerParticles()
	{
		if (this.ParticleSystem == null)
		{
			this.ParticleSystem = GetComponent<ParticleSystem>();
		}
		this.ParticleSystem.Play();
	}
}
