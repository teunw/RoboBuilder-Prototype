﻿using UnityEngine;

public class _Move : RobotBehaviourScript {

	[ShowInRobot]
	public Vector3 Step = new Vector3(0,0,.1f);
	
	void Update () {
		if (enabled)
		{
			this.transform.position = this.transform.position + Step * Time.deltaTime;
		}
	}

	public override string ToString()
	{
		return this.name;
	}
}