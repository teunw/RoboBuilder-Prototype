using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Loop : RobotBehaviourScript {

	
	/// <summary>
	/// To check if it is the start or the end
	/// </summary>
	[ShowInRobot]
	public bool start = true;

	/// <summary>
	/// Reference to the end or start
	/// If this is the start then it will reference to the end script
	/// If this is the end then it will reference to the begin script
	/// </summary>
	public _Loop other;

	[ShowInRobot]
	public int currentIteration = 0;

	/// <summary>
	/// Called at the end of the loop 
	/// </summary>
	/// <returns>true if the loop should end</returns>
	public abstract bool EndOfLoop();
}
