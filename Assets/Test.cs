using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Test : VRTK_InteractableObject
{

	public int TestVariable = 0;

	public override void OnInteractableObjectUsed(InteractableObjectEventArgs e)
	{
		base.OnInteractableObjectUsed(e);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
