using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableObjectMidair : VRTK_InteractableObject
{
	private Rigidbody midAirRigidbody;

	[Header("Grab/Ungrab parameters", order = 0)]
	public bool UseGravityOnGrab = true;
	public bool IsKinematicOnGrab = false;

	public bool UseGravityOnUngrab = false;
	public bool IsKinematicOnUngrab = true;
	
	// Use this for initialization
	public virtual void Start ()
	{
		midAirRigidbody = GetComponent<Rigidbody>();
	}

	public virtual void SetMoving()
	{
		midAirRigidbody.useGravity = UseGravityOnGrab;
		midAirRigidbody.isKinematic = IsKinematicOnGrab;
	}

	public virtual void SetStill()
	{
		midAirRigidbody.useGravity = UseGravityOnUngrab;
		midAirRigidbody.useGravity = IsKinematicOnUngrab;
	}

	public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
	{
		base.OnInteractableObjectGrabbed(e);
		SetMoving();
	}

	public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
	{
		base.OnInteractableObjectUngrabbed(e);
		SetStill();
	}
}
