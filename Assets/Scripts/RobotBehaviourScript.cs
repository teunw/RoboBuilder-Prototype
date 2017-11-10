using System;
using UnityEngine;

public abstract class RobotBehaviourScript : MonoBehaviour
{
	[HideInInspector]
	public GameObject Cube;
	
	[ShowInRobot]
	public bool Enabled = true;

	private void Start()
	{
		this.Cube = gameObject;
	}

	public virtual void OnBehaviourTriggered() {}

	public virtual void Copy<T>(ref T copy) where T : RobotBehaviourScript
	{
		copy.Cube = Cube;
	}

}

public class ShowInRobot : Attribute
{
}