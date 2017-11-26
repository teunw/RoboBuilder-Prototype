using UnityEngine;

public class _MaterialChange : RobotBehaviourScript
{
	public Material Material;
	
	void Update () 
	{
		if (Enabled)
		{
			GetComponent<Renderer>().material = Material;
			Done();
		}
	}
}
