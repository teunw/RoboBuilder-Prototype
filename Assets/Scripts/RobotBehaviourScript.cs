using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotBehaviourScript : MonoBehaviour
{
	public Robot Robot;
	public bool enabled = true;
	
	public virtual void OnBehaviourTriggered() {}
	public virtual void Update() {}
}

public class ShowInRobot : Attribute
{
}