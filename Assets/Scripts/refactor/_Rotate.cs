﻿using System;
using UnityEngine;

public class _Rotate : RobotBehaviourScript {

	public Vector3 Step = new Vector3(.5f,.5f,.5f);

	void Update () {
		if (enabled)
		{
			transform.eulerAngles = transform.eulerAngles + Step * Time.deltaTime;
		}
	}
	
	public override void Copy<T>(ref T copyO)
	{
		base.Copy(ref copyO);
		
		var copy = copyO as _Rotate;
		if (copy != null)
		{
			copy.Step = Step;
			copy.enabled = enabled;
			copy.Cube = Cube;
		}
		else
		{
			throw new NotImplementedException("Somthing went horribly wrong");
		}
	}
}
