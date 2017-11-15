using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererFollowTargets : MonoBehaviour
{
	public Transform[] GameObjects;
	public LineRenderer LineRenderer;

	public void Start()
	{
		LineRenderer = GetComponent<LineRenderer>();
		if (LineRenderer.positionCount != this.GameObjects.Length)
		{
			throw new Exception("Line renderer count doesn't match");
		}
	}

	// Update is called once per frame
	void Update () {
		if (GameObjects.Length <= 0)
		{
			return;
		}
		if (LineRenderer.positionCount != this.GameObjects.Length)
		{
			return;
		}
		for (var i = 0; i < GameObjects.Length; i++)
		{
			LineRenderer.SetPosition(i, GameObjects[i].position);
		}
	}
}
