using System;
using UnityEngine;

public abstract class RobotBehaviourScript : MonoBehaviour 
{
	public Robot Robot;
	public bool enabled = true;

	public virtual void OnBehaviourTriggered() {}

	public abstract void Copy<T>(ref T copy) where T : RobotBehaviourScript;

}

public class ShowInRobot : Attribute
{
}