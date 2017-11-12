using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotBehaviourScript))]
public class Transmitter : MonoBehaviour
{
	[HideInInspector]
	public RobotBehaviourScript BehaviourScript;
	
	private void Start()
	{
		BehaviourScript = gameObject.GetComponent<RobotBehaviourScript>();
		if (BehaviourScript == null)
		{
			throw new NullReferenceException("Could not find a RobotBehaviourScript on " + gameObject.name);
		}	
	}
	
	
}
