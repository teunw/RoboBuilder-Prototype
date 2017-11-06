using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Rotate : RobotBehaviourScript {

	public Vector3 Step = new Vector3(.5f,.5f,.5f);

	void Update () {
		if (enabled)
		{
			transform.eulerAngles = transform.eulerAngles + Step * Time.deltaTime;
		}
	}
}
