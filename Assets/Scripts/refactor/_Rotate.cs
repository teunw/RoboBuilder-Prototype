using System;
using UnityEngine;

public class _Rotate : RobotBehaviourScript {

	[ShowInRobot]
	public Vector3 Step = new Vector3(0,90,0);

	[ShowInRobot]
	public Vector3 Total = new Vector3(0,90,0);

	public Vector3 Current = new Vector3(0,0,0);
	
	void Update () {
		if (Enabled)
		{
			Vector3 a = Step * Time.deltaTime;
			if (Current.y + a.y <= Total.y)
			{
				transform.eulerAngles = transform.eulerAngles + a;
			}
			else if(Current.y + a.y > Total.y)
			{
				Debug.Log("total - current" +  (Total.y - Current.y));
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, Total.y - Current.y, transform.eulerAngles.z);
			}

			Current += Step * Time.deltaTime;
			if (Current.x >= Total.x && Current.y >=Total.y && Current.z >=Total.z)
			{
				Current = new Vector3();
				Done();
			}
		}
	}
	
	public override void Copy<T>(ref T copyO)
	{
		base.Copy(ref copyO);
		
		var copy = copyO as _Rotate;
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
