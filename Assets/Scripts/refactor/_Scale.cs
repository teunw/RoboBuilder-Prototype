﻿using System;
using UnityEngine;

public class _Scale : RobotBehaviourScript {

	public Vector3 Step = new Vector3(.5f,.5f,.5f);

	void Update () {
		if (Enabled)
		{
			transform.localScale = transform.localScale + Step * Time.deltaTime;
		}
	}

	public override void Copy<T>(ref T copyO)
	{
		var copy = copyO as _Move;
		if (copy != null)
		{
			copy.Step = Step;
			copy.Enabled = Enabled;
		}
		else
		{
			throw new NotImplementedException("Somthing went horribly wrong");
		}
	}
}
