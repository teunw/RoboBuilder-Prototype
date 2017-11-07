using System;
using UnityEngine;

public abstract class RobotBehaviourScript : MonoBehaviour
{
	public Robot Robot;
	public bool enabled = true;
	
	public virtual void OnBehaviourTriggered() {}
}

public class ShowInRobot : Attribute
{
}