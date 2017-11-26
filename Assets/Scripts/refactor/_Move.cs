using System;
using System.Reflection;
using UnityEngine;

public class _Move : RobotBehaviourScript {

	[ShowInRobot]
	public Vector3 Step = new Vector3(0,0,.1f);

	[ShowInRobot]
	public Vector3 Total = new Vector3(0,0,1f);

	public Vector3 Current = new Vector3(0,0,0);

	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			foreach (FieldInfo field in GetFields())
			{
				Debug.Log(field.Name);
			}
		}
		
		if (Enabled)
		{
			transform.position = transform.position + Step * Time.deltaTime;
			
			//todo : make sure this works for all directions
			Current += Step * Time.deltaTime;
			if (Current.x >= Total.x && Current.y >=Total.y && Current.z >=Total.z)
			{
				Current = new Vector3();
				Done();
			}
		}
	}

	public override string ToString()
	{
		return name;
	}

	public override void Copy<T>(ref T copyO)
	{
		base.Copy(ref copyO);
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
