using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotBehaviourScript : MonoBehaviour
{
	public Robot Robot;
	
	public virtual void OnBehaviourTriggered() {}
}
