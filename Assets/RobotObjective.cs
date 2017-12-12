using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class RobotObjective : MonoBehaviour
{
	public Receiver Receiver { get; private set; }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.HasComponent<RobotBehaviourScript>())
		{
			this.Receiver = other.gameObject.GetComponent<Receiver>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.HasComponent<Receiver>() && 
		    Receiver != null &&
		    other.gameObject.GetComponent<Receiver>().Equals(this.Receiver))
		{
			this.Receiver = null;
		}
	} 
}
