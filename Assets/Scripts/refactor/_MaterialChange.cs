using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MaterialChange : RobotBehaviourScript
{
	public Material Material;
	
	void Update () {
		GetComponent<Renderer>().material = Material;
		Done();
	}
}
