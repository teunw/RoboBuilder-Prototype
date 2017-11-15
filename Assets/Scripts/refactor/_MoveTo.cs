using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRTK;

public class _MoveTo : RobotBehaviourScript
{
    public GameObject Target;

    public float Speed = .1f;

    // Use this for initialization
    void Start()
    {
        if (Target == null)
        {
            throw new NullReferenceException("Could not find a target");
        }
        if (Target == null)
        {
            Target = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Target.transform.localScale = new Vector3(.2f, .2f, .2f);
            Target.transform.position = Target.transform.position + new Vector3(0, this.transform.localScale.y * 2, 0);
            Target.AddComponent<Rigidbody>();
            Target.AddComponent<Collider>();
            Target.AddComponent<VRTK_InteractableObject>().isGrabbable = true;
        }
    }

    private void Update()
    {
        var step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
    }



    public override void Copy<T>(ref T copyO)
    {
        base.Copy(ref copyO);

        var copy = copyO as _MoveTo;
        if (copy != null)
        {
            copy.Speed = Speed;
            copy.Target = Target;
        }
        else
        {
            throw new NotImplementedException("Somthing went horribly wrong");
        }
    }
}