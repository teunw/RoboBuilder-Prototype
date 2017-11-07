using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ForLoop : _Loop
{
	[ShowInRobot]
	public int nrOfLoops = 1;

	/// <summary>
	/// Step every loop should take
	/// aka how much the currentIteration should be incremented like in a for loop
	/// </summary>
	[ShowInRobot] 
	public int step = 1;
	
	public override bool EndOfLoop()
	{
		currentIteration += step;
		if (currentIteration < nrOfLoops)
		{
			currentIteration = 0;
			return true;
		}
		return false;
	}
}
