using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class RobotObjective : MonoBehaviour
{
	public Receiver Receiver { get; private set; }

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("Something entered trigger");
		if (other.gameObject.HasComponent<RobotBehaviourScript>())
		{
			Debug.Log("Receiver entered trigger");
			this.Receiver = other.gameObject.GetComponent<Receiver>();
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.HasComponent<Receiver>() && 
		    Receiver != null &&
		    other.gameObject.GetComponent<Receiver>().Equals(this.Receiver))
		{
			Debug.Log("Receiver left trigger");
			this.Receiver = null;
		}
	} 
}
