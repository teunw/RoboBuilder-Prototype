using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class RobotObjective : MonoBehaviour
{
	public RobotBehaviourScript RobotBehaviourScript { get; private set; }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.HasComponent<RobotBehaviourScript>())
		{
			this.RobotBehaviourScript = other.gameObject.GetComponent<RobotBehaviourScript>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.HasComponent<RobotBehaviourScript>() && 
		    RobotBehaviourScript != null &&
		    other.gameObject.GetComponent<RobotBehaviourScript>().Equals(this.RobotBehaviourScript))
		{
			this.RobotBehaviourScript = null;
		}
	} 
}
