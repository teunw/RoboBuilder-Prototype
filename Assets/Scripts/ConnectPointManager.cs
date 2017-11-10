using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ConnectPointManager : MonoBehaviour
{
	public ConnectPoint[] ConnectPoints;
	public StartCube StartCube;
	public Receiver Receiver;

	public virtual void Start()
	{
		var manager = this;
		ConnectPoints.ForEach(cp => cp.ConnectPointManager = manager);
	}

	public void DisableExcept(ConnectPoint connectPoint)
	{
		ConnectPoints
			.Where(c => c != connectPoint)
			.ForEach(c => c.gameObject.transform.parent.transform.gameObject.SetActive(false));
	}
}
