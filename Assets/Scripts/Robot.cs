using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{

	public List<RobotBehaviourScript> BehaviourScripts = new List<RobotBehaviourScript>();
	
	public void OnTriggerEnter(Collider other)
	{
		var robotBehaviour = other.GetComponent<RobotBehaviourScript>();
		if (robotBehaviour != null && !BehaviourScripts.Contains(robotBehaviour))
		{
			Debug.Log("Triggering behaviour");
			BehaviourScripts.Add(robotBehaviour);
			BehaviourScripts.Last().OnBehaviourTriggered();
		}
	}
}
