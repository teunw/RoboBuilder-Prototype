using System;
using UnityEngine;

public class _Rotate : RobotBehaviourScript {

	[ShowInRobot]
	public Vector3 Step = new Vector3(0,90,0);

	[ShowInRobot]
	public Vector3 Total = new Vector3(0,90,0);

	public Vector3 Current = new Vector3(0,0,0);
	
	public float startDegree;
	
	private void Start()
	{
		Init();
		EnabledChanged();
	}

	protected override void EnabledChanged()
	{
		base.EnabledChanged();
		if (Enabled)
		{
			startDegree = transform.eulerAngles.y;
		}
	}
	private bool _wasDisabled = true;

	void Update () {
		if (!Enabled)
		{
			_wasDisabled = true;
		}
		if (Enabled)
		{
			if (_wasDisabled)
			{
				startDegree = transform.eulerAngles.y;
				_wasDisabled = false;
			}
			Vector3 deltaStep = Step * Time.deltaTime;
			if (Current.y + deltaStep.y < Total.y)
			{
				transform.eulerAngles = transform.eulerAngles + deltaStep;
			}
			else if(Current.y + deltaStep.y >= Total.y)
			{
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, (startDegree+ Total.y)%360, transform.eulerAngles.z);
			}

			Current += Step * Time.deltaTime;
			if (Current.y >=Total.y)
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
